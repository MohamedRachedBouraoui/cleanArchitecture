using System.Collections.Generic;
using Uccspu.Affaires.Communs.Exceptions;

namespace Uccspu.Affaires.Episodes.Requetes
{
    public class EpisodeVm : ReponseApi
    {
        public IReadOnlyList<RecupererEpisodeDto> ListeEpisodes { get; set; }
    }
}
