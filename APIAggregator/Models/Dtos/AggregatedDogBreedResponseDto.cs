namespace APIAggregator.Models.Dtos;

public class AggregatedDogBreedResponseDto
{
    public List<AggregatedDogBreedInfoDto> Breeds { get; set; }
    public PaginationDto Pagination { get; set; }
}