using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;

namespace EscolaDeCursos.Servicos.Modulos.ModuloCategoria;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria _repositorio;

    public ServicoCategoria(IRepositorioCategoria repositorio)
    {
        _repositorio = repositorio;
    }

    public void Excluir(Guid id)
    {
        if (_repositorio.ExisteCursoVinculado(id))
            throw new Exception("Não é possível excluir uma categoria que possui cursos vinculados.");

        _repositorio.Excluir(id);
    }
    
    // ... métodos de Cadastrar e Selecionar seguindo o padrão
}