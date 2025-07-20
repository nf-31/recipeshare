using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeShare.Library.Models.Entities;

public class Ingredient
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Recipe))]
    public int RId { get; set; }

    [Required]
    public string Name { get; set; }

    public decimal Quantity { get; set; }

    public string Unit { get; set; }
    
}
