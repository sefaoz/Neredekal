using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neredekal.Rapor.Application.Abstractions.Repositories;
using Neredekal.Rapor.Infrastructure.Persistence;
using Neredekal.Rapor.Infrastructure.Persistence.Repositories;
using Neredekal.Rapor.OutboxWorker;

internal class Program
{
    public static async Task Main(string[] args)
    {
        IHostBuilder builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<ReportDbContext>(options =>
                {
                    options.UseNpgsql("");
                });

                services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
                services.AddHostedService<OutboxWorkerBackgroundService>();
                services.AddMediatR(configuration =>
                {
                    configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                });

                services.AddMassTransit(busConfigurator =>
                {
                    busConfigurator.UsingRabbitMq((context, configure) =>
                    {
                        var host = "localhost";
                        var username = "guest";
                        var password = "guest";
                        configure.Host(new Uri(host!), hostConfigurator =>
                        {
                            hostConfigurator.Username(username);
                            hostConfigurator.Password(password);
                        });

                        configure.ConfigureEndpoints(context);
                    });
                });
            })
            .UseWindowsService();

        await builder.Build().RunAsync();
    }
}