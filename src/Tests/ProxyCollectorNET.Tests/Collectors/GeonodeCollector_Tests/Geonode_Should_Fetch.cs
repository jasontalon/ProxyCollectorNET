using System.Threading.Tasks;
using NUnit.Framework;
using ProxyCollectorNET.Collectors;
using Shouldly;

namespace ProxyCollectorNET.Tests.Collectors.GeonodeCollector_Tests;

public class Geonode_Should_Fetch : BaseSetup
{
    [Test]
    public async Task Should_Fetch_Addresses()
    {
        var service = GetService<IGeonodeCollector>();

        var (results, error) = await service.FetchAsync();

        results.ShouldNotBeNull(error?.ToString());
    }
}