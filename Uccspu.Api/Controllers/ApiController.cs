using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Uccspu.Affaires.Communs.Exceptions;

namespace Uccspu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult ResultatApi(ReponseApi reponseApi)
        {
            return reponseApi.ReponseApiCodeStatut_ switch
            {
                200 => Ok(reponseApi),
                201 => Ok(reponseApi),
                400 => BadRequest(reponseApi),
                401 => Unauthorized(reponseApi),
                404 => NotFound(reponseApi),
                _ => null
            };
        }
    }
}
