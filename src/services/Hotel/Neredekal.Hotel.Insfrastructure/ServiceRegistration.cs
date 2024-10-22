using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Insfrastructure.Persistence;
using Neredekal.Hotel.Insfrastructure.Persistence.Repositories;

namespace Neredekal.Hotel.Insfrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HotelDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            #region Repositories

            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IHotelContactInfoRepository, HotelContactInfoRepository>();

            #endregion
        }
    }
}
