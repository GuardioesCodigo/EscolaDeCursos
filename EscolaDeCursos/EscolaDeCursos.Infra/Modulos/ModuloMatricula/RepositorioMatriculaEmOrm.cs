using System;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.WebApp.Compartilhado.Infra.Orm;

namespace EscolaDeCursos.Infra.Modulos.ModuloMatricula;

public sealed class RepositorioMatriculaEmOrm(EscolaDeCursosDbContext dbContext) 
    : RepositorioBaseEmOrm<Matricula>(dbContext), IRepositorioMatricula

{
    public override List<Matricula> SelecionarTodos()
    {
        return registros
            .OrderByDescending(m => m.DataMatricula)
            .ToList();
    }

    public override List<Matricula> Filtrar(Func<Matricula, bool> filtro)
    {
        return registros
            .Where(filtro)
            .OrderByDescending(m => m.DataMatricula)
            .ToList();
    }
}
