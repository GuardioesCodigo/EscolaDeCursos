using FluentResults;
using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Compartilhado;


namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public class ServicoTurma : ServicoBase<Turma>
{
    private readonly IRepositorioTurma _repositorioTurma;
    private readonly IRepositorioInstrutor _repositorioInstrutor;
    private readonly IRepositorioCurso _repositorioCurso;

    public ServicoTurma(IRepositorioTurma repositorioTurma, 
                        IRepositorioInstrutor repositorioInstrutor,
                        IRepositorioCurso repositorioCurso)
    {
        // Atribuição correta usando o prefixo _
        _repositorioTurma = repositorioTurma;
        _repositorioInstrutor = repositorioInstrutor;
        _repositorioCurso = repositorioCurso;
    }

   public Result Cadastrar(CadastrarTurmaDto dto)
    {
        Turma turma = new Turma(dto.CursoId, dto.InstrutorId, dto.DataInicio, dto.DataTermino, dto.CapacidadeAlunos, dto.Nome);

        Result resultadoValidacao = ValidarEntidade(turma);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        _repositorioTurma.Cadastrar(turma);

        return Result.Ok();
    }

    public Result Editar(EditarTurmaDto dto)
    {
        Turma turma = new Turma(dto.CursoId, dto.InstrutorId, dto.DataInicio, dto.DataTermino, dto.CapacidadeAlunos, dto.Nome);

        Result resultadoValidacao = ValidarEntidade(turma);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = _repositorioTurma.Editar(dto.Id, turma);

        if (!conseguiuEditar)
            return Result.Fail("Turma não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = _repositorioTurma.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Turma não encontrada.");

        return Result.Ok();
    }

  public List<ListarTurmaDto> SelecionarTodos()
    {
        return _repositorioTurma.SelecionarTodos()
            .Select(t =>
            {
                // Usando os campos declarados com _
                Curso? curso = _repositorioCurso.SelecionarPorId(t.CursoId);
                Instrutor? instrutor = _repositorioInstrutor.SelecionarPorId(t.InstrutorId);

                return new ListarTurmaDto(
                    t.Id,
                    t.Nome,
                    curso?.Titulo ?? "Curso não encontrado", // Usando Titulo, como definido na sua entidade Curso
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
    // 1. Uso dos campos com _
    Turma? turma = _repositorioTurma.SelecionarPorId(id);

    if (turma == null)
        return Result.Fail("Turma não encontrada.");

    // 2. Uso dos campos com _
    Curso? curso = _repositorioCurso.SelecionarPorId(turma.CursoId);
    Instrutor? instrutor = _repositorioInstrutor.SelecionarPorId(turma.InstrutorId);

    return Result.Ok(new DetalhesTurmaDto(
        turma.Id,
        turma.Nome,
        curso?.Titulo ?? "Curso não encontrado", // 3. Corrigido para .Titulo
        instrutor?.Nome ?? "Instrutor não encontrado",
        turma.DataInicio,
        turma.DataTermino,
        turma.CapacidadeAlunos
    ));
}
}