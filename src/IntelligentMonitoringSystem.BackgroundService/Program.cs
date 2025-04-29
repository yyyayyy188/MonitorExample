using Coravel;
using IntelligentMonitoringSystem.Application;
using IntelligentMonitoringSystem.BackgroundService;
using IntelligentMonitoringSystem.BackgroundService.Invocable.EventSources;
using IntelligentMonitoringSystem.Domain;
using IntelligentMonitoringSystem.Infrastructure;
using Serilog;
using Winton.Extensions.Configuration.Consul;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up!");
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddConsul($"dotnet-config/appsettings.{builder.Environment.EnvironmentName.ToLower()}.json", op =>
    {
        op.ConsulConfigurationOptions = cco =>
        {
            cco.Address = new Uri(builder.Configuration["Consul:Address"] ?? string.Empty);
            cco.Token = builder.Configuration["Consul:Token"];
        };
        op.OnLoadException = ex =>
        {
            if (ex?.Exception.Message != null)
            {
                Log.Error(ex.Exception.Message);
            }
        };
        op.ReloadOnChange = true;
    });
    builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());
    builder.Services.AddBackgroundService(builder);
    builder.Services.AddApplication();
    builder.Services.AddDomain();
    builder.Services.AddInfrastructure(builder.Configuration);
    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.UseHealthChecks("/health");
    app.UseRouting();

    app.Use(next => context =>
    {
        context.Request.EnableBuffering();
        return next(context);
    });

    app.MapControllers();
    app.Services.UseScheduler(scheduler =>
    {
        scheduler.Schedule<EventSourceProcessInvocable>()
            .EveryTenSeconds();
    }).LogScheduledTaskProgress();
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
}
finally
{
    await Log.CloseAndFlushAsync();
}