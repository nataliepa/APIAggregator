namespace APIAggregator.Models.Dtos;

public class DogBreedsWithPaginationDto
{
    public List<DogBreedDto> Breeds { get; set; } = new();
    public PaginationDto Pagination { get; set; }
}