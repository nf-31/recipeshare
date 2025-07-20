using RecipeShare.Library.Models;

namespace RecipeShare.Library.Repositories;

public interface IRecipeShareRepository
{
    Task<Recipe> GetRecipeById(int id, CancellationToken cancellationToken);
    
    Task<Recipe> GetRecipeByTitle(string title, CancellationToken cancellationToken);
    
    Task<IEnumerable<Recipe>> GetRecipes(CancellationToken cancellationToken);
    
    Task<IEnumerable<Recipe>> GetRecipeByDietaryTag(string dietaryTag, CancellationToken cancellationToken);
}