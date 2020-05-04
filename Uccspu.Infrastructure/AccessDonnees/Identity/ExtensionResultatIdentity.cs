using Microsoft.AspNetCore.Identity;
using System.Linq;
using Uccspu.Affaires.Communs.Models;

namespace Uccspu.Infrastructure.Identity
{
    public static class ExtensionResultatIdentity
    {
        public static Resultat ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Resultat.Success()
                : Resultat.Echec(result.Errors.Select(e => e.Description));
        }
    }
}