using RecipeShare.Library.Models;

namespace RecipeShare.Library.BusinessLogic;

public interface IRecipeShareBusinessLogic
{
    Task<Recipe> GetRecipeById(int id, CancellationToken cancellationToken);
    
    Task<Recipe> GetRecipeByTitle(string title, CancellationToken cancellationToken);
    
    Task<IEnumerable<Recipe>> GetRecipes(CancellationToken cancellationToken);
    
    Task<IEnumerable<Recipe>> GetRecipesByDietaryTag(string dietaryTag, CancellationToken cancellationToken);
}