using LangCardsAPI.Requests;
using LangCardsAPI.Services;
using LangCardsApplication;
using LangWordsApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LangCardsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    private readonly CollectionsManipulationManager _collectionsManipulationManager;
    private readonly ILogger<WordsController> _logger;
    private readonly WordsManipulationManager _wordsManipulationManager;

    public WordsController(
        WordsManipulationManager wordsManipulationManager,
        CollectionsManipulationManager collectionsManipulationManager,
        ILogger<WordsController> logger)
    {
        _wordsManipulationManager = wordsManipulationManager;
        _collectionsManipulationManager = collectionsManipulationManager;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
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
        if (result != null)
            return Ok(result);

        return BadRequest(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWord(CreateWordRequest request)
    {
        var creationResult = await _wordsManipulationManager.CreateWordAsync(request.ToCreateCommandRequest());
        if (creationResult != null) return CreatedAtAction(nameof(CreateWord), creationResult);
        return BadRequest("qq");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWord([FromBody] CreateWordRequest request, Guid id)
    {
        Console.WriteLine($"Updating of word {id}. New Value {request.Word}");
        var updatingResult = await _wordsManipulationManager.UpdateWordAsync(request.ToCreateCommandRequest(), id);
        Console.WriteLine("Value after updating");
        Console.WriteLine(updatingResult.Id);
        if (updatingResult != null) return Ok(updatingResult);
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWord(Guid id)
    {
        try
        {
            var collections = await _collectionsManipulationManager.GetCollections();
            var canBeRemoved = collections.Any(c => !c.Words.Any(w => w.Id == id));
            if (!canBeRemoved)
            {
                _logger.LogInformation("Word {id} cannot be removed because it is used in some collections", id);
                return BadRequest("Word is used in some collections");
            }

            await _wordsManipulationManager.DeleteWordAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task RemoveUnusedWords()
    {
        var words = await _wordsManipulationManager.GetWordsAsync();
        var collections = await _collectionsManipulationManager.GetCollections();
        var usedWords = collections.SelectMany(collection => collection.Words.Select(w => w.Id)).ToList();
        var canBeRemoved = words.Where(currentWord => !usedWords.Contains(currentWord.Id)).ToList();
        canBeRemoved.ForEach(w => _logger.LogInformation("Word {word} is not used in any collection", w.Value));
        _logger.LogInformation("Words to remove: {wordsCount}", canBeRemoved.Count);
    }
}