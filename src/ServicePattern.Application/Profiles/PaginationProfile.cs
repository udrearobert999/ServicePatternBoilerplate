using AutoMapper;
using ServicePattern.Application.Dtos;

namespace ServicePattern.Application.Profiles;

internal sealed class PaginationProfile : Profile
{
    public PaginationProfile()
    {
        CreateMap(typeof(PagedRequestDto), typeof(PagedResponseDto<>)).ReverseMap();
    }
}