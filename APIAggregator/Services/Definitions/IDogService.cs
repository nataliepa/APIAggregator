using APIAggregator.Models.Dtos;

namespace APIAggregator.Services.Definitions;

public interface IDogService
{
    Task<DogBreedsWithPaginationDto> GetDogBreeds(int page = 1);
}