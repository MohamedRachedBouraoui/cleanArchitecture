using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Uccspu.Affaires.Communs.Interfaces;

namespace Uccspu.Api.Services
{
    public class UtilisateurEnCoursService : IUtilisateurEnCoursService
    {
        public UtilisateurEnCoursService()
        {

        }
        public UtilisateurEnCoursService(IHttpContextAccessor httpContextAccessor)
        {
            IdUtilisateur = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        public string IdUtilisateur { get; set; }
    }
}
