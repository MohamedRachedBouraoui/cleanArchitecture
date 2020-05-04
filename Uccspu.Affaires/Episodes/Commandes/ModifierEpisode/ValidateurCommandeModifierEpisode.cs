using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Affaires.Episodes.Specifications;
using Uccspu.Domaine.Entites;

namespace Uccspu.Affaires.Episodes.Commandes.ModifierEpisode
{
    public class ValidateurCommandeModifierEpisode : AbstractValidator<CommandeModifierEpisode>
    {
        private readonly IUniteDeTravail uniteDeTravail;

        public ValidateurCommandeModifierEpisode(IUniteDeTravail uniteDeTravail)
        {            
            this.uniteDeTravail = uniteDeTravail;
            RuleFor(v => v.Libelle)
               .NotEmpty().WithMessage("Le libéllé est requis.")
               .MaximumLength(5).WithMessage("Le libéllé ne doit pas dépasser 5 caractères.")
               .MustAsync(LibelleDoitEtreUnique).WithMessage("Le libéllé indiqué existe déjà.");
        }

        private async Task<bool> LibelleDoitEtreUnique(CommandeModifierEpisode model, string libelle, CancellationToken cancellationToken)
        {
            var episode = await uniteDeTravail.Repository<Episode,int>().RecupererAsync(new SpecificationAutreEpisodeAvecMemeLibelle(libelle));
            return episode == null;
        }
    }
}
