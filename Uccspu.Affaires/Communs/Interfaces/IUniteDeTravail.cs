using System;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Domaine.Communs;

namespace Uccspu.Affaires.Communs.Interfaces
{
    public interface IUniteDeTravail : IDisposable
    {
        IRepository<TEntity,TKey> Repository<TEntity,TKey>() where TEntity : Auditable;
        public Task<bool> EnregistrerTous(CancellationToken cancellationToken);
    }
}
