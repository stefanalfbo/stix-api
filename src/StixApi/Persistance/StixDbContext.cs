using Microsoft.EntityFrameworkCore;
using StixApi.Domain.Entities;

namespace StixApi.Persistance;

public class StixDbContext : DbContext
{
    public StixDbContext(DbContextOptions<StixDbContext> options) : base(options)
    {
    }

    public DbSet<Vulnerability> Vulnerabilities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vulnerability>()
            .ToTable("Vulnerability")
            .Ignore(v => v.Extensions);
    }
}