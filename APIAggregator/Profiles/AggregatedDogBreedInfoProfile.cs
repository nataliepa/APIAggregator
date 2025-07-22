using APIAggregator.Models.Dtos;
using AutoMapper;

namespace APIAggregator.Profiles;

public class AggregatedDogBreedInfoProfile : Profile
{
    public AggregatedDogBreedInfoProfile()
    {
        CreateMap<DogBreedDto, AggregatedDogBreedInfoDto>();
    }
}