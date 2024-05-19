using Microsoft.EntityFrameworkCore;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;
using ServicePattern.Infrastructure.Persistence.Repositories;

namespace ServicePattern.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    public IRepository<Movie, Guid> Movies { get; set; }
    public IRepository<Genre, Guid> Genres { get; set; }

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;

        Movies = new Repository<Movie, Guid>(dbContext);
        Genres = new Repository<Genre, Guid>(dbContext);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}