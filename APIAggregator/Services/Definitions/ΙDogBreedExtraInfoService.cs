using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreedExtraInfoDto;

namespace APIAggregator.Services.Definitions;

public interface ΙDogBreedExtraInfoService
{
    Task<DogBreedExtraInfoDto?> GetDogBreedExtraInfo(string breedName);
}