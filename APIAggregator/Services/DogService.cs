using System.Text.Json;
using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.DogBreed;
using APIAggregator.Services.Definitions;
using AutoMapper;

namespace APIAggregator.Services;

public class DogService : IDogService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DogService> _logger;
    private readonly IMapper _mapper;

    public DogService(HttpClient httpClient, ILogger<DogService> logger, IMapper mapper)
    {
        _httpClient = httpClient;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<DogBreedsWithPaginationDto> GetDogBreeds(int page = 1)
    {
        var url = $"https://dogapi.dog/api/v2/breeds?page[number]={page}";
        
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Dog API returned status {StatusCode}", response.StatusCode);
            return new DogBreedsWithPaginationDto();
        }
        
        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<DogApiResponse>(json, options);
        
        if (result?.Data is null or { Count: 0 })
        {
            _logger.LogWarning("Dog API returned empty data on page {Page}", page);
            return new DogBreedsWithPaginationDto();
        }
        
        var breeds =  result.Data
            .Where(b => b.Attributes != null)
            .Select(b => _mapper.Map<DogBreedDto>(b))
            .ToList() ?? new();
        
        var paginationDto = _mapper.Map<PaginationDto>(result.Meta?.Pagination);
        
        return new DogBreedsWithPaginationDto
        {
            Breeds = breeds,
            Pagination = paginationDto
        };
    }
}