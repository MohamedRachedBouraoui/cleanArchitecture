using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Exceptions;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Commandes.SupprimerEpisode
{
    public class CommandeSupprimerEpisode : IRequest<ReponseApi>
    {
        public int Id { get; set; }

        public class GestionnaireCommandeSupprimerEpisode : IRequestHandler<CommandeSupprimerEpisode, ReponseApi>
        {
            private readonly IUniteDeTravail _uniteDeTravail;
            private readonly IRepository<Episode,int> _episodeRepository;

            public GestionnaireCommandeSupprimerEpisode(IUniteDeTravail uniteDeTravail)
            {
                _uniteDeTravail = uniteDeTravail;
                _episodeRepository = _uniteDeTravail.Repository<Episode,int>();
            }

            public async Task<ReponseApi> Handle(CommandeSupprimerEpisode request, CancellationToken cancellationToken)
            {

                var episodeASupprimer = await _episodeRepository.RecupererParIdAsync(request.Id);

                if (episodeASupprimer == null)
                {
                    return new ReponseApi(404);
                }

                _episodeRepository.Supprimer(episodeASupprimer);


                bool resultat = await _uniteDeTravail.EnregistrerTous(cancellationToken);
                return new ReponseApi(resultat?400:200); 
            }
        }
    }
}
