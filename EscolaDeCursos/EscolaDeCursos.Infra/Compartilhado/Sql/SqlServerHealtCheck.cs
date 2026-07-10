using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ControleDeMedicamentosWeb.Infra.Compartilhado.Sql;

public sealed class SqlServerHealthCheck : IHealthCheck
{
    private readonly ISqlConnectionFactory connectionFactory;

    public SqlServerHealthCheck(ISqlConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            using SqlConnection connection = connectionFactory.CreateConnection();
            await connection.OpenAsync(cancellationToken);

            using SqlCommand command = new("SELECT 1", connection);
            await command.ExecuteScalarAsync(cancellationToken);

            return HealthCheckResult.Healthy("SQL Server está disponível.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("SQL Server indisponível.", ex);
        }
    }
}