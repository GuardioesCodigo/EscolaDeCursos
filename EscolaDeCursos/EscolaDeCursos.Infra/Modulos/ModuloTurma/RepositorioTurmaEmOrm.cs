using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.WebApp.Compartilhado.Infra.Orm;
using System.Linq.Expressions;

namespace EscolaDeCursos.Infraestrutura.Orm.Modulos.ModuloTurma;

public sealed class RepositorioTurmaEmOrm(EscolaDeCursosDbContext dbContext) 
    : RepositorioBaseEmOrm<Turma>(dbContext), IRepositorioTurma
{
    public override List<Turma> SelecionarTodos()
    {
        return registros
            .OrderBy(t => t.Nome)
            .ToList();
    }

    public override List<Turma> Filtrar(Func<Turma, bool> filtro)
    {
        return registros
            .Where(filtro)
            .OrderBy(t => t.Nome)
            .ToList();
    }
}