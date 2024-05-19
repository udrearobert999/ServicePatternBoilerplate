namespace ServicePattern.Domain.Entities;

public class Genre : IEntity<Guid>
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}