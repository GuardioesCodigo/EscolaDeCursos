using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace EscolaDeCursos.Infra.Comartilhado.Logging;

public static class SerilogFactory
{
    public static Logger Create(IConfiguration configuration)
    {
        string caminhoAppData = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorio = Path.Combine(caminhoAppData, "EscolaDeCursos");

        Directory.CreateDirectory(caminhoDiretorio);

        string caminhoLogs = Path.Combine(caminhoDiretorio, "erro.log");

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                caminhoLogs,
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Error
            );

        NewRelicOptions newRelicOptions = configuration
            .GetSection(NewRelicOptions.SectionName)
            .Get<NewRelicOptions>() ?? new NewRelicOptions();

        if (string.IsNullOrWhiteSpace(newRelicOptions.LicenseKey))
        {
            throw new InvalidOperationException(
                "A chave de licença do NewRelic não foi configurada. Configure Logging:NewRelic:LicenseKey."
            );
        }

        loggerConfiguration.WriteTo.NewRelicLogs(
            endpointUrl: newRelicOptions.EndpointUrl,
            applicationName: newRelicOptions.ApplicationName,
            licenseKey: newRelicOptions.LicenseKey
        );

        return loggerConfiguration.CreateLogger();
    }
}
