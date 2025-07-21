namespace RecipeShare.Library.Models.RequestModels;

public class RecipeRequest
{
    public string Title { get; set; }
    public int CookingTime { get; set; }
    public ICollection<AddIngredientRequest>? Ingredients { get; set; } = new List<AddIngredientRequest>();
    public ICollection<AddStepRequest>? Steps { get; set; } = new List<AddStepRequest>();
    public ICollection<string>? DietaryTags { get; set; } = new List<string>();
}

public class AddIngredientRequest
{
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
}

public class AddStepRequest
{
    public string Description { get; set; }
    public int StepNumber { get; set; }
}