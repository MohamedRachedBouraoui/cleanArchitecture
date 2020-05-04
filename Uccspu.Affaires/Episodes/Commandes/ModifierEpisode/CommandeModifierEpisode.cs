using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Commandes.ModifierEpisode
{
    public class CommandeModifierEpisode : IRequest<ModifierEpisodeDto>
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
    }

    public class GestionnaireCommandeModifierEpisode : IRequestHandler<CommandeModifierEpisode, ModifierEpisodeDto>
    {
        private readonly IMapper _mapper;
        private readonly IUniteDeTravail _uniteDeTravail;
        private readonly IRepository<Episode,int> _episodeRepository;
        public GestionnaireCommandeModifierEpisode(IUniteDeTravail uniteDeTravail, IMapper mapper)
        {
            _mapper = mapper;
            _uniteDeTravail = uniteDeTravail;
            _episodeRepository = _uniteDeTravail.Repository<Episode,int>();
        }
        public async Task<ModifierEpisodeDto> Handle(CommandeModifierEpisode request, CancellationToken cancellationToken)
        {
            var episodeAModifier = await _episodeRepository.RecupererParIdAsync(request.Id);

            if (episodeAModifier == null)
            {
                return new ModifierEpisodeDto
                {
                    ReponseApiCodeStatut_ = 400
                };
            }

            episodeAModifier.Libelle = request.Libelle;
            _episodeRepository.Modifier(episodeAModifier);


            await _uniteDeTravail.EnregistrerTous(cancellationToken);
            ModifierEpisodeDto modifierEpisodeDto = _mapper.Map<ModifierEpisodeDto>(episodeAModifier);
            modifierEpisodeDto.ReponseApiCodeStatut_ = 200;
            return modifierEpisodeDto;
        }
    }
}
