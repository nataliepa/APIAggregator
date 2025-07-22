namespace APIAggregator.Models.Dtos;

public class PaginationDto
{
    public int Current { get; set; }
    public int? Next { get; set; }
    public int Last { get; set; }
    public int Records { get; set; }
}