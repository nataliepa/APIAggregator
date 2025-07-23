using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreedCeoImageDto;

namespace APIAggregator.Services.Definitions;

public interface IDogCeoImageService
{
    Task<DogBreedCeoImageDataDto?> GetRandomDogImageByBreed(string breed);
}