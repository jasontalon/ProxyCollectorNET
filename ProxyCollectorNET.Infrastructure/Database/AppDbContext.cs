using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ProxyCollectorNET.Domain.Entities;

namespace ProxyCollectorNET.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(builder =>
        {
            builder.HasKey(k =>
                new
                {
                    k.IPAddress, k.Port
                });

            builder.Property(p => p.CreatedAtUtc).HasValueGenerator<GuidValueGenerator>();

        });
    }
}