using APIAggregator.Models.Dtos;

namespace APIAggregator.Services.Definitions;

public interface ΙDogBreedExtraInfoService
{
    Task<DogBreedExtraInfoDto?> GetDogBreedExtraInfo(string breedName);
}