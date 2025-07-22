namespace APIAggregator.Models.Entities.WikipediaDogSummary;

public class WikipediaDogBreedInfoResponse
{
    public string? Title { get; set; }
    public string? Extract { get; set; }
    public Thumbnail? Thumbnail { get; set; }
    public Content_Urls? Content_Urls { get; set; }
}