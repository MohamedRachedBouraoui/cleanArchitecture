using AutoMapper;
using NUnit.Framework;
using System;
using Uccspu.Affaires.Communs.Mappings;
using Uccspu.Affaires.Episodes.Requetes;
using Uccspu.Domaine.Entites;

namespace Affaire.TestsUnitaires.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void DevraitAvoirUneConfigurationValide()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(Episode), typeof(RecupererEpisodeDto))]
        public void DevraitSupporterLesMappingsIndiques(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
