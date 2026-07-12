using FluentResults;
using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

public class ServicoMatricula : ServicoBase<Matricula>
{
    private readonly IRepositorioMatricula repositorioMatricula;
    private readonly IRepositorioAluno repositorioAluno;
    private readonly IRepositorioTurma repositorioTurma;

    public ServicoMatricula(
        IRepositorioMatricula repositorioMatricula,
        IRepositorioAluno repositorioAluno,
        IRepositorioTurma repositorioTurma)
    {
        this.repositorioMatricula = repositorioMatricula;
        this.repositorioAluno = repositorioAluno;
        this.repositorioTurma = repositorioTurma;
    }

    public Result Cadastrar(CadastrarMatriculaDto dto)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(dto.AlunoId);
        if (aluno == null)
            return Falha(nameof(dto.AlunoId), "Aluno não encontrado.");

        Turma? turma = repositorioTurma.SelecionarPorId(dto.TurmaId);
        if (turma == null)
            return Falha(nameof(dto.TurmaId), "Turma não encontrada.");

        if (ExisteMatriculaNaTurma(dto.AlunoId, dto.TurmaId))
            return Falha(nameof(dto.AlunoId), "Este aluno já está matriculado nesta turma.");

        int matriculasAtivas = MatriculasAtivasNaTurma(dto.TurmaId);

        if (matriculasAtivas >= turma.CapacidadeAlunos)
            return Falha(nameof(dto.TurmaId), "A turma atingiu a capacidade máxima de alunos matriculados.");

        Matricula matricula = new Matricula(dto.AlunoId, dto.TurmaId, SituacaoMatricula.Ativa);

        Result resultadoValidacao = ValidarEntidade(matricula);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMatricula.Cadastrar(matricula);

        return Result.Ok();
    }

    public Result Cancelar(Guid id)
    {
        Matricula? matricula = repositorioMatricula.SelecionarPorId(id);

        if (matricula == null)
            return Result.Fail("Matrícula não encontrada.");

        matricula.Situacao = SituacaoMatricula.Trancada;

        repositorioMatricula.Editar(id, matricula);

        return Result.Ok();
    }

    public Result Concluir(Guid id)
    {
        Matricula? matricula = repositorioMatricula.SelecionarPorId(id);

        if (matricula == null)
            return Result.Fail("Matrícula não encontrada.");

        matricula.Situacao = SituacaoMatricula.Concluida;

        repositorioMatricula.Editar(id, matricula);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioMatricula.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Matrícula não encontrada.");

        return Result.Ok();
    }

    public List<ListarMatriculaDto> SelecionarTodos()
    {
        List<Matricula> matriculas = repositorioMatricula.SelecionarTodos();
        List<Aluno> alunos = repositorioAluno.SelecionarTodos();
        List<Turma> turmas = repositorioTurma.SelecionarTodos();

        return matriculas
            .Select(m =>
            {
                Aluno? aluno = alunos.FirstOrDefault(a => a.Id == m.AlunoId);
                Turma? turma = turmas.FirstOrDefault(t => t.Id == m.TurmaId);

                return new ListarMatriculaDto(
                    m.Id,
                    aluno?.Nome ?? "Aluno não encontrado",
                    turma?.Nome ?? "Turma não encontrada",
                    m.DataMatricula,
                    m.Situacao
                );
            })
            .ToList();
    }

    public Result<DetalhesMatriculaDto> SelecionarPorId(Guid id)
    {
        Matricula? matricula = repositorioMatricula.SelecionarPorId(id);

        if (matricula == null)
            return Result.Fail("Matrícula não encontrada.");

        Aluno? aluno = repositorioAluno.SelecionarPorId(matricula.AlunoId);
        Turma? turma = repositorioTurma.SelecionarPorId(matricula.TurmaId);

        return Result.Ok(new DetalhesMatriculaDto(
            matricula.Id,
            aluno?.Nome ?? "Aluno não encontrado",
            turma?.Nome ?? "Turma não encontrada",
            matricula.DataMatricula,
            matricula.Situacao
        ));
    }

    private bool ExisteMatriculaNaTurma(Guid alunoId, Guid turmaId)
    {
        return repositorioMatricula
            .Filtrar(m => m.AlunoId == alunoId && m.TurmaId == turmaId)
            .Any();
    }

    private int MatriculasAtivasNaTurma(Guid turmaId)
    {
        return repositorioMatricula
            .Filtrar(m => m.TurmaId == turmaId && m.Situacao == SituacaoMatricula.Ativa)
            .Count();
    }
}