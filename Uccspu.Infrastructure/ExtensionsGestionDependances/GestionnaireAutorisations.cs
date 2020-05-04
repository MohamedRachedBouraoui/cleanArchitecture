using Microsoft.Extensions.DependencyInjection;

namespace Uccspu.Infrastructure.ExtensionsGestionDependances
{
    public static class GestionnaireAutorisations
    {
        public static IServiceCollection AjouterAutorisations(this IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Nom de la Policy", policy => policy.RequireRole("NOMS DES ROLES SEPARES PAR ','"));               
            });

            return services;
        }
    }
}