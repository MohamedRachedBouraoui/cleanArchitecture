using AutoMapper;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.AccessDonnees;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Domaine.Communs;

namespace Uccspu.Infrastructure.AccessDonnees
{
    public class UniteDeTravail : IUniteDeTravail
    {
        private readonly UccspuDbContext _uccspuDbContext;
        private readonly IMapper _mapper;
        private Hashtable _repositories;
        public UniteDeTravail(UccspuDbContext uccspuDbContext, IMapper mapper)
        {
            _uccspuDbContext = uccspuDbContext;
            _mapper = mapper;


        }
        public void Dispose()
        {
            _uccspuDbContext.Dispose();
        }

        public async Task<bool> EnregistrerTous(CancellationToken cancellationToken)
        {

            var result = await _uccspuDbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Auditable
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var entityType = typeof(TEntity);
            var entityName = entityType.Name;

            if (_repositories.ContainsKey(entityName) == false)
            {

                var repositoryType = typeof(Repository<,>);
                Type[] typeParameters = new Type[] { typeof(TEntity), typeof(TKey) };
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeParameters), _uccspuDbContext, _mapper);

                
                _repositories.Add(entityName, repositoryInstance);
            }

            return (IRepository<TEntity, TKey>)_repositories[entityName];
        }
    }
}
