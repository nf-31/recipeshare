using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeShare.Library.BusinessLogic;

namespace RecipeShare.Controllers;

[ApiController]
[Route("api/recipes")]
[Authorize]
public class DeleteRecipesController : ControllerBase
{
    private readonly IRecipeShareBusinessLogic _recipeShareBusinessLogic;

    public DeleteRecipesController(IRecipeShareBusinessLogic recipeShareBusinessLogic)
    {
        _recipeShareBusinessLogic = recipeShareBusinessLogic;
    }
    
    /// <summary>
    /// Delete recipe by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> DeleteRecipe(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            await _recipeShareBusinessLogic.DeleteRecipe(id, cancellationToken);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}