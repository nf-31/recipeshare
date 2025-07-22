using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeShare.Library.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookingTime", "Title" },
                values: new object[,]
                {
                    { 1, 45, "Classic Spaghetti Bolognese" },
                    { 2, 30, "Vegan Chickpea Curry" },
                    { 3, 20, "Grilled Chicken Salad" }
                });

            migrationBuilder.InsertData(
                table: "DietaryTags",
                columns: new[] { "Id", "Name", "RId", "RecipeId" },
                values: new object[,]
                {
                    { 1, "High Protein", 1, null },
                    { 2, "Vegan", 2, null },
                    { 3, "Gluten-Free", 2, null },
                    { 4, "Low Carb", 3, null },
                    { 5, "Gluten-Free", 3, null }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity", "RId", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 1, "Spaghetti", 200m, 1, null, "g" },
                    { 2, "Minced Beef", 300m, 1, null, "g" },
                    { 3, "Tomato Sauce", 150m, 1, null, "ml" },
                    { 4, "Onion", 1m, 1, null, "unit" },
                    { 5, "Chickpeas", 400m, 2, null, "g" },
                    { 6, "Coconut Milk", 200m, 2, null, "ml" },
                    { 7, "Curry Powder", 2m, 2, null, "tsp" },
                    { 8, "Spinach", 100m, 2, null, "g" },
                    { 9, "Chicken Breast", 250m, 3, null, "g" },
                    { 10, "Mixed Greens", 100m, 3, null, "g" },
                    { 11, "Cherry Tomatoes", 10m, 3, null, "units" },
                    { 12, "Olive Oil", 1m, 3, null, "tbsp" }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "RId", "RecipeId", "StepNumber", "Text" },
                values: new object[,]
                {
                    { 1, 1, null, 1, "Boil spaghetti until al dente." },
                    { 2, 1, null, 2, "Cook minced beef in a pan until browned." },
                    { 3, 1, null, 3, "Add onion and tomato sauce. Simmer." },
                    { 4, 1, null, 4, "Serve sauce over spaghetti." },
                    { 5, 2, null, 1, "Sauté spices." },
                    { 6, 2, null, 2, "Add chickpeas and coconut milk." },
                    { 7, 2, null, 3, "Simmer and add spinach." },
                    { 8, 2, null, 4, "Serve hot with rice." },
                    { 9, 3, null, 1, "Grill chicken until cooked through." },
                    { 10, 3, null, 2, "Toss greens and tomatoes in a bowl." },
                    { 11, 3, null, 3, "Top with sliced chicken and drizzle olive oil." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DietaryTags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DietaryTags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DietaryTags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DietaryTags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DietaryTags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
