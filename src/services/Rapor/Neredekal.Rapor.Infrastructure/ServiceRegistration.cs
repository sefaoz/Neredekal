using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neredekal.Rapor.Application.Abstractions.Repositories;
using Neredekal.Rapor.Infrastructure.Persistence;
using Neredekal.Rapor.Infrastructure.Persistence.Repositories;

namespace Neredekal.Rapor.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReportDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            #region Repositories

            services.AddScoped<IReportDetailRepository, ReportDetailRepository>();
            services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();

            #endregion
        }
    }
}
