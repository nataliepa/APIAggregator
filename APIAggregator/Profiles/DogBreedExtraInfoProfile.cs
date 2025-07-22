using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.DogBreedExtraInfo;
using AutoMapper;

namespace APIAggregator.Profiles;

public class DogBreedExtraInfoProfile : Profile
{
    public DogBreedExtraInfoProfile()
    {
        CreateMap<DogBreedExtraInfoResponse, DogBreedExtraInfoDto>();
    }
}