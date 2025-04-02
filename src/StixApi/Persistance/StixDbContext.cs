using Microsoft.EntityFrameworkCore;
using StixApi.Persistance.Models;

namespace StixApi.Persistance;

public class StixDbContext : DbContext
{
    public StixDbContext(DbContextOptions<StixDbContext> options) : base(options)
    {
    }

    public DbSet<VulnerabilityDbModel> Vulnerabilities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(StixDbContext).Assembly);
    }
}