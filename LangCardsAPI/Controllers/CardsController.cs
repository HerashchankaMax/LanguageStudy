using LangCardsAPI.Requests;
using LangCardsAPI.Services;
using LangCardsApplication;
using Microsoft.AspNetCore.Mvc;

namespace LangCardsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly CardsManipulationManager _cardsManipulationManager;

    public CardsController(CardsManipulationManager cardsManipulationManager)
    {
        _cardsManipulationManager = cardsManipulationManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetCards()
    {
        var result = await _cardsManipulationManager.GetCardsAsync();
        return Ok(result);
    }

    [HttpGet("searchTerm")]
    public async Task<IActionResult> GetByValue(string value)
    {
        try
        {
            var result = await _cardsManipulationManager.GetFlashCardsByText(value);
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
        var result = await _cardsManipulationManager.GetCardByIdAsync(id);
        if(result != null)
            return Ok(result);
        
        return BadRequest(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody]CreateCardRequest request)
    {
        var creationResult = await _cardsManipulationManager.CreateCardAsync(request.ToCreateCommandRequest());
        if (creationResult != null)
        {
            return Ok(creationResult);
        }
        return BadRequest("qq"); 
    }
    
    [HttpPut]
    public async Task<IActionResult> CreateUpdate([FromBody] UpdateCardRequest request, [FromQuery] Guid id)
    {
        var updatingResult = await _cardsManipulationManager.UpdateCardAsync(request.WordId, id);
        if (updatingResult != null)
        {
            return Ok(updatingResult);
        }
        return BadRequest(); 
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCard([FromQuery] Guid id)
    {
        try
        {
            await _cardsManipulationManager.DeleteCardAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}