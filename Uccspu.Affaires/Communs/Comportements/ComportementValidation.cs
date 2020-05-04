using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Exceptions;
using Uccspu.Affaires.Communs.Extensions;

namespace Uccspu.Affaires.Communs.Comportements
{
    public class ComportementValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ComportementValidation(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Type typeReponse = typeof(TResponse);

            if (_validators.Any() == false || typeof(ReponseApi) != typeReponse.BaseType)
            {
                return next();
            }

            var context = new ValidationContext(request);

            var echecs = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (echecs.Count == 0)
            {
                return next();
            }

            
                TResponse instance = Activator.CreateInstance<TResponse>();
            
                PropertyInfo propertyInfo = typeReponse.GetProperty(nameof(ReponseApi.ReponseApiCodeStatut_));
                propertyInfo.SetValue(instance, 400, null);                
                
                propertyInfo = typeReponse.GetProperty(nameof(ReponseApi.ReponseApiMessage_));
            propertyInfo.SetValue(instance, FormatterMessagesErreurs(echecs));//  string.Join('\n', failures.Select(f => f.ErrorMessage)), null); ;// ;// ;

                return Task.FromResult(instance);
            
        }

        private object FormatterMessagesErreurs(List<ValidationFailure> echecs)
        {
            Dictionary<string, string[]> Erreurs = new Dictionary<string, string[]>();

            var groupeEchecs = echecs
                 .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in groupeEchecs)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Erreurs.Add(propertyName, propertyFailures);
            }
            return JsonConvert.SerializeObject(Erreurs);
        }
    }
}