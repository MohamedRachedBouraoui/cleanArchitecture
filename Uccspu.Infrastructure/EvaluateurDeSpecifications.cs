using Microsoft.EntityFrameworkCore;
using System.Linq;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Domaine.Communs;

namespace Uccspu.Infrastructure
{
    public class EvaluateurDeSpecifications<T> where T : Auditable
    {
        public static IQueryable<T> Recuperer(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            inputQuery = GererCriteres(inputQuery, spec);
            inputQuery = GererTries(inputQuery, spec);
            inputQuery = GererInclusions(inputQuery, spec);
            inputQuery = GererPagination(inputQuery, spec); // DOIT ETRE AU DERNIER LIEU

            return inputQuery;


        }

        private static IQueryable<T> GererPagination(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if (spec.AvecPagination == true)
            {
                inputQuery = inputQuery.Skip(spec.PagesASauter).Take(spec.UnePageDoitInclure);
            }
            return inputQuery;
        }

        private static IQueryable<T> GererInclusions(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            // inputQuery = spec.Includes.Aggregate(inputQuery, (current, include) => current.Include(include));
            foreach (var include in spec.Inclusions)
            {
                inputQuery = inputQuery.Include(include);
            }

            return inputQuery;
        }

        private static IQueryable<T> GererTries(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if (spec.TriesPar != null)
            {
                inputQuery = inputQuery.OrderBy(spec.TriesPar);
            }

            if (spec.TriesParDesc != null)
            {
                inputQuery = inputQuery.OrderByDescending(spec.TriesParDesc);
            }

            return inputQuery;
        }

        private static IQueryable<T> GererCriteres(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if (spec.Critere != null)
            {
                inputQuery = inputQuery.Where(spec.Critere);
            }

            return inputQuery;
        }
    }
}