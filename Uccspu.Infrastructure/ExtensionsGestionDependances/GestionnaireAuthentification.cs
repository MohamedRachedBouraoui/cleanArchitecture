using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Uccspu.Infrastructure.AccessDonnees.Identity;

namespace Uccspu.Infrastructure.ExtensionsGestionDependances
{
    public static class GestionnaireAuthentification
    {
        public static IServiceCollection AjouterServicesIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigurerIdentity(services, configuration);

            // J ai commenter ce code car nous allons utiliser Office365
             ConfigurerJwt(services,configuration);          

            return services;
        }

        private static void ConfigurerJwt(IServiceCollection services, IConfiguration configuration)
        {
            // SignInManager  Needs this Authentication service to be declared here
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtParams(configuration["Token:Key"], configuration["Token:Issuer"]));
        }

        private static void ConfigurerIdentity(IServiceCollection services, IConfiguration configuration)
        {
            IdentityBuilder builder = services.AddIdentityCore<Utilisateur>();

            builder = new IdentityBuilder(builder.UserType, services);            
            builder.AddEntityFrameworkStores<UccspuIdentityDbContext>();//  Pour créer tables utilisées par IDENTITY
            builder.AddSignInManager<SignInManager<Utilisateur>>();


            // SI ON VA AJOUTER DES ROLES //
            /*
            builder = new IdentityBuilder(builder.UserType, typeof(Role), services);// Pour ajouter les rôles
            builder.AddEntityFrameworkStores<UccspuIdentityDbContext>();//  Pour créer tables utilisées par IDENTITY
            builder.AddSignInManager<SignInManager<Utilisateur>>();
            builder.AddRoleValidator<RoleValidator<Role>>();  // Pour ajouter les rôles
            builder.AddRoleManager<RoleManager<Role>>(); // Pour ajouter les rôles
            */

        }

        private static Action<JwtBearerOptions> JwtParams(string tokenKey, string tokenIssuer)
        {
            return opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateIssuer = true,
                    ValidIssuer = tokenIssuer,
                    //ValidateAudience === Who the token was issued to ?
                    ValidateAudience = false // MUST INIT THIS VALUE (true / false) otherwise the token validation will fail
                };
            };
        }
    }
}