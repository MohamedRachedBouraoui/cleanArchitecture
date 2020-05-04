using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Exceptions;
using Uccspu.Affaires.Episodes.Commandes.CreerEpisode;
using Uccspu.Affaires.Episodes.Commandes.ModifierEpisode;
using Uccspu.Affaires.Episodes.Commandes.SupprimerEpisode;
using Uccspu.Affaires.Episodes.Requetes;

namespace Uccspu.Api.Controllers
{
    // [Authorize]
    public class EpisodesController : ApiController
    {
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EpisodeVm>> RecupererToutesEpisodes()
        {
            var result = await Mediator.Send(new RequeteRecupererToutesEpisodes());
            return ResultatApi(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreerEpisodeDto>> CreerEpisode(CommandeCreerEpisode commandeCreerEpisode)
        {
            var result = await Mediator.Send(commandeCreerEpisode);
            return ResultatApi(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ModifierEpisodeDto>> ModifierEpisode(int id, CommandeModifierEpisode commande)
        {
            if (id != commande.Id)
            {
                return ResultatApi(new ReponseApi { ReponseApiCodeStatut_ = 400 });
            }

            var result = await Mediator.Send(commande);
            return ResultatApi(result);
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReponseApi>> SupprimerEpisode(int id)
        {
            var result = await Mediator.Send(new CommandeSupprimerEpisode { Id = id });
            return ResultatApi(result);
        }
    }
}
