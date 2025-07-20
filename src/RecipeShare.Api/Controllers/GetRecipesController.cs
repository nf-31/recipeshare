using Microsoft.AspNetCore.Mvc;
using RecipeShare.Library.BusinessLogic;

namespace RecipeShare.Controllers;

[ApiController]
[Route("api/recipes")]
public class GetRecipesController : ControllerBase
{
    private readonly IRecipeShareBusinessLogic _recipeShareBusinessLogic;
    
    public GetRecipesController(IRecipeShareBusinessLogic recipeShareBusinessLogic)
    {
        _recipeShareBusinessLogic = recipeShareBusinessLogic;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRecipes(CancellationToken cancellationToken)
    {
        try
        {
            var recipes = await _recipeShareBusinessLogic.GetRecipes(cancellationToken);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRecipeById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var recipe = await _recipeShareBusinessLogic.GetRecipeById(id, cancellationToken);
            return Ok(recipe);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpGet]
    [Route("title")]
    public async Task<IActionResult> GetRecipeByTitle([FromQuery] string title, CancellationToken cancellationToken)
    {
        try
        {
            var recipe = await _recipeShareBusinessLogic.GetRecipeByTitle(title, cancellationToken);
            return Ok(recipe);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpGet]
    [Route("dietaryTag")]
    public async Task<IActionResult> GetRecipesByDietaryTag([FromQuery] string dietaryTag, CancellationToken cancellationToken)
    {
        try
        {
            var recipes = await _recipeShareBusinessLogic.GetRecipesByDietaryTag(dietaryTag, cancellationToken);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}