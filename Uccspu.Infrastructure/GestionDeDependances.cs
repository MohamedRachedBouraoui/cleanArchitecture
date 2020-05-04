using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Infrastructure.AccessDonnees;
using Uccspu.Infrastructure.ExtensionsGestionDependances;
using Uccspu.Infrastructure.Identity;

namespace Uccspu.Infrastructure
{
    public static class GestionDeDependances
    {
        public static IServiceCollection AjouterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AjouterContextesBd(configuration);
            services.AjouterServicesIdentity(configuration);
            services.AddScoped<IUniteDeTravail, UniteDeTravail>();
            services.AddTransient<IGestionUtilisateurService, GestionUtilisateurService>();
            return services;
        }
    }
}
