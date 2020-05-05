using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uccspu.AccessDonnees;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Api;
using Uccspu.Domaine.Communs;
using Uccspu.Infrastructure.AccessDonnees.Identity;

namespace Affaire.Tests.Integration
{

    [SetUpFixture]
    public class TestsUtils
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpointUccspu;
        private static Checkpoint _checkpointIdentity;
        public static string _IdUtilisateur;
        private static ServiceCollection _services;

        [OneTimeSetUp]
        public async Task ExecuterUneFoisAvantTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            _services = new ServiceCollection();

            // Utilisé piour la configuration des services Identity 
            _services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "Uccspu.Api"));


            _services.AddLogging();
            startup.ConfigureServices(_services);

            ConfigurerServiceUtilisateurEnCoursPourTests();

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpointIdentity = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };

            _checkpointUccspu = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };


            PreparerBaseDonnees();
        }

        private static void ConfigurerServiceUtilisateurEnCoursPourTests()
        {

            // Remplacer l'implémentation du 'ICurrentUserService'
            // par un moq

            var currentUserServiceDescriptor = _services.FirstOrDefault(d =>
                d.ServiceType == typeof(IUtilisateurEnCoursService));

            _services.Remove(currentUserServiceDescriptor);

            _services.AddTransient(provider => Mock.Of<IUtilisateurEnCoursService>(u => u.IdUtilisateur == _IdUtilisateur));
        }

        private static void PreparerBaseDonnees()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<UccspuDbContext>();

            context.Database.Migrate();

            var contextIdentity = scope.ServiceProvider.GetService<UccspuIdentityDbContext>();

            contextIdentity.Database.Migrate();

        }

        public static async Task<string> ConfigurerUtilisateurParDefautAsync()
        {
            return await ConfigurerUtilisateurParDefaut("test@local", "Testing1234!");
        }

        public static async Task<string> ConfigurerUtilisateurParDefaut(string userName, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<Utilisateur>>();

            var user = new Utilisateur { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            _IdUtilisateur = user.Id;

            return _IdUtilisateur;
        }
        /// <summary>
        /// Executer la méthode 'Send' de MediatR
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<TResponse> MediatRSendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static async Task<TEntity> RecupererParIdAsync<TEntity, TKey>(TKey id)
            where TEntity : Auditable
        {
            using var scope = _scopeFactory.CreateScope();

            IUniteDeTravail uniteDeTavail = scope.ServiceProvider.GetService<IUniteDeTravail>();


            return await uniteDeTavail.Repository<TEntity, TKey>().RecupererParIdAsync(id);
        }
        public static async Task ReinitialiserEtatInitial()
        {
            await _checkpointUccspu.Reset(_configuration.GetConnectionString("DefaultConnection"));
            await _checkpointIdentity.Reset(_configuration.GetConnectionString("IdentityConnection"));
            _IdUtilisateur = null;
        }

        [OneTimeTearDown]
        public void ExecuterALaFinDeTousLesTests()
        {
        }
    }
}
