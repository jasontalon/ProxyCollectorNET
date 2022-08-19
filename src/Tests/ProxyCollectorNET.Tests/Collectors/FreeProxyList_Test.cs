using System.Threading.Tasks;
using NUnit.Framework;
using ProxyCollectorNET.Collectors;
using Shouldly;

namespace ProxyCollectorNET.Tests.Collectors;

public class FreeProxyList_Test : BaseSetup
{
    [Test]
    public async Task FreeProxyList_Should_Fetch_Addresses()
    {
        var service = GetService<IFreeProxyListCollector>();

        var response = await service.FetchAsync();

        response.Addresses.ShouldNotBeNull(response.Exception?.ToString());
    }
}