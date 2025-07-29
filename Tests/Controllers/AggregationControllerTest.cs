using APIAggregator.Controllers;
using APIAggregator.Models.Dtos;
using APIAggregator.Models.Dtos.DogBreed;
using APIAggregator.Models.Dtos.DogBreedCeoImageDto;
using APIAggregator.Models.Dtos.DogBreedExtraInfoDto;
using APIAggregator.Models.Dtos.WikipediaDogBreedInfoDto;
using APIAggregator.Services.Definitions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Controllers;

public class AggregationControllerTest
{
    [Fact]
    public async Task GetDogBreeds_ReturnsOk_WhenResultExists()
    {
        // Arrange
        var mockService = new Mock<IDogAggregatorService>();
        var mockLogger = new Mock<ILogger<AggregationController>>();
        
        var filter = new DogBreedFilterDto { Page = 1 };

        var expected = new AggregatedDogBreedResponseDto
        {
            Breeds = new List<AggregatedDogBreedInfoDto>
            { 
                new()
                {
                    DogBreedDto = new DogBreedDto { Name = "Akita" },
                    DogBreedExtraInfoDto = new DogBreedExtraInfoDto { Name = "Akita", Bred_for = "Hunting bears"},
                    DogBreedCeoImageDataDto = new DogBreedCeoImageDataDto { Message = "https://images.dog.ceo/breeds/akita/512px-Akita_inu.jpg" },
                    WikipediaDogBreedInfoDto = new WikipediaDogBreedInfoDto { Title = "Akita", Extract = "Akita is a Japanese name"}
                }
            },
            Pagination = new PaginationDto
            {
                Current = 1,
                Next = null,
                Last = 1,
                Records = 1
            }
        };
        
        mockService.Setup(s => s.GetAggregatedDogInfo(filter))
            .ReturnsAsync(expected);
        
        var controller = new AggregationController(mockService.Object, mockLogger.Object);
        
        // Act
        var result = await controller.GetDogBreeds(filter);
        
        // Assert
        var ok = result.Result as OkObjectResult;
        ok.Should().NotBeNull();
        ok.StatusCode.Should().Be(StatusCodes.Status200OK);
        
        var resultValue = ok.Value as AggregatedDogBreedResponseDto;
        resultValue.Should().NotBeNull();
        resultValue.Breeds.Should().NotBeNull();

        var breedItem = resultValue.Breeds.FirstOrDefault();
        breedItem.Should().NotBeNull();
        
        breedItem.DogBreedDto.Should().NotBeNull();
        breedItem.DogBreedDto.Name.Should().NotBeNull().And.Be("Akita");
        
        breedItem.DogBreedExtraInfoDto.Should().NotBeNull();
        breedItem.DogBreedExtraInfoDto.Name.Should().NotBeNull().And.Be("Akita");
        breedItem.DogBreedExtraInfoDto.Bred_for.Should().NotBeNull().And.Be("Hunting bears");
        
        breedItem.DogBreedCeoImageDataDto.Should().NotBeNull();
        breedItem.DogBreedCeoImageDataDto.Message.Should().NotBeNull().And.Be("https://images.dog.ceo/breeds/akita/512px-Akita_inu.jpg");
        
        breedItem.WikipediaDogBreedInfoDto.Should().NotBeNull();
        breedItem.WikipediaDogBreedInfoDto.Title.Should().NotBeNull().And.Be("Akita");
        breedItem.WikipediaDogBreedInfoDto.Extract.Should().NotBeNull().And.Be("Akita is a Japanese name");
    }

    [Fact]
    public async Task GetDogBreeds_ThrowsException_Returns500()
    {
        // Arrange
        var mockService = new Mock<IDogAggregatorService>();
        var mockLogger = new Mock<ILogger<AggregationController>>();
        
        var filter = new DogBreedFilterDto { Page = 1 };
        
        mockService.Setup(s => s.GetAggregatedDogInfo(filter))
            .ThrowsAsync(new Exception("Something went wrong"));
        
        var controller = new AggregationController(mockService.Object, mockLogger.Object);
        
        // Act
        var result = await controller.GetDogBreeds(filter);
        
        // Assert
        var objectResult = result.Result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        objectResult.Value.Should().Be("An error occurred while processing your request.");
    }

    [Fact]
    public async Task GetDogBreeds_ReturnsOk_WithEmptyList()
    {
        // Arrange
        var mockService = new Mock<IDogAggregatorService>();
        var mockLogger = new Mock<ILogger<AggregationController>>();
        
        var filter = new DogBreedFilterDto { Page = 1 };
        
        var expected = new AggregatedDogBreedResponseDto
        {
            Breeds = new List<AggregatedDogBreedInfoDto> (),
            Pagination = new PaginationDto
            {
                Current = 1,
                Next = null,
                Last = 1,
                Records = 1
            }
        };
        
        mockService.Setup(s => s.GetAggregatedDogInfo(filter))
            .ReturnsAsync(expected);
        
        var controller = new AggregationController(mockService.Object, mockLogger.Object);
        
        // Act
        var result = await controller.GetDogBreeds(filter);
        
        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        
        var resultValue = okResult.Value as AggregatedDogBreedResponseDto;
        resultValue.Should().NotBeNull();
        resultValue.Breeds.Should().NotBeNull().And.BeEmpty();
    }
}