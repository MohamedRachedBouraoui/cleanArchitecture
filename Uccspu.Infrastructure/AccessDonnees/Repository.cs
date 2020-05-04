using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uccspu.AccessDonnees;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Domaine.Communs;

namespace Uccspu.Infrastructure.AccessDonnees
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : Auditable
    {
        private readonly UccspuDbContext _uccspuDbContext;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _mapperConfigurationProvider;

        public Repository(UccspuDbContext uccspuDbContext, IMapper mapper)
        {
            _uccspuDbContext = uccspuDbContext;
            _mapper = mapper;
            _mapperConfigurationProvider = mapper.ConfigurationProvider;
        }

        #region Requetes
        public async Task<int> CompterAsync(ISpecification<T> spec)
        {
            return await AppliquerSpecification(spec).CountAsync().ConfigureAwait(false);
        }

        public async Task<DTO> RecupererParIdAsync<DTO>(TKey id)
        {
            var result = await _uccspuDbContext.Set<T>().FindAsync(id).ConfigureAwait(false);
            return _mapper.Map<DTO>(result); // A améliorer
        }

        public async Task<DTO> RecupererAsync<DTO>(ISpecification<T> spec)
        {
            return await AppliquerSpecification(spec).ProjectTo<DTO>(_mapperConfigurationProvider).FirstOrDefaultAsync().ConfigureAwait(false); ;
        }

        public async Task<IReadOnlyList<T>> RecupererListeAsync()
        {
            return await _uccspuDbContext.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<T>> RecupererListeAsync(ISpecification<T> spec)
        {
            return await AppliquerSpecification(spec).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<DTO>> RecupererListeAsync<DTO>()
        {
            return await _uccspuDbContext.Set<T>().ProjectTo<DTO>(_mapperConfigurationProvider).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<DTO>> RecupererListeAsync<DTO>(ISpecification<T> spec)
        {
            return await AppliquerSpecification(spec).ProjectTo<DTO>(_mapperConfigurationProvider).ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> RecupererParIdAsync(TKey id)
        {
            return await _uccspuDbContext.Set<T>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<T> RecupererAsync(ISpecification<T> spec)
        {
            return await AppliquerSpecification(spec).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        #endregion

        #region Commandes
        public void Ajouter(T entite)
        {
            _uccspuDbContext.Set<T>().Add(entite);
        }

        public void Modifier(T entite)
        {
            _uccspuDbContext.Set<T>().Attach(entite);
            _uccspuDbContext.Entry(entite).State = EntityState.Modified;
        }

        public void Supprimer(T entite)
        {
            _uccspuDbContext.Set<T>().Remove(entite);
        }

        #endregion


        

       

        private IQueryable<T> AppliquerSpecification(ISpecification<T> spec)
        {
            return EvaluateurDeSpecifications<T>.Recuperer(_uccspuDbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
