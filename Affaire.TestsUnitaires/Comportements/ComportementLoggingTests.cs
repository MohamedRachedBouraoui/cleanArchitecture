using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Comportements;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Affaires.Episodes.Commandes.CreerEpisode;

namespace Affaire.TestsUnitaires.Comportements
{
    public class ComportementLoggingTests
    {
        private readonly Mock<ILogger<CommandeCreerEpisode>> _logger;
        private readonly Mock<IUtilisateurEnCoursService> _utilisateurEnCoursService;
        private readonly Mock<IGestionUtilisateurService> _identityService;


        public ComportementLoggingTests()
        {
            _logger = new Mock<ILogger<CommandeCreerEpisode>>();

            _utilisateurEnCoursService = new Mock<IUtilisateurEnCoursService>();

            _identityService = new Mock<IGestionUtilisateurService>();
        }

        [Test]
        public async Task DevraitAppelerRecupererNomUtilisateurAsyncUneSeuleFoisSiAuthentifie()
        {
            _utilisateurEnCoursService.Setup(x => x.IdUtilisateur).Returns("Administrator");

            var requestLogger = new ComportementLogging<CommandeCreerEpisode>(_logger.Object, _utilisateurEnCoursService.Object, _identityService.Object);

            await requestLogger.Process(new CommandeCreerEpisode { Libelle = "Nouvelle Episode" }, new CancellationToken());

            _identityService.Verify(i => i.RecupererNomUtilisateurAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task DevraitNePasAppelerRecupererNomUtilisateurAsyncSiNonAuthentifie()
        {
            var requestLogger = new ComportementLogging<CommandeCreerEpisode>(_logger.Object, _utilisateurEnCoursService.Object, _identityService.Object);

            await requestLogger.Process(new CommandeCreerEpisode { Libelle = "Nouvelle Episode" }, new CancellationToken());

            _identityService.Verify(i => i.RecupererNomUtilisateurAsync(null), Times.Never);
        }
    }
}