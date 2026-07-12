using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.WebApp.Compartilhado.Infra.Orm;

namespace EscolaDeCursos.Infra.Modulos.ModuloAluno;

public sealed class RepositorioAlunoEmOrm(EscolaDeCursosDbContext dbContext) : 
    RepositorioBaseEmOrm<Aluno>(dbContext), IRepositorioAluno
{
    public override List<Aluno> SelecionarTodos()
    {
        return registros.OrderBy(a => a.Nome).ToList();
    }

    public override List<Aluno> Filtrar(Func<Aluno, bool> filtro)
    {
        return base.Filtrar(filtro).OrderBy(a => a.Nome).ToList();
    }

    public Aluno? SelecionarPorCpf(string cpf)
    {
        return registros.SingleOrDefault(a => a.Cpf == cpf);
    }
}
