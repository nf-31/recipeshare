using Microsoft.Extensions.Logging;
using Moq;
using RecipeShare.Library.BusinessLogic.Implementations;
using RecipeShare.Library.Models.RequestModels;
using RecipeShare.Library.Models.ResponseModels;
using RecipeShare.Library.Repositories;

namespace RecipeShare.Tests;

[TestFixture]
public class RecipeShareBusinessLogicTests
{
    private Mock<IRecipeShareRepository> _repositoryMock;
    private Mock<ILogger<RecipeShareBusinessLogic>> _loggerMock;
    private RecipeShareBusinessLogic _businessLogic;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IRecipeShareRepository>();
        _loggerMock = new Mock<ILogger<RecipeShareBusinessLogic>>();
        _businessLogic = new RecipeShareBusinessLogic(_repositoryMock.Object, _loggerMock.Object);
        _cancellationToken = CancellationToken.None;
    }

    #region GetRecipeById Tests

    [Test]
    public async Task GetRecipeById_ValidId_ReturnsRecipe()
    {
        // Arrange
        var expectedRecipe = CreateRecipeResponse();
        _repositoryMock.Setup(r => r.GetRecipeById(1, _cancellationToken))
            .ReturnsAsync(expectedRecipe);

        // Act
        var result = await _businessLogic.GetRecipeById(1, _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(expectedRecipe.Title));
    }

    [Test]
    public async Task GetRecipeById_InvalidId_ReturnsNull()
    {
        // Arrange
        int invalidId = -1;

        // Act
        var result = await _businessLogic.GetRecipeById(invalidId, _cancellationToken);

        // Assert
        Assert.That(result, Is.Null);
        _repositoryMock.Verify(r => r.GetRecipeById(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task GetRecipeById_NonExistentId_ReturnsNull()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetRecipeById(1, _cancellationToken))
            .ReturnsAsync((RecipeResponse)null);

        // Act
        var result = await _businessLogic.GetRecipeById(1, _cancellationToken);

        // Assert
        Assert.That(result, Is.Null);
    }

    #endregion

    #region GetRecipeByTitle Tests

    [Test]
    public async Task GetRecipeByTitle_ValidTitle_ReturnsRecipe()
    {
        // Arrange
        var expectedRecipe = CreateRecipeResponse();
        _repositoryMock.Setup(r => r.GetRecipeByTitle("Test Recipe", _cancellationToken))
            .ReturnsAsync(expectedRecipe);

        // Act
        var result = await _businessLogic.GetRecipeByTitle("Test Recipe", _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo(expectedRecipe.Title));
    }

    [Test]
    public async Task GetRecipeByTitle_EmptyTitle_ReturnsNull()
    {
        // Act
        var result = await _businessLogic.GetRecipeByTitle("", _cancellationToken);

        // Assert
        Assert.That(result, Is.Null);
        _repositoryMock.Verify(r => r.GetRecipeByTitle(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task GetRecipeByTitle_NonExistentTitle_ReturnsNull()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetRecipeByTitle("Nonexistent", _cancellationToken))
            .ReturnsAsync((RecipeResponse)null);

        // Act
        var result = await _businessLogic.GetRecipeByTitle("Nonexistent", _cancellationToken);

        // Assert
        Assert.That(result, Is.Null);
    }

    #endregion

    #region GetRecipes Tests

    [Test]
    public async Task GetRecipes_RecipesExist_ReturnsAllRecipes()
    {
        // Arrange
        var expectedRecipes = new List<RecipeResponse>
        {
            CreateRecipeResponse(),
            CreateRecipeResponse()
        };
        _repositoryMock.Setup(r => r.GetRecipes(_cancellationToken))
            .ReturnsAsync(expectedRecipes);

        // Act
        var result = await _businessLogic.GetRecipes(_cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetRecipes_NoRecipesExist_ReturnsEmptyList()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetRecipes(_cancellationToken))
            .ReturnsAsync(new List<RecipeResponse>());

        // Act
        var result = await _businessLogic.GetRecipes(_cancellationToken);

        // Assert
        Assert.That(result, Is.Empty);
    }

    #endregion

    #region GetRecipesByDietaryTag Tests

    [Test]
    public async Task GetRecipesByDietaryTag_ValidTag_ReturnsMatchingRecipes()
    {
        // Arrange
        var expectedRecipes = new List<RecipeResponse>
        {
            CreateRecipeResponse(),
            CreateRecipeResponse()
        };
        _repositoryMock.Setup(r => r.GetRecipeByDietaryTag("vegetarian", _cancellationToken))
            .ReturnsAsync(expectedRecipes);

        // Act
        var result = await _businessLogic.GetRecipesByDietaryTag("vegetarian", _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetRecipesByDietaryTag_EmptyTag_ReturnsEmptyList()
    {
        // Act
        var result = await _businessLogic.GetRecipesByDietaryTag("", _cancellationToken);

        // Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetRecipeByDietaryTag(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Test]
    public async Task GetRecipesByDietaryTag_NoMatchingRecipes_ReturnsEmptyList()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetRecipeByDietaryTag("vegan", _cancellationToken))
            .ReturnsAsync(new List<RecipeResponse>());

        // Act
        var result = await _businessLogic.GetRecipesByDietaryTag("vegan", _cancellationToken);

        // Assert
        Assert.That(result, Is.Empty);
    }

    #endregion

    #region Helper Methods

    private RecipeResponse CreateRecipeResponse()
    {
        return new RecipeResponse
        {
            Id = 1,
            Title = "Test Recipe",
            CookingTime = 30,
            Ingredients = new List<IngredientResponse>
            {
                new() { Name = "Ingredient 1", Quantity = 1, Unit = "pc" }
            },
            Steps = new List<StepResponse>
            {
                new() { Description = "Step 1", StepNumber = 1 }
            },
            DietaryTags = new List<DietaryTagResponse>
            {
                new() { Name = "vegetarian" }
            }
        };
    }

    #endregion

    #region AddRecipe Tests

    [Test]
    public async Task AddRecipe_ValidRecipe_CallsRepository()
    {
        // Arrange
        var recipe = new RecipeRequest
        {
            Title = "Test Recipe",
            CookingTime = 30,
            Ingredients = new List<AddIngredientRequest>
            {
                new() { Name = "Ingredient 1", Quantity = 1, Unit = "pc" }
            }
        };

        // Act
        await _businessLogic.AddRecipe(recipe, _cancellationToken);

        // Assert
        _repositoryMock.Verify(r => r.AddRecipe(recipe, _cancellationToken), Times.Once);
        VerifyLog(LogLevel.Information, $"Adding new recipe with title {recipe.Title}");
    }

    [Test]
    public void AddRecipe_NullRecipe_ThrowsArgumentNullException()
    {
        // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _businessLogic.AddRecipe(null, _cancellationToken));
        Assert.That(ex.ParamName, Is.EqualTo("recipe"));
        VerifyLog(LogLevel.Warning, "Invalid recipe");
    }
    
    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    public void AddRecipe_InvalidCookingTime_ThrowsException(int cookingTime)
    {
        // Arrange
        
        var recipe = new RecipeRequest
        {
            Title = "Test Recipe",
            CookingTime = cookingTime,
            Ingredients = new List<AddIngredientRequest>
            {
                new() { Name = "Ingredient 1", Quantity = 1, Unit = "pc" }
            }
        };
        
        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(() => _businessLogic.AddRecipe(recipe, _cancellationToken));
        Assert.That(ex.Message, Is.EqualTo("Recipe cooking time must be greater than zero"));
        VerifyLog(LogLevel.Warning, "Invalid cooking time");
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void AddRecipe_InvalidTitle_ThrowsException(string? title)
    {
        // Arrange
        var recipe = new RecipeRequest
        {
            Title = title,
            CookingTime = 30,
            Ingredients = new List<AddIngredientRequest>
            {
                new() { Name = "Ingredient 1", Quantity = 1, Unit = "pc" }
            }
        };
        
        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(() => _businessLogic.AddRecipe(recipe, _cancellationToken));
        Assert.That(ex.Message, Is.EqualTo("Recipe title is required"));
        VerifyLog(LogLevel.Warning, "Invalid recipe title");
    }

    [Test]
    public void AddRecipe_RepositoryThrowsException()
    {
        // Arrange
        var recipe = new RecipeRequest { Title = "Test Recipe" };
        _repositoryMock.Setup(r => r.AddRecipe(recipe, _cancellationToken))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(() => _businessLogic.AddRecipe(recipe, _cancellationToken));
    }

    #endregion

    #region UpdateRecipeById Tests

    [Test]
    public async Task UpdateRecipeById_ValidIdAndRecipe_CallsRepository()
    {
        // Arrange
        var recipe = new RecipeRequest { Title = "Updated Recipe" };

        // Act
        await _businessLogic.UpdateRecipeById(1, recipe, _cancellationToken);

        // Assert
        _repositoryMock.Verify(r => r.UpdateRecipeById(1, recipe, _cancellationToken), Times.Once);
        VerifyLog(LogLevel.Information, "Updating recipe with ID 1");
    }

    [Test]
    public void UpdateRecipeById_InvalidId_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var recipe = new RecipeRequest { Title = "Test Recipe" };

        // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _businessLogic.UpdateRecipeById(-1, recipe, _cancellationToken));
        Assert.That(ex.ParamName, Is.EqualTo("id"));
        VerifyLog(LogLevel.Warning, "Invalid recipe ID: -1");
    }

    [Test]
    public void UpdateRecipeById_RepositoryThrowsException()
    {
        // Arrange
        var recipe = new RecipeRequest { Title = "Test Recipe" };
        _repositoryMock.Setup(r => r.UpdateRecipeById(1, recipe, _cancellationToken))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(() => _businessLogic.UpdateRecipeById(1, recipe, _cancellationToken));
    }

    #endregion

    #region UpdateRecipeByTitle Tests

    [Test]
    public async Task UpdateRecipeByTitle_ValidRecipe_CallsRepository()
    {
        // Arrange
        var recipe = new RecipeRequest { Title = "Existing Recipe" };

        // Act
        await _businessLogic.UpdateRecipeByTitle(recipe, _cancellationToken);

        // Assert
        _repositoryMock.Verify(r => r.UpdateRecipeByTitle(recipe, _cancellationToken), Times.Once);
        VerifyLog(LogLevel.Information, $"Updating recipe with title {recipe.Title}");
    }

    [Test]
    public void UpdateRecipeByTitle_EmptyTitle_ThrowsArgumentNullException()
    {
        // Arrange
        var recipe = new RecipeRequest { Title = "" };

        // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentNullException>(() =>
            _businessLogic.UpdateRecipeByTitle(recipe, _cancellationToken));
        Assert.That(ex.ParamName, Is.EqualTo("Title"));
        VerifyLog(LogLevel.Warning, "Invalid recipe title");
    }

    [Test]
    public void UpdateRecipeByTitle_RepositoryThrowsException()
    {
        // Arrange
        var recipe = new RecipeRequest { Title = "Test Recipe" };
        _repositoryMock.Setup(r => r.UpdateRecipeByTitle(recipe, _cancellationToken))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(() => _businessLogic.UpdateRecipeByTitle(recipe, _cancellationToken));
    }

    #endregion

    #region Helper Methods
    
    private void VerifyLog(LogLevel level, string message)
    {
        _loggerMock.Verify(
            x => x.Log(
                level,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString().Contains(message)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    #endregion
    
    #region Delete Recipe Tests

    [Test]
    public void DeleteRecipe_ThrowsArgumentOutOfRangeException()
    {
         // Act & Assert
        var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _businessLogic.DeleteRecipe(-1, _cancellationToken));
        Assert.That(ex.ParamName, Is.EqualTo("id"));
        VerifyLog(LogLevel.Warning, "Invalid recipe ID: -1");
        
    }
    
    [Test]
    public void DeleteRecipe_RepositoryThrowsException()
    {
        // Arrange
        _repositoryMock.Setup(r => r.DeleteRecipe(1, _cancellationToken))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(() => _businessLogic.DeleteRecipe(1, _cancellationToken));
    }
    
    [Test]
    public async Task DeleteRecipe_ValidId_CallsRepository()
    {
        // Arrange
        var id = 1;
        // Act
        await _businessLogic.DeleteRecipe(id, _cancellationToken);

        // Assert
        _repositoryMock.Verify(r => r.DeleteRecipe(id, _cancellationToken), Times.Once);
        VerifyLog(LogLevel.Information, $"Deleting recipe with ID {id}");
    }

    # endregion
}