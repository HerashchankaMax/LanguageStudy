using LangCardsAPI.Requests;
using LangCardsApplication;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCard(Guid id)
    {
        var result = await _cardsManipulationManager.GetCardByIdAsync(id);
        if(result != null)
            return Ok(result);
        
        return BadRequest(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCard(CreateCardRequest request)
    {
        var creationResult = await _cardsManipulationManager.CreateCardAsync(request.ToCreateCommandRequest());
        if (creationResult != null)
        {
            return Ok(creationResult);
        }
        return BadRequest(); 
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> CreateUpdate(CreateCardRequest request, Guid id)
    {
        var updatingResult = await _cardsManipulationManager.UpdateCardAsync(request.ToCreateCommandRequest(), id);
        if (updatingResult != null)
        {
            return Ok(updatingResult);
        }
        return BadRequest(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCard(Guid id)
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