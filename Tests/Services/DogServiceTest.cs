/*using System.Net;
using System.Text;
using System.Text.Json;
using APIAggregator.Models.Dtos;
using APIAggregator.Models.Entities.DogBreed;
using APIAggregator.Profiles;
using APIAggregator.Services;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;

namespace Tests.Services;
using Microsoft.Extensions.Logging;

public class DogServiceTest
{
    [Fact]
    public async void UserService_GetDogApiResponse_ReturnsDogBreedsWithPaginationDto()
    {
        // Arrange
        var logger = A.Fake<ILogger<DogService>>();
        var cache = new MemoryCache(new MemoryCacheOptions());

        var dogApiResponse = new DogApiResponse()
        {
            Data = new List<DogBreedData>
            {
                new DogBreedData
                {
                    Id = "1",
                    Attributes = new DogAttributes
                    {
                        Name = "Akita",
                        Description = "Dignified and courageous",
                        Life = new DogRange { Min = 10, Max = 12 },
                        Male_Weight = new DogRange { Min = 30, Max = 45 },
                        Female_Weight = new DogRange { Min = 25, Max = 35 },
                        Hypoallergenic = false
                    }
                }
            },
            Meta = new DogApiMeta
            {
                Pagination = new DogApiPagination
                {
                    Current = 1,
                    Next = null,
                    Last = 1,
                    Records = 1
                }
            }
        };

        var json = JsonSerializer.Serialize(dogApiResponse);
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        
        var handler = A.Fake<HttpMessageHandler>();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://dogapi.dog/api/v2/")
        };
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DogBreedProfile>();
            cfg.CreateMap<DogApiPagination, PaginationDto>();
        });
        
        var mapper = config.CreateMapper();
        var service = new DogService(httpClient, logger, mapper, cache);
        
        // Act
        var result = await service.GetDogBreeds(1);
        
        // Assert
        result.Should().NotBeNull();
        result.Breeds.
    }   
}   */