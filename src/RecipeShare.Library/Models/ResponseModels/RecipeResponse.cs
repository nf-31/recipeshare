namespace RecipeShare.Library.Models.ResponseModels;

public class RecipeResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int CookingTime { get; set; }
    public IEnumerable<IngredientResponse> Ingredients { get; set; }
    public IEnumerable<StepResponse> Steps { get; set; }
    public IEnumerable<DietaryTagResponse> DietaryTags { get; set; }
}

public class IngredientResponse
{
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
}

public class StepResponse
{
    public string Description { get; set; }
    public int StepNumber { get; set; }
}

public class DietaryTagResponse
{
    public string Name { get; set; }
}
