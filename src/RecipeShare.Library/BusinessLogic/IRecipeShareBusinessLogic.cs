using RecipeShare.Library.Models;
using RecipeShare.Library.Models.ResponseModels;

namespace RecipeShare.Library.BusinessLogic;

public interface IRecipeShareBusinessLogic
{
    Task<RecipeResponse?> GetRecipeById(int id, CancellationToken cancellationToken);
    
    Task<RecipeResponse?> GetRecipeByTitle(string title, CancellationToken cancellationToken);
    
    Task<IEnumerable<RecipeResponse?>> GetRecipes(CancellationToken cancellationToken);
    
    Task<IEnumerable<RecipeResponse?>> GetRecipesByDietaryTag(string dietaryTag, CancellationToken cancellationToken);
}