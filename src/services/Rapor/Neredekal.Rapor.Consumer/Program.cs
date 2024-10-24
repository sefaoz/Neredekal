using Neredekal.Rapor.Consumer.Services;
using Polly;
using Neredekal.Rapor.Infrastructure;
using Neredekal.Rapor.Application;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;
using MassTransit;
using Neredekal.Rapor.Consumer.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog();
ConfigureLogging();
ConfigureElasticSink(builder.Configuration, builder.Environment.EnvironmentName);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationService();

builder.Services.AddMassTransit(configuration => {
    configuration.AddConsumer<ReportDetailCreatedIntegrationEventConsumer>();

    configuration.UsingRabbitMq((context, configure) =>
    {
        var host = "amqp://host.docker.internal:5672";
        var username = "guest";
        var password = "guest";
        configure.Host(new Uri(host!), hostConfigurator =>
        {
            hostConfigurator.Username(username);
            hostConfigurator.Password(password);
        });

        configure.ReceiveEndpoint("report-detail-created-integration-event-queue", e => {
            e.ConfigureConsumer<ReportDetailCreatedIntegrationEventConsumer>(context);

            e.UseRetry(retryConfig =>
            {
                retryConfig.Interval(3, TimeSpan.FromSeconds(5));
            });
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddHttpClient<IHotelService, HotelService>(client =>
{
    client.BaseAddress = new Uri("http://host.docker.internal:5001/api/Hotel/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddTransientHttpErrorPolicy(policyBuilder =>
    policyBuilder.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
.AddTransientHttpErrorPolicy(policyBuilder =>
    policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();



static void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Debug()
        .WriteTo.Console()
.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day).WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}