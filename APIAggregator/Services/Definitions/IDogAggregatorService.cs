using APIAggregator.Models.Dtos;

namespace APIAggregator.Services.Definitions;

public interface IDogAggregatorService
{
    Task<AggregatedDogBreedResponseDto> GetAggregatedDogInfo(DogBreedFilterDto filter);
}