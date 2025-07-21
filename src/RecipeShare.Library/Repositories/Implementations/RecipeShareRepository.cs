using Microsoft.EntityFrameworkCore;
using RecipeShare.Library.EntityFramework;
using RecipeShare.Library.Models.ResponseModels;

namespace RecipeShare.Library.Repositories.Implementations;

public class RecipeShareRepository : IRecipeShareRepository
{
    private readonly RecipeShareDbContext _context;
    public RecipeShareRepository(RecipeShareDbContext context)
    {
        _context = context;
    }

    public async Task<RecipeResponse?> GetRecipeById(int id, CancellationToken cancellationToken)
    {
        return await _context.Recipes
            .AsSplitQuery()
            .AsNoTracking() 
            .Where(r => r.Id == id)
            .Select(r => new RecipeResponse
            {
                Id = r.Id,
                Title = r.Title,
                CookingTime = r.CookingTime,
                Ingredients = r.Ingredients.Select(i => new IngredientResponse
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList(),
                Steps = r.Steps.OrderBy(s => s.StepNumber).Select(s => new StepResponse
                {
                    Description = s.Text,
                    StepNumber = s.StepNumber
                }).ToList(),
                DietaryTags = r.DietaryTags.Select(d => new DietaryTagResponse
                {
                    Name = d.Name
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

    }

    public async Task<RecipeResponse?> GetRecipeByTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Recipes
            .AsSplitQuery()
            .Where(r => r.Title == title)
            .Select(r => new RecipeResponse
            {
                Id = r.Id,
                Title = r.Title,
                CookingTime = r.CookingTime,
                Ingredients = r.Ingredients
                    .Select(i => new IngredientResponse
                    {
                        Name = i.Name,
                        Quantity = i.Quantity,
                        Unit = i.Unit
                    })
                    .ToList(), // Materialize collection
                Steps = r.Steps
                    .OrderBy(s => s.StepNumber) // Optional: enforce step order
                    .Select(s => new StepResponse
                    {
                        Description = s.Text,
                        StepNumber = s.StepNumber
                    })
                    .ToList(),
                DietaryTags = r.DietaryTags
                    .Select(d => new DietaryTagResponse
                    {
                        Name = d.Name
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

    }

    public async Task<IEnumerable<RecipeResponse?>> GetRecipes(CancellationToken cancellationToken)
    {
        return await _context.Recipes
            .AsSplitQuery()
            .AsNoTracking()
            .Select(r => new RecipeResponse
            {
                Id = r.Id,
                Title = r.Title,
                CookingTime = r.CookingTime,
                Ingredients = r.Ingredients.Select(i => new IngredientResponse
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList(),
                Steps = r.Steps.OrderBy(s => s.StepNumber).Select(s => new StepResponse
                {
                    Description = s.Text,
                    StepNumber = s.StepNumber
                }).ToList(),
                DietaryTags = r.DietaryTags.Select(d => new DietaryTagResponse
                {
                    Name = d.Name
                }).ToList()
            })
            .ToListAsync(cancellationToken);

    }

    public async Task<IEnumerable<RecipeResponse?>> GetRecipeByDietaryTag(string dietaryTag, CancellationToken cancellationToken)
    {
        return await _context.Recipes
            .AsSplitQuery() 
            .Where(r => r.DietaryTags.Any(t => t.Name == dietaryTag)) 
            .Select(r => new RecipeResponse
            {
                Id = r.Id,
                Title = r.Title,
                CookingTime = r.CookingTime,
                Ingredients = r.Ingredients
                    .Select(i => new IngredientResponse
                    {
                        Name = i.Name,
                        Quantity = i.Quantity,
                        Unit = i.Unit
                    })
                    .ToList(), 
                Steps = r.Steps
                    .Select(s => new StepResponse
                    {
                        Description = s.Text,
                        StepNumber = s.StepNumber
                    })
                    .ToList(),
                DietaryTags = r.DietaryTags
                    .Select(d => new DietaryTagResponse
                    {
                        Name = d.Name
                    })
                    .ToList()
            })
            .ToListAsync(cancellationToken);

    }
}