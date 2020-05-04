using Microsoft.AspNetCore.Mvc;
using Uccspu.Affaires.Communs.Exceptions;

namespace Uccspu.Api.Controllers
{
    [Route("erreurs/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    // Il faut ajouter 'IgnoreApi' pour empêcher 'Swagger' de documenter ce 'Controller' parceque sinon il va générer une
    // exception comme quoi on a fourni de verbe HTTP pour cette classe or pour nous ce dernier
    // va traiter tous types de requête.  
    public class ErreursController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ReponseApi(code));
        }
    }
}
