using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Interfaces;

namespace Uccspu.Affaires.Communs.Comportements
{
    public class ComportementPerformance<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IUtilisateurEnCoursService _utilisateurEnCoursService;
        private readonly IGestionUtilisateurService _identityService;

        public ComportementPerformance(
            ILogger<TRequest> logger,
            IUtilisateurEnCoursService utilisateurEnCoursService,
            IGestionUtilisateurService identityService)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _utilisateurEnCoursService = utilisateurEnCoursService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var nomRequete = typeof(TRequest).Name;
                var idUtilisateur = _utilisateurEnCoursService.IdUtilisateur ?? string.Empty;
                var nomUtilisateur = string.Empty;

                if (!string.IsNullOrEmpty(idUtilisateur))
                {
                    nomUtilisateur = await _identityService.RecupererNomUtilisateurAsync(idUtilisateur);
                }

                _logger.LogWarning($"Problème performance - Rêquète lente: {nomRequete} ({elapsedMilliseconds} milliseconds) {idUtilisateur} {nomUtilisateur} {request}");
            }

            return response;
        }
    }
}
