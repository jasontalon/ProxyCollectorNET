using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace ProxyCollectorNET.Tests;

public abstract class BaseSetup
{
    private IHost host;
    private IServiceProvider serviceProvider;

    [OneTimeSetUp]
    protected void OneTimeSetup() => host = ProxyCollectorNET.BuildHost();

    [SetUp]
    protected void Setup() => serviceProvider = host.Services;

    protected T GetService<T>() => serviceProvider.GetRequiredService<T>();
}