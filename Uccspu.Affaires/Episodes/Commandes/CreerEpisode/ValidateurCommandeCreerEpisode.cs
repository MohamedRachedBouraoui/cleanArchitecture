using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Affaires.Episodes.Specifications;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Commandes.CreerEpisode
{
    public class ValidateurCommandeCreerEpisode : AbstractValidator<CommandeCreerEpisode>
    {
        private readonly IUniteDeTravail uniteDeTravail;

        public ValidateurCommandeCreerEpisode(IUniteDeTravail uniteDeTravail)
        {
            this.uniteDeTravail = uniteDeTravail;
            RuleFor(v => v.Libelle)
               .NotEmpty().WithMessage("Le libéllé est requis.")
               .MaximumLength(200).WithMessage("Le libéllé ne doit pas dépasser 200 caractères.")
               .MustAsync(LibelleDoitEtreUnique).WithMessage("Le libéllé indiqué existe déjà.");
        }

        private async Task<bool> LibelleDoitEtreUnique(string libelle, CancellationToken cancellationToken)
        {
            var episode = await uniteDeTravail.Repository<Episode,int>().RecupererAsync(new SpecificationEpisodeParLibelle(libelle));
            return episode == null;
        }
    }
}
