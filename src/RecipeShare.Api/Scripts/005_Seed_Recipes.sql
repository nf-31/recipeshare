-- Insert Recipes
INSERT INTO Recipe (Title, CookingTime) VALUES
                                            (N'Classic Spaghetti Bolognese', 45),
                                            (N'Vegan Chickpea Curry', 30),
                                            (N'Grilled Chicken Salad', 20);

-- Get inserted Recipe IDs (simulate identity for scripting)
DECLARE @Recipe1Id INT = (SELECT ID FROM Recipe WHERE Title = N'Classic Spaghetti Bolognese');
DECLARE @Recipe2Id INT = (SELECT ID FROM Recipe WHERE Title = N'Vegan Chickpea Curry');
DECLARE @Recipe3Id INT = (SELECT ID FROM Recipe WHERE Title = N'Grilled Chicken Salad');

-- Ingredients for Recipe 1
INSERT INTO Ingredients (RID, Name, Quantity, Unit) VALUES
                                                        (@Recipe1Id, N'Spaghetti', 200, N'g'),
                                                        (@Recipe1Id, N'Minced Beef', 300, N'g'),
                                                        (@Recipe1Id, N'Tomato Sauce', 150, N'ml'),
                                                        (@Recipe1Id, N'Onion', 1, N'unit');

-- Steps for Recipe 1
INSERT INTO Steps (RID, StepNumber, Text) VALUES
                                              (@Recipe1Id, 1, N'Boil spaghetti until al dente.'),
                                              (@Recipe1Id, 2, N'Cook minced beef in a pan until browned.'),
                                              (@Recipe1Id, 3, N'Add onion and tomato sauce. Simmer.'),
                                              (@Recipe1Id, 4, N'Serve sauce over spaghetti.');

-- Dietary Tags for Recipe 1
INSERT INTO DietaryTags (RID, Name) VALUES
    (@Recipe1Id, N'High Protein');

-- Ingredients for Recipe 2
INSERT INTO Ingredients (RID, Name, Quantity, Unit) VALUES
                                                        (@Recipe2Id, N'Chickpeas', 400, N'g'),
                                                        (@Recipe2Id, N'Coconut Milk', 200, N'ml'),
                                                        (@Recipe2Id, N'Curry Powder', 2, N'tsp'),
                                                        (@Recipe2Id, N'Spinach', 100, N'g');

-- Steps for Recipe 2
INSERT INTO Steps (RID, StepNumber, Text) VALUES
                                              (@Recipe2Id, 1, N'Sauté spices.'),
                                              (@Recipe2Id, 2, N'Add chickpeas and coconut milk.'),
                                              (@Recipe2Id, 3, N'Simmer and add spinach.'),
                                              (@Recipe2Id, 4, N'Serve hot with rice.');

-- Dietary Tags for Recipe 2
INSERT INTO DietaryTags (RID, Name) VALUES
                                        (@Recipe2Id, N'Vegan'),
                                        (@Recipe2Id, N'Gluten-Free');

-- Ingredients for Recipe 3
INSERT INTO Ingredients (RID, Name, Quantity, Unit) VALUES
                                                        (@Recipe3Id, N'Chicken Breast', 250, N'g'),
                                                        (@Recipe3Id, N'Mixed Greens', 100, N'g'),
                                                        (@Recipe3Id, N'Cherry Tomatoes', 10, N'units'),
                                                        (@Recipe3Id, N'Olive Oil', 1, N'tbsp');

-- Steps for Recipe 3
INSERT INTO Steps (RID, StepNumber, Text) VALUES
                                              (@Recipe3Id, 1, N'Grill chicken until cooked through.'),
                                              (@Recipe3Id, 2, N'Toss greens and tomatoes in a bowl.'),
                                              (@Recipe3Id, 3, N'Top with sliced chicken and drizzle olive oil.');

-- Dietary Tags for Recipe 3
INSERT INTO DietaryTags (RID, Name) VALUES
                                        (@Recipe3Id, N'Low Carb'),
                                        (@Recipe3Id, N'Gluten-Free');
