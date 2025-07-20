using RecipeShare.Library.Models;

namespace RecipeShare.Library.Repositories.Implementations;

public class RecipeShareRepository : IRecipeShareRepository
{
    public RecipeShareRepository()
    {
        
    }

    public async Task<Recipe> GetRecipeById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Recipe> GetRecipeByTitle(string title, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Recipe>> GetRecipes(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Recipe>> GetRecipeByDietaryTag(string dietaryTag, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}