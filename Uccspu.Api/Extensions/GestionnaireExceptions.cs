using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Exceptions;

namespace Uccspu.Api.Extensions
{
    public class GestionnaireExceptions
    {
        private const int codeErreurInterne = (int)HttpStatusCode.InternalServerError;
        private readonly RequestDelegate _next;
        private readonly ILogger<GestionnaireExceptions> _logger;
        private readonly IHostEnvironment _env;
        public GestionnaireExceptions(RequestDelegate next, ILogger<GestionnaireExceptions> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Si pas d'excéption alors continuer normalement
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = codeErreurInterne;

                string details = ex.StackTrace.ToString();
                var response = _env.IsDevelopment() ?
                new ReponseApi(codeErreurInterne, ex.Message, details) // Mode DEV
                : new ReponseApi(codeErreurInterne); // Mode PROD

                var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await context.Response.WriteAsync(json);


            }
        }
    }
}