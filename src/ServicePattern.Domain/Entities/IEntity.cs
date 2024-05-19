namespace ServicePattern.Domain.Entities;

public interface IEntity<TKey> where TKey : struct
{
    TKey Id { get; init; }
}