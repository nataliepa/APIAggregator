using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.DogBreed;
using AutoMapper;

namespace APIAggregator.Profiles;

public class PaginationProfile : Profile
{
    public PaginationProfile()
    {
        CreateMap<DogApiPagination, PaginationDto>()
            .ForMember(dest => dest.Current, opt => opt.MapFrom(src => src.Current))
            .ForMember(dest => dest.Next, opt => opt.MapFrom(src => src.Next))
            .ForMember(dest => dest.Last, opt => opt.MapFrom(src => src.Last))
            .ForMember(dest => dest.Records, opt => opt.MapFrom(src => src.Records));
    }
}