namespace APIAggregator.Models.Entities.DogBreed;

public class DogAttributes
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DogRange? Life { get; set; }
    public DogRange? Male_Weight { get; set; }
    public DogRange? Female_Weight { get; set; }
    public bool? Hypoallergenic { get; set; }
}