using System.Collections.Generic;
using System.Threading.Tasks;
using Uccspu.Domaine.Communs;

namespace Uccspu.Affaires.Communs.Interfaces
{
    public interface IRepository<T,Key> where T : Auditable
    {
        Task<T> RecupererParIdAsync(Key id);
        Task<DTO> RecupererParIdAsync<DTO>(Key id);
        Task<IReadOnlyList<T>> RecupererListeAsync();
        Task<IReadOnlyList<DTO>> RecupererListeAsync<DTO>();
        Task<T> RecupererAsync(ISpecification<T> spec);
        Task<DTO> RecupererAsync<DTO>(ISpecification<T> spec);
        Task<IReadOnlyList<T>> RecupererListeAsync(ISpecification<T> spec);
        Task<IReadOnlyList<DTO>> RecupererListeAsync<DTO>(ISpecification<T> spec);
        Task<int> CompterAsync(ISpecification<T> spec);


        /*
         * Les méthodes suivantes ne doivent pas être Async
         * car il vont interagir seulement avec le "dbContext" (dans la mémoire)
         * et pas avec la BD, car c'est au moment de l'appel à la méthode
         * "EnregistrerTous()" de "UniteDeTravail" qu'ils seront réellement 
         * enregistrés dans la BD
         */
        void Ajouter(T entitte);
        void Modifier(T entitte);
        void Supprimer(T entitte);
    }
}
