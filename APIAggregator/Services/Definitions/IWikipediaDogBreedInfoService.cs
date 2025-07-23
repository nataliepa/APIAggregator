using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.WikipediaDogBreedInfoDto;

namespace APIAggregator.Services.Definitions;

public interface IWikipediaDogBreedInfoService
{
    Task<WikipediaDogBreedInfoDto?> GetWikipediaDogBreedInfo(string breedName);
}