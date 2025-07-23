using System.Text.Json;
using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreedExtraInfoDto;
using APIAggregator.Models.Entities.DogBreedExtraInfo;
using APIAggregator.Services.Definitions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace APIAggregator.Services;

public class DogBreedExtraInfoService : ΙDogBreedExtraInfoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DogService> _logger;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public DogBreedExtraInfoService(HttpClient httpClient, ILogger<DogService> logger, IMapper mapper, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _logger = logger;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<DogBreedExtraInfoDto?> GetDogBreedExtraInfo(string breedName)
    {
        if (string.IsNullOrWhiteSpace(breedName))
            return null;
        
        var cacheKey = $"extra_{breedName.ToLower().Trim().Replace(" ", "_")}";

        if (_cache.TryGetValue(cacheKey, out DogBreedExtraInfoDto? cached))
        {
            return cached;
        }
        
        var url = $"https://api.thedogapi.com/v1/breeds/search?q={breedName.Replace(" ", "_")}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("TheDogAPI returned status {StatusCode}", response.StatusCode);
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var results = JsonSerializer.Deserialize<List<DogBreedExtraInfoDto>>(json, options);
        
        var breedResult = results?.FirstOrDefault(r => 
            string.Equals(r.Name, breedName, StringComparison.OrdinalIgnoreCase));
        
        if (breedResult is null)
        {
            _logger.LogWarning("TheDogApi API returned null result for breed {BreedName}", breedName);
            return null;
        }
        
        _cache.Set(cacheKey, breedResult, TimeSpan.FromHours(1));
        
        return _mapper.Map<DogBreedExtraInfoDto>(breedResult);
    }
}