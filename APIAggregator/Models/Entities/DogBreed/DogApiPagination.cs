namespace APIAggregator.Models.Entities.DogBreed;

public class DogApiPagination
{
    public int Current { get; set; }
    public int? Next { get; set; }
    public int Last { get; set; }
    public int Records { get; set; }
}