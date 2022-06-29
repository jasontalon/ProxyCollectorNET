using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ProxyCollectorNET.Infrastructure;
using ProxyCollectorNET.Infrastructure.Database;

namespace ProxyCollectorNET.Tests;

public class Test : BaseSetup
{
    [Test]
    public async Task Should_Test()
    {
        var cancellationToken = new CancellationTokenSource().Token;
        var db = GetService<AppDbContext>();
        var strategies = GetService<IEnumerable<IProxyCollectorStrategy>>();

        var strategy = strategies.First();

        await strategy.WorkAsync(cancellationToken);
    }
}