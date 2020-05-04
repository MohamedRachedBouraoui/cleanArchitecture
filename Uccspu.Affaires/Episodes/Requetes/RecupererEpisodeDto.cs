using Uccspu.Affaires.Communs.Mappings;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Requetes
{
    public class RecupererEpisodeDto : IMapFrom<Episode>
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
    }
}
