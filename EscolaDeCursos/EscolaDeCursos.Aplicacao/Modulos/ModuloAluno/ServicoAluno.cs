using FluentResults;
using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

public class ServicoAluno : ServicoBase<Aluno>
{
    private readonly IRepositorioAluno repositorioAluno;

    public ServicoAluno(IRepositorioAluno repositorioAluno)
    {
        this.repositorioAluno = repositorioAluno;
    }

    public Result Cadastrar(CadastrarAlunoDto dto)
    {
        if (ExisteCpf(dto.Cpf))
            return Falha(nameof(dto.Cpf), "Já existe um aluno com este CPF.");

        Aluno aluno = new Aluno(dto.Nome, dto.Cpf, dto.Email);

        Result resultadoValidacao = ValidarEntidade(aluno);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioAluno.Cadastrar(aluno);

        return Result.Ok();
    }

    public Result Editar(EditarAlunoDto dto)
    {
        if (ExisteCpf(dto.Cpf, dto.Id))
            return Falha(nameof(dto.Cpf), "Já existe um aluno com este CPF.");

        Aluno aluno = new Aluno(dto.Nome, dto.Cpf, dto.Email);

        Result resultadoValidacao = ValidarEntidade(aluno);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioAluno.Editar(dto.Id, aluno);

        if (!conseguiuEditar)
            return Result.Fail("Aluno não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioAluno.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Aluno não encontrado.");

        return Result.Ok();
    }

    public List<ListarAlunosDto> SelecionarTodos()
    {
        return repositorioAluno
            .SelecionarTodos()
            .Select(a => new ListarAlunosDto(a.Id, a.Nome, a.Cpf, a.Email))
            .ToList();
    }

    public Result<DetalhesAlunoDto> SelecionarPorId(Guid id)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(id);

        if (aluno == null)
            return Result.Fail("Aluno não encontrado.");

        return Result.Ok(new DetalhesAlunoDto(
            aluno.Id,
            aluno.Nome,
            aluno.Cpf,
            aluno.Email
        ));
    }

    private bool ExisteCpf(string cpf, Guid? idIgnorar = null)
    {
        return repositorioAluno
            .Filtrar(a => a.Cpf == cpf && (!idIgnorar.HasValue || a.Id != idIgnorar.Value))
            .Any();
    }
}
