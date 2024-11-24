using LangCardsAPI.Requests;
using LangCardsAPI.Services;
using LangCardsApplication;
using LangCardsApplication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LangCardsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly CollectionsManipulationManager _collectionsManipulationManager;
    private readonly ILogger<CollectionsController> _logger;

    public CollectionsController(CollectionsManipulationManager cardsManipulationManager,
        ILogger<CollectionsController> logger)
    {
        _collectionsManipulationManager = cardsManipulationManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetCards()
    {
        var result = await _collectionsManipulationManager.GetCollections();
        return Ok(result);
    }

    [HttpGet("searchTerm")]
    public async Task<IActionResult> GetByValue(string value)
    {
        try
        {
            var result = await _collectionsManipulationManager.GetCollectionsByFilter(value);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCard(Guid id)
    {
        var result = await _collectionsManipulationManager.GetCollectionByIdAsync(id);
        if (result != null)
            return Ok(result);

        return BadRequest(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] CreateCollectionRequest request)
    {
        var creationResult = await _collectionsManipulationManager.CreateCollection(request.ToCreateCommandRequest());
        if (creationResult != null) return Ok(creationResult);
        return BadRequest("qq");
    }

    [HttpPut]
    public async Task<IActionResult> CreateUpdate([FromBody] CollectionDataCommandRequest request, [FromQuery] Guid id)
    {
        try
        {
            var requestWords = request.Words;

            var updatingResult = await _collectionsManipulationManager.UpdateCollectionAsync(request, id);
            if (updatingResult != null) return Ok(updatingResult);
        }
        catch (Exception e)
        {
        }

        return BadRequest();
    }

    [HttpPatch]
    public async Task<IActionResult> AddWord([FromBody] CreateWordCommandRequest request, [FromQuery] Guid id)
    {
        _logger.LogInformation($"Trying to add word {request.Word} to collection {id}");
        _logger.LogInformation($"{request.Word}, {request.Translation}, {request.Definition}");
        var result = await _collectionsManipulationManager.AddWord(id, request);
        if (result)
        {
            _logger.LogInformation($"Added word: {request.Word}");
            return Ok(result);
        }

        _logger.LogError($"Failed to add word: {request.Word}");
        return BadRequest($"Word {request.Word} is not added");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCard([FromQuery] Guid id)
    {
        try
        {
            await _collectionsManipulationManager.DeleteCollection(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}