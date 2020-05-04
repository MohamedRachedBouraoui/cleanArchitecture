using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Uccspu.Affaires.Communs.Exceptions
{
    public class ExceptionDeValidation : Exception
    {
        public ExceptionDeValidation()
            : base("Un ou plusieurs échecs de validation se sont produits.")
        {
            Erreurs = new Dictionary<string, string[]>();
        }

        public ExceptionDeValidation(IEnumerable<ValidationFailure> echecs)
            : this()
        {
            var groupeEchecs = echecs
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in groupeEchecs)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Erreurs.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Erreurs { get; }
    }
}