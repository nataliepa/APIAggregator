namespace APIAggregator.Models.Dtos;

public class AggregatedDogBreedInfoDto
{
    public DogBreedDto DogBreedDto { get; set; }
    public DogBreedExtraInfoDto DogBreedExtraInfoDto { get; set; }
    public DogBreedCeoImageDataDto DogBreedCeoImageDataDto { get; set; }
    public WikipediaDogBreedInfoDto WikipediaDogBreedInfoDto { get; set; }
}