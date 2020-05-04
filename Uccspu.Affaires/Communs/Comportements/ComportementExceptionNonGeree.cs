using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Uccspu.Affaires.Communs.Comportements
{
    public class ComportementExceptionNonGeree<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public ComportementExceptionNonGeree(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest requete, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var nomRequete = typeof(TRequest).Name;

                _logger.LogError(ex, $"Excéption non gérée en relation avec la Rêquète {nomRequete} {requete}");

                throw;
            }
        }
    }
}
