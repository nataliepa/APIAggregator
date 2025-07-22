using APIAggregator.Models.Dtos;
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
    
    public async Task<AggregatedDogBreedResponseDto> GetAggregatedDogInfo(int page)
    {
        var breedApiResult = await _dogService.GetDogBreeds(page);
        var breeds = breedApiResult.Breeds;
        var result = new List<AggregatedDogBreedInfoDto>();

        foreach (var breed in breeds)
        {
            var breedName = breed.Name?.ToLower();

            if (string.IsNullOrWhiteSpace(breedName))
            {
                _logger.LogWarning("Breed name is null or empty. Skipping entry.");
                continue;
            }

            var image = await _dogCeoImageService.GetRandomDogImageByBreed(breedName);

            var wikipediaInfo = await _wikipediaDogBreedInfoService.GetWikipediaDogBreedInfo(breedName)
                                ?? await _wikipediaDogBreedInfoService.GetWikipediaDogBreedInfo($"{breedName} (dog breed)");

            var dogBreedExtraInfo = await _dogBreedExtraInfoService.GetDogBreedExtraInfo(breedName);

            var aggregated = new AggregatedDogBreedInfoDto
            {
                DogBreedDto = breed,
                DogBreedCeoImageDataDto = image ?? new DogBreedCeoImageDataDto
                {
                    Message = "https://via.placeholder.com/default.jpg"
                },
                WikipediaDogBreedInfoDto = wikipediaInfo ?? new WikipediaDogBreedInfoDto(),
                DogBreedExtraInfoDto = dogBreedExtraInfo ?? new DogBreedExtraInfoDto()
            };

            result.Add(aggregated);
        }
        
        return new AggregatedDogBreedResponseDto
        {
            Breeds = result,
            Pagination = _mapper.Map<PaginationDto>(breedApiResult.Pagination)
        };
    }

}