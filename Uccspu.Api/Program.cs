using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Uccspu.AccessDonnees;
using Uccspu.Infrastructure.AccessDonnees.Identity;
using Uccspu.Infrastructure.Identity;

namespace Uccspu.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            await AjouterDonneesParDefautsDansBDAsync(host).ConfigureAwait(false);
            await host.RunAsync().ConfigureAwait(false);
        }

        private static async Task AjouterDonneesParDefautsDansBDAsync(IWebHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();

            IServiceProvider services = scope.ServiceProvider;

            try
            {
                await AjouterDonneesUccspu(services).ConfigureAwait(false);
                await AjouterDonneesIdentity(services).ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Une erreur s'est produite lors de l'ajout des données par défauts.");
            }
        }

        private static async Task AjouterDonneesIdentity(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<Utilisateur>>();
            var identityContext = services.GetRequiredService<UccspuIdentityDbContext>();
            await identityContext.Database.MigrateAsync().ConfigureAwait(false);
            await GenerateurDonneesParDefautUccspuIdentityDbContext.AjouterUtilisateursParDefautAsync(userManager).ConfigureAwait(false);
        }

        private static async Task AjouterDonneesUccspu(IServiceProvider services)
        {
            var uccspuDbContext = services.GetRequiredService<UccspuDbContext>();
            await uccspuDbContext.Database.MigrateAsync().ConfigureAwait(false);
            await GenerateurDonneesParDefautUccspuDbContext.AjouterDonneesParDefautAsync(uccspuDbContext).ConfigureAwait(false);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   var env = hostingContext.HostingEnvironment;

                   config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true);

                   if (env.IsDevelopment())
                   {
                       var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                       if (appAssembly != null)
                       {
                           config.AddUserSecrets(appAssembly, optional: true);
                       }
                   }

                   config.AddEnvironmentVariables();

                   if (args != null)
                   {
                       config.AddCommandLine(args);
                   }
               })
               .UseStartup<Startup>();

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
