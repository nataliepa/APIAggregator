using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreedCeoImageDto;
using APIAggregator.Models.Dtos.DogBreedExtraInfoDto;
using APIAggregator.Models.Dtos.WikipediaDogBreedInfoDto;
using APIAggregator.Services.Definitions;
using AutoMapper;

namespace APIAggregator.Services;

public class DogAggregatorService : IDogAggregatorService
{
    private readonly IDogService _dogService;
    private readonly IDogCeoImageService _dogCeoImageService;
    private readonly IWikipediaDogBreedInfoService _wikipediaDogBreedInfoService;
    private readonly ΙDogBreedExtraInfoService _dogBreedExtraInfoService;
    private readonly ILogger<DogAggregatorService> _logger;
    private readonly IMapper _mapper;

    public DogAggregatorService(IDogService dogService, 
        IDogCeoImageService dogCeoImageService, 
        IWikipediaDogBreedInfoService wikipediaDogBreedInfoService, 
        ΙDogBreedExtraInfoService dogBreedExtraInfoService,
        ILogger<DogAggregatorService> logger, 
        IMapper mapper)
    {
        _dogService = dogService;
        _dogCeoImageService = dogCeoImageService;
        _wikipediaDogBreedInfoService = wikipediaDogBreedInfoService;
        _dogBreedExtraInfoService = dogBreedExtraInfoService;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<AggregatedDogBreedResponseDto> GetAggregatedDogInfo(DogBreedFilterDto filter)
    {
        var breedApiResult = await _dogService.GetDogBreeds(filter.Page);
        var breeds = breedApiResult.Breeds;
        var result = new List<AggregatedDogBreedInfoDto>();
        
        if (!string.IsNullOrWhiteSpace(filter.Name))
            breeds = breeds.Where(b => b.Name?.ToLower().Contains(filter.Name.ToLower()) == true).ToList();

        if (filter.Hypoallergenic.HasValue)
            breeds = breeds.Where(b => b.Hypoallergenic == filter.Hypoallergenic.Value).ToList();
        

        foreach (var breed in breeds)
        {
            if (string.IsNullOrWhiteSpace(breed.Name))
            {
                _logger.LogWarning("Breed name is null or empty. Skipping entry.");
                continue;
            }

            var breedName = breed.Name.ToLower();

            var imageTask = _dogCeoImageService.GetRandomDogImageByBreed(breedName);

            var wikipediaInfoTask = _wikipediaDogBreedInfoService.GetWikipediaDogBreedInfo(breedName);

            var dogBreedExtraInfoTask = _dogBreedExtraInfoService.GetDogBreedExtraInfo(breedName);
            
            await Task.WhenAll(imageTask, wikipediaInfoTask, dogBreedExtraInfoTask);

            var aggregated = new AggregatedDogBreedInfoDto
            {
                DogBreedDto = breed,
                DogBreedCeoImageDataDto = imageTask.Result ?? new DogBreedCeoImageDataDto
                {
                    Message = string.IsNullOrWhiteSpace(imageTask.Result?.Message) 
                        ? "https://via.placeholder.com/default.jpg" 
                        : imageTask.Result.Message
                },
                WikipediaDogBreedInfoDto = wikipediaInfoTask.Result ?? new WikipediaDogBreedInfoDto(),
                DogBreedExtraInfoDto = dogBreedExtraInfoTask.Result ?? new DogBreedExtraInfoDto()
            };

            result.Add(aggregated);
        }
        
        if (!string.IsNullOrWhiteSpace(filter.BreedGroup))
        {
            result = result
                .Where(r => r.DogBreedExtraInfoDto?.Breed_group?
                    .Contains(filter.BreedGroup, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(filter.Temperament))
        {
            result = result
                .Where(r => r.DogBreedExtraInfoDto?.Temperament?
                    .Contains(filter.Temperament, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }

        if (filter.MinWeight.HasValue)
        {
            result = result
                .Where(r => r.DogBreedDto?.MaleWeightMin >= filter.MinWeight ||
                            r.DogBreedDto?.FemaleWeightMin >= filter.MinWeight)
                .ToList();
        }

        if (filter.MaxWeight.HasValue)
        {
            result = result
                .Where(r => r.DogBreedDto?.MaleWeightMax <= filter.MaxWeight &&
                            r.DogBreedDto?.FemaleWeightMax <= filter.MaxWeight)
                .ToList();
        }

        if (filter.MinLifeSpan.HasValue)
        {
            result = result
                .Where(r => r.DogBreedDto?.LifeSpanMin >= filter.MinLifeSpan)
                .ToList();
        }

        if (filter.MaxLifeSpan.HasValue)
        {
            result = result
                .Where(r => r.DogBreedDto?.LifeSpanMax <= filter.MaxLifeSpan)
                .ToList();
        }
        
        return new AggregatedDogBreedResponseDto
        {
            Breeds = result,
            Pagination = _mapper.Map<PaginationDto>(breedApiResult.Pagination)
        };
    }

}