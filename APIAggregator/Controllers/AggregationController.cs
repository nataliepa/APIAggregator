using APIAggregator.Models.Dtos;
using APIAggregator.Services.Definitions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIAggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AggregationController : ControllerBase
{
    private readonly IDogAggregatorService _dogAggregatorService;
    private readonly ILogger<AggregationController> _logger;
    
    public AggregationController(IDogAggregatorService dogAggregatorService, ILogger<AggregationController> logger)
    {
        _dogAggregatorService = dogAggregatorService;
        _logger = logger;
    }

    /// <summary>
    /// Επιστρέφει λίστα με breeds σκύλων.
    /// </summary>
    [HttpGet("dogs")]
    [ProducesResponseType(typeof(List<AggregatedDogBreedInfoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<AggregatedDogBreedInfoDto?>>> GetDogBreeds(
        [FromQuery] int page = 1,
        string? name = null,
        bool? hypoallergenic = null,
        string? breedGroup = null,
        string? temperament = null
        )
    {
        try
        {
            var result = await _dogAggregatorService.GetAggregatedDogInfo(page);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving aggregated dog data for page {Page}", page);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}