using ServicePattern.Application.Dtos.Shared;

namespace ServicePattern.Application.Dtos.Movies.Options
{
    public class GetAllMoviesOptionsDto : BaseGetCollectionOptionsDto
    {
        public string? Title { get; init; }
    }
}