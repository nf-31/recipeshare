using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeShare.Library.Models.Entities;

public class DietaryTag
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Recipe))]
    public int RId { get; set; }

    public string Name { get; set; }
    
}
