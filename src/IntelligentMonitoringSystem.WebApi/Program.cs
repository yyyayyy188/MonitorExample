using Coravel;
using IntelligentMonitoringSystem.Application;
using IntelligentMonitoringSystem.Domain;
using IntelligentMonitoringSystem.Infrastructure;
using IntelligentMonitoringSystem.WebApi;
using IntelligentMonitoringSystem.WebApi.Extensions;
using IntelligentMonitoringSystem.WebApi.Infrastructures.Middlewares;
using IntelligentMonitoringSystem.WebApi.Invocable.EventSources;
using IntelligentMonitoringSystem.WebApi.Invocable.PersonnelAccessRecords;
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

    builder.Services.AddWebApi(builder);
    builder.Services.AddApplication();
    builder.Services.AddDomain();
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.UseMiddleware<RequestLogContextMiddleware>();
    app.UseSerilogRequestLogging();
    app.UseExceptionHandler();
    app.UseResponseCaching();
    app.UseHealthChecks("/health");
    app.UseRouting();
    app.UseAuthorization();

    app.Use(next => context =>
    {
        context.Request.EnableBuffering();
        return next(context);
    });

    app.MapControllers();

    var webSocketOptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(10) };
    app.UseWebSockets(webSocketOptions);

    app.Services.UseScheduler(scheduler =>
    {
        scheduler.Schedule<AbnormalPersonnelAccessRecordInvocable>()
            .EveryMinute();
        scheduler.Schedule<EventSourceProcessInvocable>()
            .EveryMinute();
    }).LogScheduledTaskProgress();

    if (!app.Environment.IsProduction())
    {
        app.UseMiniProfiler();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(options =>
        {
            const string routeTemplate = CleanSvcRouteAttribute.SwaggerRouteBase + "/swagger/{documentname}/swagger.json";
            options.RouteTemplate = routeTemplate;
        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/{CleanSvcRouteAttribute.SwaggerRouteBase}/swagger/v1/swagger.json", "智能监控系统 API V1");
            options.RoutePrefix = $"{CleanSvcRouteAttribute.SwaggerRouteBase}/swagger";
        });
    }

    // 执行数据库预热
    await app.PreheatAsync();

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