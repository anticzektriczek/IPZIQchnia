using CommunityToolkit.Mvvm.ComponentModel;
using iquchnia.Models;
using iquchnia.Services;
using System.Collections.ObjectModel;

namespace iquchnia.ViewModels;

public partial class RecipesViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    public ObservableCollection<Recipe> AllRecipes { get; } = new();

    public RecipesViewModel(IRecipeService recipeService)
    {
        _recipeService = recipeService;
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        var recipes = await _recipeService.GetRecipesAsync();

        AllRecipes.Clear();
        foreach (var recipe in recipes)
        {
            AllRecipes.Add(recipe);
        }
    }
}