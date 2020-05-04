using Uccspu.Affaires.Communs.Models;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Specifications
{
    public class SpecificationAutreEpisodeAvecMemeLibelle : Specification<Episode>
    {
        public SpecificationAutreEpisodeAvecMemeLibelle(string libelle) : base(e =>e.Libelle == libelle)
        {

        }
    }
}
