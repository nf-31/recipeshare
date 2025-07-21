namespace RecipeShare.Library.Models.RequestModels;

public class RecipeRequest
{
    public string Title { get; set; }
    public int CookingTime { get; set; }
    public List<AddIngredientRequest> Ingredients { get; set; } = new();
    public List<AddStepRequest> Steps { get; set; } = new();
    public List<string> DietaryTags { get; set; } = new();
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