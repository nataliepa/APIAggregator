using System.Text.Json;
using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.DogBreedExtraInfo;
using APIAggregator.Services.Definitions;
using AutoMapper;

namespace APIAggregator.Services;

public class DogBreedExtraInfoService : ΙDogBreedExtraInfoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DogService> _logger;
    private readonly IMapper _mapper;

    public DogBreedExtraInfoService(HttpClient httpClient, ILogger<DogService> logger, IMapper mapper)
    {
        _httpClient = httpClient;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<DogBreedExtraInfoDto?> GetDogBreedExtraInfo(string breedName)
    {
        var url = $"https://api.thedogapi.com/v1/breeds/search?q={breedName}";
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
        
        var results = JsonSerializer.Deserialize<List<DogBreedExtraInfoResponse>>(json, options);
        
        var breedResult = results?.FirstOrDefault(r => 
            string.Equals(r.Name, breedName, StringComparison.OrdinalIgnoreCase));
        
        if (breedResult is null)
        {
            _logger.LogWarning("TheDogApi API returned null result for breed {BreedName}", breedName);
            return null;
        }
        
        return _mapper.Map<DogBreedExtraInfoDto>(breedResult);
    }
}