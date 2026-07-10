using FluentResults;
using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;

public class ServicoInstrutor : ServicoBase<Instrutor>
{
    private readonly IRepositorioInstrutor repositorioInstrutor;

    public ServicoInstrutor(IRepositorioInstrutor repositorioInstrutor)
    {
        this.repositorioInstrutor = repositorioInstrutor;
    }

    public Result Cadastrar(CadastrarInstrutorDto dto)
    {
        if (ExisteCpf(dto.Email))
            return Falha(nameof(dto.Email), "Já existe um instrutor cadastrado com este CPF.");

        Instrutor instrutor = new Instrutor(dto.Nome, dto.Cpf, dto.Email);

        Result resultadoValidacao = ValidarEntidade(instrutor);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioInstrutor.Cadastrar(instrutor);

        return Result.Ok();
    }

    public Result Editar(EditarInstrutorDto dto)
    {
        if (ExisteCpf(dto.Email, dto.Id))
            return Falha(nameof(dto.Email), "Já existe um instrutor cadastrado com este CPF.");

        Instrutor instrutor = new Instrutor(dto.Nome, dto.Cpf, dto.Email);

        Result resultadoValidacao = ValidarEntidade(instrutor);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioInstrutor.Editar(dto.Id, instrutor);

        if (!conseguiuEditar)
            return Result.Fail("Instrutor não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioInstrutor.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Instrutor não encontrado.");

        return Result.Ok();
    }

    public List<ListarInstrutorDto> SelecionarTodos()
    {
        return repositorioInstrutor
            .SelecionarTodos()
            .Select(i => new ListarInstrutorDto(i.Id, i.Nome, i.Cpf, i.Email))
            .ToList();
    }

    public Result<DetalhesInstrutorDto> SelecionarPorId(Guid id)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(id);

        if (instrutor == null)
            return Result.Fail("Instrutor não encontrado.");

        return Result.Ok(new DetalhesInstrutorDto(
            instrutor.Id,
            instrutor.Nome,
            instrutor.Cpf,
            instrutor.Email
        ));
    }
    private bool ExisteCpf(string cpf, Guid? idIgnorar = null)
    {
        return repositorioInstrutor
            .SelecionarTodos()
            .Any(a => a.Cpf == cpf && (!idIgnorar.HasValue || a.Id != idIgnorar.Value));
    }
}
