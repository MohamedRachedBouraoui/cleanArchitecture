using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Requetes
{
    public class RequeteRecupererToutesEpisodes : IRequest<EpisodeVm>
    {
    }

    public class GestionnaireRequeteRecupererToutesEpisodes : IRequestHandler<RequeteRecupererToutesEpisodes, EpisodeVm>
    {
        private readonly IUniteDeTravail _uniteDeTravail;
        private readonly IRepository<Episode,int> _episodeRepository;

        public GestionnaireRequeteRecupererToutesEpisodes(IUniteDeTravail uniteDeTravail)
        {
            _uniteDeTravail = uniteDeTravail;
            _episodeRepository = _uniteDeTravail.Repository<Episode,int>();
        }
        public async Task<EpisodeVm> Handle(RequeteRecupererToutesEpisodes request, CancellationToken cancellationToken)
        {
            var episodesDto = await _episodeRepository.RecupererListeAsync<RecupererEpisodeDto>();

            return new EpisodeVm
            {
                ReponseApiCodeStatut_=200,
                ListeEpisodes = episodesDto
                
            };
        }
    }
}
