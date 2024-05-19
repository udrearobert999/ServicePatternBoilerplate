namespace ServicePattern.Domain.Entities;

public class Movie : IEntity<Guid>
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public required string? Description { get; set; }
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
}