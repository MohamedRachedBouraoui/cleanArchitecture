using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Models;

namespace Uccspu.Affaires.Communs.Interfaces
{
    public interface IGestionUtilisateurService
    {
        Task<string> RecupererNomUtilisateurAsync(string idUtilisateur);

        Task<(Resultat Result, string idUtilisateur)> CreerUtilisateurAsync(string nomUtilisateur, string motDePasse);

        Task<Resultat> SupprimerUtilisateurAsync(string idUtilisateur);
    }
}
