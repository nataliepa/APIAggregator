using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreed;
using AutoMapper;

namespace APIAggregator.Profiles;

public class AggregatedDogBreedInfoProfile : Profile
{
    public AggregatedDogBreedInfoProfile()
    {
        CreateMap<DogBreedDto, AggregatedDogBreedInfoDto>();
    }
}