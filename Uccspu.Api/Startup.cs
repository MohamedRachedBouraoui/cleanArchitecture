using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uccspu.Affaires;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Api.Extensions;
using Uccspu.Api.Services;
using Uccspu.Infrastructure;

namespace Uccspu.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUtilisateurEnCoursService, UtilisateurEnCoursService>();

            services.AjouterAffaires();
            services.AjouterInfrastructure(Configuration);
            services.AddHttpContextAccessor();
            services.AjouterCors();
            services.AjouterSwaggerDoc();

            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling =
                 Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });


            // DOIT ETRE APRES "AddControllers"
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<GestionnaireExceptions>();

            // Si l'utilisateur demande des 'Endpoints' non existantes alors passer le code 404 à  "/errors/404"
            // pour lui retouner une réponse formattée par notre "ReponseApi"            
            app.UseStatusCodePagesWithReExecute("/erreurs/{0}");

            app.UseHealthChecks("/health"); // ?????

            app.UseHttpsRedirection();

            app.UtiliserSwaggerDoc();

            app.UseRouting();

            app.UtiliserCors(); // DOIT ÊTRE AVANT "UseAuthorization"

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
