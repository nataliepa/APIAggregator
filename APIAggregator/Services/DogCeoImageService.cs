using System.Text.Json;
using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreedCeoImageDto;
using APIAggregator.Models.Entities.DogBreedCeoImage;
using APIAggregator.Services.Definitions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace APIAggregator.Services;

public class DogCeoImageService : IDogCeoImageService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DogCeoImageService> _logger;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public DogCeoImageService(HttpClient httpClient, ILogger<DogCeoImageService> logger, IMapper mapper, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _logger = logger;
        _mapper = mapper;
        _cache = cache;
    }
    
    public async Task<DogBreedCeoImageDataDto?> GetRandomDogImageByBreed(string breedName)
    {
        if (string.IsNullOrWhiteSpace(breedName))
            return null;
        
        var cacheKey = $"image_{breedName.ToLower().Trim().Replace(" ", "_")}";

        if (_cache.TryGetValue(cacheKey, out DogBreedCeoImageDataDto? cached))
        {
            return cached;
        }
        
        var url = $"https://dog.ceo/api/breed/{breedName}/images/random";
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("DogCeo API returned status {StatusCode}", response.StatusCode);
            return null;
        }
        
        var json = await response.Content.ReadAsStringAsync();
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<DogBreedCeoImageDataDto>(json, options);
        
        if (result is null)
        {
            _logger.LogWarning("TheDogCeoImage API returned null result for breed {breed}", breedName);
            return null;
        }
        
        _cache.Set(cacheKey, result, TimeSpan.FromHours(1));
        
        return _mapper.Map<DogBreedCeoImageDataDto>(result);
    }
}