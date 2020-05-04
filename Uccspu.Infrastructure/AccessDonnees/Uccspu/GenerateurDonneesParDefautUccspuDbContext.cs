using System.Linq;
using System.Threading.Tasks;
using Uccspu.Domaine.Entites;

namespace Uccspu.AccessDonnees
{
    public class GenerateurDonneesParDefautUccspuDbContext
    {


        public static async Task AjouterDonneesParDefautAsync(UccspuDbContext context)
        {

            if (context.Episodes.Any())
            {
                return;
            }
            context.Episodes.Add(new Episode
            {
                Libelle = "Episode-1"
            });

            await context.SaveChangesAsync();
        }
    }
}
