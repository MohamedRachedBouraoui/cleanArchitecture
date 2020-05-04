using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Uccspu.Api.Extensions
{
    public static class GestionnaireCors
    {
        private const string UCCSPU_CORS = "uccspu_cors";

        public static IServiceCollection AjouterCors(this IServiceCollection services)
        {

            services.AddCors(opt =>
            {
                opt.AddPolicy(UCCSPU_CORS, policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
            return services;
        }

        public static IApplicationBuilder UtiliserCors(this IApplicationBuilder app)
        {
            app.UseCors(UCCSPU_CORS); //Must be before "UseAuthorization"

            return app;
        }
    }
}