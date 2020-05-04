using NUnit.Framework;
using System.Threading.Tasks;

namespace Affaire.Tests.Integration
{
    using static TestsUtils;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ReinitialiserEtatInitial();
        }
    }
}