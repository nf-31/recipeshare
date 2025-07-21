using RecipeShare.Library.Models;
using RecipeShare.Library.Models.RequestModels;
using RecipeShare.Library.Models.ResponseModels;

namespace RecipeShare.Library.Repositories;

public interface IRecipeShareRepository
{
    Task<RecipeResponse?> GetRecipeById(int id, CancellationToken cancellationToken);
    
    Task<RecipeResponse?> GetRecipeByTitle(string title, CancellationToken cancellationToken);
    
    Task<IEnumerable<RecipeResponse?>> GetRecipes(CancellationToken cancellationToken);
    
    Task<IEnumerable<RecipeResponse?>> GetRecipeByDietaryTag(string dietaryTag, CancellationToken cancellationToken);

    Task AddRecipe(RecipeRequest request, CancellationToken cancellationToken);
}