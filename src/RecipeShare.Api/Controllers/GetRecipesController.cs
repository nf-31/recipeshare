using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeShare.Library.BusinessLogic;

namespace RecipeShare.Controllers;

[ApiController]
[Route("api/recipes")]
[Authorize]
public class GetRecipesController : ControllerBase
{
    private readonly IRecipeShareBusinessLogic _recipeShareBusinessLogic;
    
    public GetRecipesController(IRecipeShareBusinessLogic recipeShareBusinessLogic)
    {
        _recipeShareBusinessLogic = recipeShareBusinessLogic;
    }
    
    /// <summary>
    /// get all recipes.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
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
    
    /// <summary>
    /// Get recipe by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Get recipe by title.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Get recipes by dietary tag.
    /// </summary>
    /// <param name="dietaryTag"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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