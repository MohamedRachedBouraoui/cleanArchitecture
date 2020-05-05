using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Exceptions;
using Uccspu.Affaires.Episodes.Commandes.CreerEpisode;
using Uccspu.Domaine.Entites;

namespace Affaire.Tests.Integration.Episodes.Commandes
{

    using static TestsUtils;
    public class CommandeCreerEpisodeTests : TestBase
    {
        [Test]
        public async Task PreconditionsOk_CreerEpisodeValide()
        {
            var idUtilisateur = await ConfigurerUtilisateurParDefautAsync();

            var nouvelleEpisode = await MediatRSendAsync(new CommandeCreerEpisode
            {
                Libelle = "Nouvelle episode-TEST"
            });

            var episodeCree = await RecupererParIdAsync<Episode, int>(nouvelleEpisode.Id);

            episodeCree.Should().NotBeNull();
            episodeCree.CreePar.Should().Be(idUtilisateur);
            episodeCree.CreeLe.Should().BeCloseTo(DateTime.Now, 10000);
            episodeCree.ModifiePar.Should().BeNull();
            episodeCree.ModifieLe.Should().BeNull();
        }

        [Test]
        public async Task PreconditionsOk_RetournerCodeStatut201()
        {
            var idUtilisateur = await ConfigurerUtilisateurParDefautAsync();

            var nouvelleEpisode = await MediatRSendAsync(new CommandeCreerEpisode
            {
                Libelle = "Nouvelle episode-TEST"
            });

            nouvelleEpisode.Should().NotBeNull();
            nouvelleEpisode.ReponseApiCodeStatut_.Should().Be(201);
        }

        [Test]
        public async Task LibellInvalide_RetournerCodeStatut400EtBonMessageDeValidation()
        {
            var idUtilisateur = await ConfigurerUtilisateurParDefautAsync();

            var nouvelleEpisode = await MediatRSendAsync(new CommandeCreerEpisode());

            nouvelleEpisode.Should().NotBeNull();
            nouvelleEpisode.ReponseApiCodeStatut_.Should().Be(400);            
            nouvelleEpisode.ReponseApiMessage_.Should().Contain("Le libéllé est requis.");
        }

        [Test]
        public async Task UtilisateurNonAuthentifie_RetournerCodeStatut401EtBonMessage()
        {
            var idUtilisateur = await ConfigurerUtilisateurParDefautAsync();

            var nouvelleEpisode = await MediatRSendAsync(new CommandeCreerEpisode
            {
                Libelle = "Nouvelle episode-TEST"
            });

            nouvelleEpisode.Should().NotBeNull();
            nouvelleEpisode.ReponseApiCodeStatut_.Should().Be(401);
            nouvelleEpisode.ReponseApiMessage_.Should().Contain(ReponseApi.NON_AUTORISE);
        }
    }
}
