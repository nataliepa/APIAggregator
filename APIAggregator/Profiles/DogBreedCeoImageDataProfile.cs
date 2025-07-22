using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.DogBreedCeoImage;
using AutoMapper;

namespace APIAggregator.Profiles;

public class DogBreedCeoImageDataProfile : Profile
{
    public DogBreedCeoImageDataProfile()
    {
        CreateMap<DogBreedCeoImageData, DogBreedCeoImageDataDto>();
    }
}