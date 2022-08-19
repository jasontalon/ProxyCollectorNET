using System.Threading.Tasks;
using NUnit.Framework;
using ProxyCollectorNET.Collectors;
using Shouldly;

namespace ProxyCollectorNET.Tests.Collectors;

public class ProxyScan_Tests : BaseSetup
{
    [Test]
    public async Task ProxyScan_Should_Fetch_Address()
    {
        var service = GetService<IProxyScanCollector>();

        var (addresses, error) = await service.FetchAsync();

        addresses.ShouldNotBeNull(error.ToString());
    }
}