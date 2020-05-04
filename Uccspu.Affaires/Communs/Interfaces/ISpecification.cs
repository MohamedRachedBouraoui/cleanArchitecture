using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Uccspu.Affaires.Communs.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Critere { get; } // Les critères de rechèrches
        List<Expression<Func<T, object>>> Inclusions { get; } // Les entités filles à inclure

        Expression<Func<T, object>> TriesPar { get; }
        Expression<Func<T, object>> TriesParDesc { get; }

        bool AvecPagination { get; } // Si La liste de retour sera utilisée pour la pagination par le client
        int PagesASauter { get; } // le nbre de pages à sauter
        int UnePageDoitInclure { get; } // Combien de lignes par page
    }
}
