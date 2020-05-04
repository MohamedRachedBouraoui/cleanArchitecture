using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Interfaces;

namespace Uccspu.Affaires.Communs.Comportements
{
    public class ComportementLogging<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IUtilisateurEnCoursService _utilisateurEnCoursService;
        private readonly IGestionUtilisateurService _identityService;

        public ComportementLogging(ILogger<TRequest> logger, IUtilisateurEnCoursService utilisateurEnCoursService, IGestionUtilisateurService identityService)
        {
            _logger = logger;
            _utilisateurEnCoursService = utilisateurEnCoursService;
            _identityService = identityService;
        }

        public async Task Process(TRequest requete, CancellationToken cancellationToken)
        {
            var nomRequete = typeof(TRequest).Name;
            var idUtilisateur = _utilisateurEnCoursService.IdUtilisateur ?? string.Empty;
            string nomUtilisateur = string.Empty;

            if (!string.IsNullOrEmpty(idUtilisateur))
            {
                nomUtilisateur = await _identityService.RecupererNomUtilisateurAsync(idUtilisateur);
            }

            _logger.LogInformation($"Nouvelle Requête Http: {nomRequete} {idUtilisateur} {nomUtilisateur} {requete}");
        }
    }
}
