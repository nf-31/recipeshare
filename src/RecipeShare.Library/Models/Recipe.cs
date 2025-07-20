using System.ComponentModel.DataAnnotations;
using RecipeShare.Library.Models.Entities;

namespace RecipeShare.Library.Models;

public class Recipe
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public int CookingTime { get; set; }

    public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    
    public IEnumerable<Step> Steps { get; set; } = new List<Step>();
    
    public IEnumerable<DietaryTag> DietaryTags { get; set; } = new List<DietaryTag>();
}
