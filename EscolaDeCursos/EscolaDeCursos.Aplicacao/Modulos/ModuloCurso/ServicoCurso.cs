using FluentResults;
using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

public class ServicoCurso : ServicoBase<Curso>
{
    private readonly IRepositorioCurso repositorioCurso;

    public ServicoCurso(IRepositorioCurso repositorioCurso)
    {
        this.repositorioCurso = repositorioCurso;
    }

    public Result Cadastrar(CadastrarCursoDto dto)
    {
        Curso curso = new Curso(dto.Titulo, dto.Descricao, dto.CargaHoraria, dto.Nivel, dto.CategoriaId);

        Result resultadoValidacao = ValidarEntidade(curso);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCurso.Cadastrar(curso);

        return Result.Ok();
    }

    public Result Editar(EditarCursoDto dto)
    {
        Curso curso = new Curso(dto.Titulo, dto.Descricao, dto.CargaHoraria, dto.Nivel, dto.CategoriaId);

        Result resultadoValidacao = ValidarEntidade(curso);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioCurso.Editar(dto.Id, curso);

        if (!conseguiuEditar)
            return Result.Fail("Curso não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioCurso.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Curso não encontrado.");

        return Result.Ok();
    }

    public List<ListarCursoDto> SelecionarTodos()
    {
        return repositorioCurso
            .SelecionarTodos()
            .Select(c => new ListarCursoDto(c.Id, c.Titulo, c.Descricao, c.Nivel))
            .ToList();
    }

    public Result<DetalhesCursoDto> SelecionarPorId(Guid id)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(id);

        if (curso == null)
            return Result.Fail("Curso não encontrado.");

        return Result.Ok(new DetalhesCursoDto(
            curso.Id,
            curso.Titulo,
            curso.Descricao,
            curso.CargaHoraria,
            curso.Nivel,
            curso.CategoriaId
        ));
    }
}
