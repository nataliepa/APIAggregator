using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreed;

namespace APIAggregator.Services.Definitions;

public interface IDogService
{
    Task<DogBreedsWithPaginationDto?> GetDogBreeds(int page = 1);
}