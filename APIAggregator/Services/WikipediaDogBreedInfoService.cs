using System.Text.Json;
using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.WikipediaDogSummary;
using APIAggregator.Services.Definitions;
using AutoMapper;

namespace APIAggregator.Services;

public class WikipediaDogBreedInfoService : IWikipediaDogBreedInfoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WikipediaDogBreedInfoService> _logger;
    private readonly IMapper _mapper;

    public WikipediaDogBreedInfoService(HttpClient httpClient, ILogger<WikipediaDogBreedInfoService> logger, IMapper mapper)
    {
        _httpClient = httpClient;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<WikipediaDogBreedInfoDto?> GetWikipediaDogBreedInfo(string breedName)
    {
        if (string.IsNullOrWhiteSpace(breedName))
            return null;
        
        var url = $"https://en.wikipedia.org/api/rest_v1/page/summary/{breedName}";

        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Wikipedia API returned status {StatusCode} for breed {BreedName}", response.StatusCode, breedName);
            return null;
        }
        
        var json = await response.Content.ReadAsStringAsync();
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var result = JsonSerializer.Deserialize<WikipediaDogBreedInfoResponse>(json, options);
        
        if (result is null)
        {
            _logger.LogWarning("Wikipedia API returned null result for breed {BreedName}", breedName);
            return null;
        }
        
        return _mapper.Map<WikipediaDogBreedInfoDto>(result);
    }
}