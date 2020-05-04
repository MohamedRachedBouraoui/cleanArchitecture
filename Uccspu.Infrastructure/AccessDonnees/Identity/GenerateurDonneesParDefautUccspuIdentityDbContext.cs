using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Uccspu.Infrastructure.AccessDonnees.Identity
{
    public class GenerateurDonneesParDefautUccspuIdentityDbContext
    {
        public static async Task AjouterUtilisateursParDefautAsync(UserManager<Utilisateur> userManager)
        {
            var defaultUser = new Utilisateur { UserName = "administrateur@uccspu", Email = "administrateur@uccspu" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "!Ucc$pu!");
            }
        }
    }
}
