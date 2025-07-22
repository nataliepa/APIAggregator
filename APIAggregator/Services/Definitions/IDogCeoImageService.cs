using APIAggregator.Models.Dtos;

namespace APIAggregator.Services.Definitions;

public interface IDogCeoImageService
{
    Task<DogBreedCeoImageDataDto> GetRandomDogImageByBreed(string breed);
}