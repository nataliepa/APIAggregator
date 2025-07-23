using APIAggregator.Models.Dtos;
using APIAggregator.Services;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace APIAggregator.Tests.Services;

public class DogServiceTests
{
    private DogService CreateService(HttpResponseMessage response, IMemoryCache? cache = null)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://dogapi.dog/")
        };

        var loggerMock = new Mock<ILogger<DogService>>();
        var mapperMock = new Mock<IMapper>();

        // dummy mapping
        mapperMock.Setup(m => m.Map<List<DogBreedDto>>(It.IsAny<object>()))
            .Returns(new List<DogBreedDto> { new DogBreedDto { Name = "Test" } });
        mapperMock.Setup(m => m.Map<PaginationDto>(It.IsAny<object>()))
            .Returns(new PaginationDto { Current = 1 });

        return new DogService(httpClient, loggerMock.Object, mapperMock.Object, cache ?? new MemoryCache(new MemoryCacheOptions()));
    }
}