using APIAggregator.Models.Dtos.DogBreed;
using APIAggregator.Models.Dtos.DogBreedCeoImageDto;

namespace APIAggregator.Models.Dtos;

public class AggregatedDogBreedInfoDto
{
    public DogBreedDto DogBreedDto { get; set; }
    public DogBreedExtraInfoDto.DogBreedExtraInfoDto DogBreedExtraInfoDto { get; set; }
    public DogBreedCeoImageDataDto DogBreedCeoImageDataDto { get; set; }
    public WikipediaDogBreedInfoDto.WikipediaDogBreedInfoDto WikipediaDogBreedInfoDto { get; set; }
}