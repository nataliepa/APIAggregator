using System.ComponentModel.DataAnnotations;

namespace APIAggregator.Models.Dtos;

public class DogBreedFilterDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int Page { get; set; } = 1;
    public string? Name { get; set; }
    public bool? Hypoallergenic { get; set; }
    public string? BreedGroup { get; set; }
    public string? Temperament { get; set; }
    public int? MinWeight { get; set; }
    public int? MaxWeight { get; set; }
    public int? MinLifeSpan { get; set; }
    public int? MaxLifeSpan { get; set; }
}