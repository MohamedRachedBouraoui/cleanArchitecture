using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uccspu.AccessDonnees;
using Uccspu.Infrastructure.AccessDonnees.Identity;

namespace Uccspu.Infrastructure.ExtensionsGestionDependances
{
    public static class GestionnaireContextesBd
    {
        public static IServiceCollection AjouterContextesBd(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UtiliserBDEnMemoire")) // pour les tests
            {
                services.AddDbContext<UccspuDbContext>(options =>
                    options.UseInMemoryDatabase("uccspuDb"));
            }
            else
            {
                services.AddDbContext<UccspuDbContext>(options =>
                              options.UseSqlServer(
                                  configuration.GetConnectionString("DefaultConnection"),
                                  b => b.MigrationsAssembly(typeof(UccspuDbContext).Assembly.FullName)));

                services.AddScoped<UccspuDbContext>();
                services.AddHealthChecks().AddDbContextCheck<UccspuDbContext>();

                services.AddDbContext<UccspuIdentityDbContext>(options =>
                      options.UseSqlServer(
                          configuration.GetConnectionString("IdentityConnection"),
                          b => b.MigrationsAssembly(typeof(UccspuIdentityDbContext).Assembly.FullName)));

                services.AddHealthChecks().AddDbContextCheck<UccspuIdentityDbContext>();
            }
            

            return services;
        }
    }
}
