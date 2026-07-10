using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentosWeb.Infra.Compartilhado.Sql;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}

public sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }
}