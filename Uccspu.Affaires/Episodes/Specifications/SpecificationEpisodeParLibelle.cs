using Uccspu.Affaires.Communs.Models;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Specifications
{
    public class SpecificationEpisodeParLibelle : Specification<Episode>
    {
        public SpecificationEpisodeParLibelle(string libelle) : base(e => e.Libelle == libelle)
        {

        }
    }
}
