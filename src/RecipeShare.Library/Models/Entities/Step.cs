using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeShare.Library.Models.Entities;
public class Step
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Recipe))]
    public int RId { get; set; }

    public int StepNumber { get; set; }

    public string Text { get; set; }
    public Recipe? Recipe { get; set; }
}
