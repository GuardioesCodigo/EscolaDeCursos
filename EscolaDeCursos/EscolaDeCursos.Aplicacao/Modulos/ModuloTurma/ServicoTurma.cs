using FluentResults;
using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public class ServicoTurma : ServicoBase<Turma>
{
    private readonly IRepositorioTurma repositorioTurma;
    private readonly IRepositorioInstrutor repositorioInstrutor;
    private readonly IRepositorioCurso repositorioCurso;

    public ServicoTurma(IRepositorioTurma repositorioTurma, IRepositorioInstrutor repositorioInstrutor, IRepositorioCurso repositorioCurso)
    {
        this.repositorioTurma = repositorioTurma;
        this.repositorioInstrutor = repositorioInstrutor;
        this.repositorioCurso = repositorioCurso;
    }

    public Result Cadastrar(CadastrarTurmaDto dto)
    {
        Turma turma = new Turma(dto.CursoId, dto.InstrutorId, dto.DataInicio, dto.DataTermino, dto.CapacidadeAlunos,dto.Nome);

        Result resultadoValidacao = ValidarEntidade(turma);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioTurma.Cadastrar(turma);

        return Result.Ok();
    }

    public Result Editar(EditarTurmaDto dto)
    {
        Turma turma = new Turma(dto.CursoId, dto.InstrutorId, dto.DataInicio, dto.DataTermino, dto.CapacidadeAlunos, dto.Nome);

        Result resultadoValidacao = ValidarEntidade(turma);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioTurma.Editar(dto.Id, turma);

        if (!conseguiuEditar)
            return Result.Fail("Turma não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioTurma.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Turma não encontrada.");

        return Result.Ok();
    }

    public List<ListarTurmaDto> SelecionarTodos()
    {
        List<Turma> turmas = repositorioTurma.SelecionarTodos();
        List<Curso> cursos = repositorioCurso.SelecionarTodos();
        List<Instrutor> instrutores = repositorioInstrutor.SelecionarTodos();

        return turmas
            .Select(t =>
            {
                Curso? curso = cursos.FirstOrDefault(c => c.Id == t.CursoId);
                Instrutor? instrutor = instrutores.FirstOrDefault(i => i.Id == t.InstrutorId);

                return new ListarTurmaDto(
                    t.Id, t.Nome,
                    curso?.Titulo ?? "Curso não encontrado",
                    instrutor?.Nome ?? "Instrutor não encontrado",
                    t.DataInicio, t.DataTermino, t.CapacidadeAlunos
                );
            })
            .ToList();
    }

    public Result<DetalhesTurmaDto> SelecionarPorId(Guid id)
    {
        Turma? turma = repositorioTurma.SelecionarPorId(id);

        if (turma == null)
            return Result.Fail("Turma não encontrada.");

        Curso? curso = repositorioCurso.SelecionarPorId(turma.CursoId);
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(turma.InstrutorId);

        return Result.Ok(new DetalhesTurmaDto(
            turma.Id,
            turma.Nome,
            curso?.Titulo ?? "Curso não encontrado",
            instrutor?.Nome ?? "Instrutor não encontrado",
            turma.DataInicio,
            turma.DataTermino,
            turma.CapacidadeAlunos
        ));
    }
}