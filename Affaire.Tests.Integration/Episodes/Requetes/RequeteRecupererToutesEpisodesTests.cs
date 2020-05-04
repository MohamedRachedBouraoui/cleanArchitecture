using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using Uccspu.Affaires.Episodes.Requetes;

namespace Affaire.Tests.Integration.Episodes.Requetes
{
    using static TestsUtils;
    public class RequeteRecupererToutesEpisodesTests:TestBase
    {
        [Test]
        public async Task DevraitRetournerAuMoinsUneEpisode()
        {
            var requete = new RequeteRecupererToutesEpisodes();
            var episodes = await SendAsync(requete);

            episodes.Should().NotBeNull();
        }
    }
}
