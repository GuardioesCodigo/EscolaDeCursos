using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.WebApp.Compartilhado.Infra.Orm;

public abstract class RepositorioBaseEmOrm<T>(EscolaDeCursosDbContext dbContext) where T : EntidadeBase<T>
{
    protected readonly DbSet<T> registros = dbContext.Set<T>();

    public void Cadastrar(T entidade)
    {
        registros.Add(entidade);

        dbContext.SaveChanges();
    }

    public bool Editar(Guid idSelecionado, T entidadeAtualizada)
    {
        T? registroSelecionado = SelecionarPorId(idSelecionado);

        if (registroSelecionado == null)
            return false;

        registroSelecionado.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        T? registroSelecionado = SelecionarPorId(idSelecionado);

        if (registroSelecionado == null)
            return false;

        registros.Remove(registroSelecionado);

        dbContext.SaveChanges();

        return true;
    }

    public virtual T? SelecionarPorId(Guid idSelecionado)
    {
        return registros.SingleOrDefault(c => c.Id == idSelecionado);
    }

    public virtual List<T> SelecionarTodos()
    {
        return registros.ToList();
    }

    public virtual List<T> Filtrar(Func<T, bool> filtro)
    {
        return registros.Where(filtro).ToList();
    }
}