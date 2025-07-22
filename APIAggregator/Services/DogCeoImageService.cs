using System.Text.Json;
using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.DogBreedCeoImage;
using APIAggregator.Services.Definitions;
using AutoMapper;

namespace APIAggregator.Services;

public class DogCeoImageService : IDogCeoImageService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DogCeoImageService> _logger;
    private readonly IMapper _mapper;

    public DogCeoImageService(HttpClient httpClient, ILogger<DogCeoImageService> logger, IMapper mapper)
    {
        _httpClient = httpClient;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<DogBreedCeoImageDataDto> GetRandomDogImageByBreed(string breed)
    {
        var url = $"https://dog.ceo/api/breed/{breed}/images/random";
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

        var result = JsonSerializer.Deserialize<DogBreedCeoImageData>(json, options);
        
        if (result is null)
        {
            _logger.LogWarning("TheDogCeoImage API returned null result for breed {breed}", breed);
            return null;
        }
        
        return _mapper.Map<DogBreedCeoImageDataDto>(result);
    }
}