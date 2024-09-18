using Application.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

namespace API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ProjekatContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IKomitentRepository, KomitentRepository>();
            services.AddScoped<IRobaRepository, RobaRepository>();
            services.AddScoped<IDokumentRepository, DokumentRepository>();

            return services;
        }
    }
}
