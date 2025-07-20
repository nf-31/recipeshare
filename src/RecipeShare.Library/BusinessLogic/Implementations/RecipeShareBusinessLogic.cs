using Microsoft.Extensions.Logging;
using RecipeShare.Library.Models;
using RecipeShare.Library.Repositories;

namespace RecipeShare.Library.BusinessLogic.Implementations;

public class RecipeShareBusinessLogic : IRecipeShareBusinessLogic
{
    private readonly IRecipeShareRepository _recipeShareRepository;
    
    private readonly ILogger<RecipeShareBusinessLogic> _logger;
    
    public RecipeShareBusinessLogic(
        IRecipeShareRepository recipeShareRepository, 
        ILogger<RecipeShareBusinessLogic> logger)
    {
        _recipeShareRepository = recipeShareRepository;
        _logger = logger;
    }

    public async Task<Recipe> GetRecipeById(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipe with ID {Id}", id);
        
        return await _recipeShareRepository.GetRecipeById(id, cancellationToken);
    }
    
    public async Task<Recipe> GetRecipeByTitle(string title, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipe with title {Title}", title);
        
        return await _recipeShareRepository.GetRecipeByTitle(title, cancellationToken);
    }

    public async Task<IEnumerable<Recipe>> GetRecipes(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipes");
        
        return await _recipeShareRepository.GetRecipes(cancellationToken);
    }

    public async Task<IEnumerable<Recipe>> GetRecipesByDietaryTag(string dietaryTag,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipes with dietary tag {DietaryTag}", dietaryTag);
        
        return await _recipeShareRepository.GetRecipeByDietaryTag(dietaryTag, cancellationToken);
    }
}