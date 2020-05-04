using Uccspu.Affaires.Communs.Exceptions;
using Uccspu.Affaires.Communs.Mappings;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Commandes.CreerEpisode
{
    public class CreerEpisodeDto : ReponseApi, IMapFrom<Episode>
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
    }
}
