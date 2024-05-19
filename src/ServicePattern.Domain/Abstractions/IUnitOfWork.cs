using ServicePattern.Domain.Entities;

namespace ServicePattern.Domain.Abstractions;

public interface IUnitOfWork
{
    public IRepository<Movie, Guid> Movies { get; set; }
    public IRepository<Genre, Guid> Genres { get; set; }
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}