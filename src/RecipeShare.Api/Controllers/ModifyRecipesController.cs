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
}