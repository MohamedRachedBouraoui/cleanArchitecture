using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Attributs;
using Uccspu.Affaires.Communs.Exceptions;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Commandes.CreerEpisode
{
    [Authentifier]
    public class CommandeCreerEpisode : ReponseApi, IRequest<CreerEpisodeDto>
    {
       // public int Id { get; set; }
        public string Libelle { get; set; }
    }
    public class GestionnaireCommandeCreerEpisode : IRequestHandler<CommandeCreerEpisode, CreerEpisodeDto>
    {
        private readonly IMapper _mapper;
        private readonly IUniteDeTravail _uniteDeTravail;
        private readonly IRepository<Episode,int> _episodeRepository;
        public GestionnaireCommandeCreerEpisode(IUniteDeTravail uniteDeTravail, IMapper mapper)
        {
            _mapper = mapper;
            _uniteDeTravail = uniteDeTravail;
            _episodeRepository = _uniteDeTravail.Repository<Episode,int>();
        }

        public async Task<CreerEpisodeDto> Handle(CommandeCreerEpisode request, CancellationToken cancellationToken)
        {
            var episode = new Episode
            {
                Libelle = request.Libelle
            };

            _episodeRepository.Ajouter(episode);

            await _uniteDeTravail.EnregistrerTousAsync(cancellationToken);

            CreerEpisodeDto creerEpisodeDto = _mapper.Map<CreerEpisodeDto>(episode);
            creerEpisodeDto.ReponseApiCodeStatut_ = 201;
            return creerEpisodeDto;
        }
    }
}
