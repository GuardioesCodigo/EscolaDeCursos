using FluentResults;
using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public class ServicoTurma : ServicoBase<Turma>
{
    private readonly IRepositorioTurma repositorioTurma;
    private readonly IRepositorioInstrutor repositorioInstrutor;

    public ServicoTurma(IRepositorioTurma repositorioTurma, IRepositorioInstrutor repositorioInstrutor)
    {
        this.repositorioTurma = repositorioTurma;
        this.repositorioInstrutor = repositorioInstrutor;
    }

    public Result Cadastrar(CadastrarTurmaDto dto)
    {
        Turma turma = new Turma(dto.CursoId, dto.InstrutorId, dto.DataInicio, dto.DataTermino, dto.CapacidadeAlunos);

        Result resultadoValidacao = ValidarEntidade(turma);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioTurma.Cadastrar(turma);

        return Result.Ok();
    }

    public Result Editar(EditarTurmaDto dto)
    {
        Turma turma = new Turma(dto.CursoId, dto.InstrutorId, dto.DataInicio, dto.DataTermino, dto.CapacidadeAlunos);

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
        return repositorioTurma
            .SelecionarTodos()
            .Select(t =>
            {
                Curso? curso = repositorioCurso.SelecionarPorId(t.CursoId);
                Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(t.InstrutorId);

                return new ListarTurmaDto(
                    t.Id,
                    t.Nome,
                    curso?.Nome ?? "Curso não encontrado",
                    instrutor?.Nome ?? "Instrutor não encontrado",
                    t.DataInicio,
                    t.DataTermino,
                    t.CapacidadeAlunos
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
            curso?.Nome ?? "Curso não encontrado",
            instrutor?.Nome ?? "Instrutor não encontrado",
            turma.DataInicio,
            turma.DataTermino,
            turma.CapacidadeAlunos
        ));
    }
}