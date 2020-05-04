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
        //[Test]
        //public void DevraitGenererExceptionValidation()
        //{
        //    var commande = new CommandeCreerEpisode();
        //    FluentActions.Invoking(() => SendAsync(commande)).Should().Throw<ExceptionDeValidation>();
        //}

        [Test]
        public async Task DevraitCreerUneEpisode()
        {
            var idUtilisateur = await RunAsDefaultUserAsync();

            var nouvelleEpisode = await SendAsync(new CommandeCreerEpisode
            {
                Libelle = "Nouvelle episode"
            });

            var episodeCree = await FindAsync<Episode>(nouvelleEpisode.Id);

            episodeCree.Should().NotBeNull();
            episodeCree.CreePar.Should().Be(idUtilisateur);
            episodeCree.CreeLe.Should().BeCloseTo(DateTime.Now, 10000);
            episodeCree.ModifiePar.Should().BeNull();
            episodeCree.ModifieLe.Should().BeNull();




        }
    }
}
