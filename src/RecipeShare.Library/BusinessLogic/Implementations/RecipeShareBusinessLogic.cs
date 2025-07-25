using Microsoft.Extensions.Logging;
using RecipeShare.Library.Models.RequestModels;
using RecipeShare.Library.Models.ResponseModels;
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

    #region Get Recipes Business Logic

    public async Task<RecipeResponse?> GetRecipeById(int id, CancellationToken cancellationToken)
    {
        if (id is <= 0 or >= int.MaxValue)
        {
            _logger.LogWarning("Invalid recipe ID: {Id}", id);
            return null;
        }

        _logger.LogInformation("Getting recipe with ID {Id}", id);

        var recipe = await _recipeShareRepository.GetRecipeById(id, cancellationToken);

        if (recipe is not null) return recipe;

        _logger.LogWarning("No recipe found with ID {Id}", id);
        return null;
    }

    public async Task<RecipeResponse?> GetRecipeByTitle(string title, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            _logger.LogWarning("Invalid recipe title");
            return null;
        }

        _logger.LogInformation("Getting recipe with title {Title}", title);

        var recipe = await _recipeShareRepository.GetRecipeByTitle(title, cancellationToken);

        if (recipe is not null) return recipe;

        _logger.LogWarning("No recipe found with title {Title}", title);
        return null;
    }

    public async Task<IEnumerable<RecipeResponse?>> GetRecipes(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipes");

        var recipes = await _recipeShareRepository.GetRecipes(cancellationToken);

        if (recipes is not null && recipes.Any()) return recipes;

        _logger.LogWarning("No recipes found");
        return [];
    }

    public async Task<IEnumerable<RecipeResponse?>> GetRecipesByDietaryTag(string dietaryTag,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dietaryTag))
        {
            _logger.LogWarning("Invalid dietary tag");
            return [];
        }

        _logger.LogInformation("Getting recipes with dietary tag {DietaryTag}", dietaryTag);

        var recipes = await _recipeShareRepository.GetRecipeByDietaryTag(dietaryTag, cancellationToken);

        if (recipes is not null && recipes.Any()) return recipes;

        _logger.LogWarning("No recipes found with dietary tag {DietaryTag}", dietaryTag);
        return [];
    }

    #endregion

    #region Modify Recipes Business Logic

    public async Task AddRecipe(RecipeRequest recipe, CancellationToken cancellationToken)
    {
            if (recipe is null)
            {
                _logger.LogWarning("Invalid recipe");
                throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");
            }

            if (recipe.CookingTime <= 0)
            {
                _logger.LogWarning("Invalid cooking time");
                throw new Exception("Recipe cooking time must be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(recipe.Title))
            {
                _logger.LogWarning("Invalid recipe title");
                throw new Exception("Recipe title is required");
            }

            _logger.LogInformation("Adding new recipe with title {Title}", recipe.Title);

            await _recipeShareRepository.AddRecipe(recipe, cancellationToken);
            
    }

    public async Task UpdateRecipeById(int id, RecipeRequest recipe, CancellationToken cancellationToken)
    {
            if (id is <= 0 or >= int.MaxValue)
            {
                _logger.LogWarning("Invalid recipe ID: {Id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid recipe ID");
            }
            
            _logger.LogInformation("Updating recipe with ID {Id}", id);
            await _recipeShareRepository.UpdateRecipeById(id, recipe, cancellationToken);
    }

    public async Task UpdateRecipeByTitle(RecipeRequest recipe, CancellationToken cancellationToken)
    {
            if (string.IsNullOrWhiteSpace(recipe.Title))
            {
                _logger.LogWarning("Invalid recipe title");
                throw new ArgumentNullException(nameof(recipe.Title), "Recipe title must be present");
            }
            
            
            
            _logger.LogInformation("Updating recipe with title {Title}", recipe.Title);
            await _recipeShareRepository.UpdateRecipeByTitle(recipe, cancellationToken);
    }

    #endregion
    
    #region Delete Recipes Business Logic

    public async Task DeleteRecipe(int id, CancellationToken cancellationToken)
    {
        if (id is <= 0 or >= int.MaxValue)
        {
            _logger.LogWarning("Invalid recipe ID: {Id}", id);
            throw new ArgumentOutOfRangeException(nameof(id), "Invalid recipe ID");
        }
        
        _logger.LogInformation("Deleting recipe with ID {Id}", id);
        
        await _recipeShareRepository.DeleteRecipe(id, cancellationToken);
    }
    # endregion
}