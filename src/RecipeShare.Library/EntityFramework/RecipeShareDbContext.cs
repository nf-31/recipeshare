using Microsoft.EntityFrameworkCore;
using RecipeShare.Library.Models;
using RecipeShare.Library.Models.Entities;

namespace RecipeShare.Library.EntityFramework;

public class RecipeShareDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<DietaryTag> DietaryTags { get; set; }

    public RecipeShareDbContext(DbContextOptions<RecipeShareDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>()
            .HasOne<Recipe>()
            .WithMany(r => r.Ingredients)
            .HasForeignKey(i => i.RId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Ingredient>()
            .HasIndex(i => i.RId);

        modelBuilder.Entity<Step>()
            .HasOne<Recipe>()
            .WithMany(r => r.Steps)
            .HasForeignKey(s => s.RId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Step>()
            .HasIndex(s => s.RId);

        modelBuilder.Entity<DietaryTag>()
            .HasOne<Recipe>()
            .WithMany(r => r.DietaryTags)
            .HasForeignKey(d => d.RId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DietaryTag>()
            .HasIndex(d => d.RId);
        
        
        // Add decimal precision configuration
        modelBuilder.Entity<Ingredient>()
            .Property(i => i.Quantity)
            .HasPrecision(18, 2);
        
        
        // Seed Recipes
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe { Id = 1, Title = "Classic Spaghetti Bolognese", CookingTime = 45 },
            new Recipe { Id = 2, Title = "Vegan Chickpea Curry", CookingTime = 30 },
            new Recipe { Id = 3, Title = "Grilled Chicken Salad", CookingTime = 20 }
        );
        
        // Seed Ingredients
        modelBuilder.Entity<Ingredient>().HasData(
            // Recipe 1 ingredients
            new Ingredient { Id = 1, RId = 1, Name = "Spaghetti", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 2, RId = 1, Name = "Minced Beef", Quantity = 300, Unit = "g" },
            new Ingredient { Id = 3, RId = 1, Name = "Tomato Sauce", Quantity = 150, Unit = "ml" },
            new Ingredient { Id = 4, RId = 1, Name = "Onion", Quantity = 1, Unit = "unit" },
        
            // Recipe 2 ingredients
            new Ingredient { Id = 5, RId = 2, Name = "Chickpeas", Quantity = 400, Unit = "g" },
            new Ingredient { Id = 6, RId = 2, Name = "Coconut Milk", Quantity = 200, Unit = "ml" },
            new Ingredient { Id = 7, RId = 2, Name = "Curry Powder", Quantity = 2, Unit = "tsp" },
            new Ingredient { Id = 8, RId = 2, Name = "Spinach", Quantity = 100, Unit = "g" },
        
            // Recipe 3 ingredients
            new Ingredient { Id = 9, RId = 3, Name = "Chicken Breast", Quantity = 250, Unit = "g" },
            new Ingredient { Id = 10, RId = 3, Name = "Mixed Greens", Quantity = 100, Unit = "g" },
            new Ingredient { Id = 11, RId = 3, Name = "Cherry Tomatoes", Quantity = 10, Unit = "units" },
            new Ingredient { Id = 12, RId = 3, Name = "Olive Oil", Quantity = 1, Unit = "tbsp" }
        );
        
        // Seed Steps
        modelBuilder.Entity<Step>().HasData(
            // Recipe 1 steps
            new Step { Id = 1, RId = 1, StepNumber = 1, Text = "Boil spaghetti until al dente." },
            new Step { Id = 2, RId = 1, StepNumber = 2, Text = "Cook minced beef in a pan until browned." },
            new Step { Id = 3, RId = 1, StepNumber = 3, Text = "Add onion and tomato sauce. Simmer." },
            new Step { Id = 4, RId = 1, StepNumber = 4, Text = "Serve sauce over spaghetti." },
        
            // Recipe 2 steps
            new Step { Id = 5, RId = 2, StepNumber = 1, Text = "Sauté spices." },
            new Step { Id = 6, RId = 2, StepNumber = 2, Text = "Add chickpeas and coconut milk." },
            new Step { Id = 7, RId = 2, StepNumber = 3, Text = "Simmer and add spinach." },
            new Step { Id = 8, RId = 2, StepNumber = 4, Text = "Serve hot with rice." },
        
            // Recipe 3 steps
            new Step { Id = 9, RId = 3, StepNumber = 1, Text = "Grill chicken until cooked through." },
            new Step { Id = 10, RId = 3, StepNumber = 2, Text = "Toss greens and tomatoes in a bowl." },
            new Step { Id = 11, RId = 3, StepNumber = 3, Text = "Top with sliced chicken and drizzle olive oil." }
        );
        
        // Seed DietaryTags
        modelBuilder.Entity<DietaryTag>().HasData(
            // Recipe 1 tags
            new DietaryTag { Id = 1, RId = 1, Name = "High Protein" },
        
            // Recipe 2 tags
            new DietaryTag { Id = 2, RId = 2, Name = "Vegan" },
            new DietaryTag { Id = 3, RId = 2, Name = "Gluten-Free" },
        
            // Recipe 3 tags
            new DietaryTag { Id = 4, RId = 3, Name = "Low Carb" },
            new DietaryTag { Id = 5, RId = 3, Name = "Gluten-Free" }
        );
    }
}