using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloCategoria;

public interface IRepositorioCategoria : IRepositorio<Categoria>
{
    bool ExisteCursoVinculado(Guid categoriaId);
}