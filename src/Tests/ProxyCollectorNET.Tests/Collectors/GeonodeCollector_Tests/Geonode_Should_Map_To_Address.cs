using System.Collections.Generic;
using AutoMapper;
using NUnit.Framework;
using ProxyCollectorNET.Domain;
using ProxyCollectorNET.Dto;
using ProxyCollectorNET.Infrastructure.Extensions;
using Shouldly;

namespace ProxyCollectorNET.Tests.Collectors.GeonodeCollector_Tests;

public class Geonode_Should_Map_To_Address : BaseSetup
{
    [Test]
    public void Should_Map()
    {
        var mapper = GetService<IMapper>()!;

        var source = new GeonodeResponse
        {
            Data = new List<GeonodeResponseDatum>
            {
                new()
                {
                    Ip = "127.0.0.1",
                    City = "Manila",
                    Country = "PH",
                    Port = "8080",
                    Isp = "PLDT",
                    Org = "Philippine Long Distance",
                    LastChecked = 1660570663,
                    Protocols = new List<string> {"SOCKS5", "HTTPS"}
                }
            }
        };

        var (_, error) = mapper.TryMap<GeonodeResponse, List<Address>>(source);

        error.ShouldBeNullOrEmpty(error);
    }
}