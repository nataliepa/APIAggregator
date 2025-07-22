using APIAggregator.Models.Dtos;

namespace APIAggregator.Services.Definitions;

public interface IWikipediaDogBreedInfoService
{
    Task<WikipediaDogBreedInfoDto?> GetWikipediaDogBreedInfo(string breedName);
}