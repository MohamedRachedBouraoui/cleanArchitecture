using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Affaires.Communs.Models;
using Uccspu.Infrastructure.AccessDonnees.Identity;

namespace Uccspu.Infrastructure.Identity
{
    public class GestionUtilisateurService : IGestionUtilisateurService
    {
        private readonly UserManager<Utilisateur> _userManager;

        public GestionUtilisateurService(UserManager<Utilisateur> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(Resultat Result, string idUtilisateur)> CreerUtilisateurAsync(string nomUtilisateur, string motDePasse)
        {
            var user = new Utilisateur
            {
                UserName = nomUtilisateur,
                Email = nomUtilisateur,
            };

            var result = await _userManager.CreateAsync(user, motDePasse);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<string> RecupererNomUtilisateurAsync(string idUtilisateur)
        {
            var utilisateur = await _userManager.Users.FirstAsync(u => u.Id == idUtilisateur);

            return utilisateur.UserName;
        }

        public async Task<Resultat> SupprimerUtilisateurAsync(string idUtilisateur)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == idUtilisateur);

            if (user != null)
            {
                return await SupprimerUtilisateurAsync(user);
            }

            return Resultat.Success();
        }

        public async Task<Resultat> SupprimerUtilisateurAsync(Utilisateur user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
