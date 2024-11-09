using LangCardsAPI.Requests;
using LangCardsAPI.Services;
using LangWordsApplication;
using Microsoft.AspNetCore.Mvc;

namespace LangCardsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    private readonly WordsManipulationManager _wordsManipulationManager;

    public WordsController(WordsManipulationManager cardsManipulationManager)
    {
        _wordsManipulationManager = cardsManipulationManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetWords()
    {
        var result = await _wordsManipulationManager.GetWordsAsync();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWord(Guid id)
    {
        Console.WriteLine(id);
        var result = await _wordsManipulationManager.GetWordByIdAsync(id);
        if(result != null)
            return Ok(result);
        
        return BadRequest(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWord(CreateWordRequest request)
    {
        var creationResult = await _wordsManipulationManager.CreateWordAsync(request.ToCreateCommandRequest());
        if (creationResult != null)
        {
            return CreatedAtAction(nameof(CreateWord),creationResult);
        }
        return BadRequest("qq"); 
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWord([FromBody]CreateWordRequest request, Guid id)
    {
        Console.WriteLine($"Updating of word {id}. New Value {request.Word}");
        var updatingResult = await _wordsManipulationManager.UpdateWordAsync(request.ToCreateCommandRequest(), id);
        Console.WriteLine("Value after updating");
        Console.WriteLine(updatingResult.Id);
        if (updatingResult != null)
        {
            return Ok(updatingResult);
        }
        return BadRequest(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWord(Guid id)
    {
        try
        {
            await _wordsManipulationManager.DeleteWordAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}