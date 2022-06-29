using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ProxyCollectorNET.Infrastructure.Database;

public abstract class DateTimeOffsetValueGenerator : ValueGenerator<DateTimeOffset>
{
    public override DateTimeOffset Next(EntityEntry entry)
    {
        return DateTimeOffset.UtcNow;
    }

    public override bool GeneratesTemporaryValues { get; }
}