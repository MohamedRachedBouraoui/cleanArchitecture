using MediatR;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Attributs;
using Uccspu.Affaires.Communs.Exceptions;
using Uccspu.Affaires.Communs.Interfaces;

namespace Uccspu.Affaires.Communs.Comportements
{
    public class ComportementAuthentification<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUtilisateurEnCoursService _utilisateurEnCoursService;

        public ComportementAuthentification(IUtilisateurEnCoursService utilisateurEnCoursService)
        {
            _utilisateurEnCoursService = utilisateurEnCoursService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Type typeReponse = typeof(TResponse);            

            Authentifier attributAuthentifier =
          (Authentifier)Attribute.GetCustomAttribute(typeof(TRequest), typeof(Authentifier));
            
            if (attributAuthentifier == null
                || typeof(ReponseApi) != typeReponse.BaseType)
            {
                return await next();
            }

            else
            {
                if (string.IsNullOrEmpty(_utilisateurEnCoursService.IdUtilisateur))
                {
                    TResponse instance = Activator.CreateInstance<TResponse>();

                    PropertyInfo propertyInfo = typeReponse.GetProperty(nameof(ReponseApi.ReponseApiCodeStatut_));
                    propertyInfo.SetValue(instance, 401, null);

                    propertyInfo = typeReponse.GetProperty(nameof(ReponseApi.ReponseApiMessage_));
                    propertyInfo.SetValue(instance, "Vous n'êtes pas autorisé.", null);

                    return await Task.FromResult(instance);
                }

                return await next();

            }

        }
    }
}
