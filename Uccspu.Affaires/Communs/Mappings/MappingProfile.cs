using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Uccspu.Affaires.Communs.Mappings
{
    /// <summary>
    /// Cette classe va parcourir les classes implmentant l'interface générique "IMapFrom"
    /// pour creer les mappages et les enregistrer dans un "Profile" consommé par "Automapper"
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}