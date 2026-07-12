using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.WebApp.Compartilhado.Infra.Orm;

namespace EscolaDeCursos.Infra.Modulos.ModuloInstrutor;

public class RepositorioInstrutorEmOrm(EscolaDeCursosDbContext dbContext)
    : RepositorioBaseEmOrm<Instrutor>(dbContext), IRepositorioInstrutor
{
    public override List<Instrutor> SelecionarTodos()
    {
        return registros
            .OrderBy(i => i.Nome)
            .ToList();
    }

    public override List<Instrutor> Filtrar(Func<Instrutor, bool> filtro)
    {
        return registros
            .Where(filtro)
            .OrderBy(i => i.Nome)
            .ToList();
    }

    
}
