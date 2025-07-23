using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreed;
using APIAggregator.Models.Entities.DogBreed;
using AutoMapper;

namespace APIAggregator.Profiles;

public class DogBreedProfile : Profile
{
    public DogBreedProfile()
    {
        CreateMap<DogBreedData, DogBreedDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Attributes != null ? src.Attributes.Name : string.Empty))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Attributes != null ? src.Attributes.Description : string.Empty))
            .ForMember(dest => dest.LifeSpanMin, opt => opt.MapFrom(src =>
                src.Attributes != null && src.Attributes.Life != null ? src.Attributes.Life.Min : 0))
            .ForMember(dest => dest.LifeSpanMax, opt => opt.MapFrom(src =>
                src.Attributes != null && src.Attributes.Life != null ? src.Attributes.Life.Max : 0))
            .ForMember(dest => dest.MaleWeightMin, opt => opt.MapFrom(src =>
                src.Attributes != null && src.Attributes.Male_Weight != null ? src.Attributes.Male_Weight.Min : 0))
            .ForMember(dest => dest.MaleWeightMax, opt => opt.MapFrom(src =>
                src.Attributes != null && src.Attributes.Male_Weight != null ? src.Attributes.Male_Weight.Max : 0))
            .ForMember(dest => dest.FemaleWeightMin, opt => opt.MapFrom(src =>
                src.Attributes != null && src.Attributes.Female_Weight != null ? src.Attributes.Female_Weight.Min : 0))
            .ForMember(dest => dest.FemaleWeightMax, opt => opt.MapFrom(src =>
                src.Attributes != null && src.Attributes.Female_Weight != null ? src.Attributes.Female_Weight.Max : 0))
            .ForMember(dest => dest.Hypoallergenic,
                opt => opt.MapFrom(src => src.Attributes != null && src.Attributes.Hypoallergenic.HasValue 
                    ? src.Attributes.Hypoallergenic 
                    : null));}
}