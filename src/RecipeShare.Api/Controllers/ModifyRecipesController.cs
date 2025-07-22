using Microsoft.AspNetCore.Mvc;
using RecipeShare.Library.BusinessLogic;
using RecipeShare.Library.Models.RequestModels;

namespace RecipeShare.Controllers;

[ApiController]
[Route("api/recipes")]
public class ModifyRecipesController : ControllerBase
{
    private readonly IRecipeShareBusinessLogic _recipeShareBusinessLogic;

    public ModifyRecipesController(IRecipeShareBusinessLogic recipeShareBusinessLogic)
    {
        _recipeShareBusinessLogic = recipeShareBusinessLogic;
    }
    
    /// <summary>
    /// Add a new recipe.
    /// </summary>
    /// <param name="recipe"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddRecipe([FromBody] RecipeRequest recipe, CancellationToken cancellationToken)
    {
        try
        {
            await _recipeShareBusinessLogic.AddRecipe(recipe, cancellationToken);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Update a recipe by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="recipe"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch]
    [Route("update/{id}")]
    public async Task<IActionResult> UpdateRecipebyId([FromRoute] int id, [FromBody] RecipeRequest recipe, CancellationToken cancellationToken)
    {
        try
        {
            await _recipeShareBusinessLogic.UpdateRecipeById(id, recipe, cancellationToken);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    /// <summary>
    /// Update a recipe by title.
    /// </summary>
    /// <param name="recipe"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch]
    [Route("update/title")]
    public async Task<IActionResult> UpdateRecipeByTitle([FromBody] RecipeRequest recipe, CancellationToken cancellationToken)
    {
        try
        {
            await _recipeShareBusinessLogic.UpdateRecipeByTitle(recipe, cancellationToken);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}