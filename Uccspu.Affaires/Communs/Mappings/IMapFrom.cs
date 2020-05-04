using AutoMapper;

namespace Uccspu.Affaires.Communs.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()); // C'est l'implémentation par défaut
    }
}
