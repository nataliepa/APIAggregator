namespace APIAggregator.Models.Entities.DogBreed;

public class DogApiResponse
{
    public List<DogBreedData>? Data { get; set; }
    public DogApiMeta? Meta  { get; set; }
}