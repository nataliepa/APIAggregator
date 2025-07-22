using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.WikipediaDogSummary;
using AutoMapper;

namespace APIAggregator.Profiles;

public class WikipediaDogBreedInfoProfile : Profile
{
    public WikipediaDogBreedInfoProfile()
    {
        CreateMap<WikipediaDogBreedInfoResponse, WikipediaDogBreedInfoDto>()
            .ForMember(dest => dest.ThumbnailUrl, opt => 
                opt.MapFrom(src => 
                    (src.Thumbnail != null) 
                        ? src.Thumbnail.Source 
                        : string.Empty))            
            .ForMember(dest => dest.PageUrl, opt => 
                opt.MapFrom(src => 
                    (src.Content_Urls != null && src.Content_Urls.Desktop != null) 
                        ? src.Content_Urls.Desktop.Page 
                        : string.Empty));

    }
}