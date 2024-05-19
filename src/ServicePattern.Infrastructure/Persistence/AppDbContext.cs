using Microsoft.EntityFrameworkCore;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Infrastructure.Persistence;

internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public required DbSet<Movie> Movies { get; set; }
    public required DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}