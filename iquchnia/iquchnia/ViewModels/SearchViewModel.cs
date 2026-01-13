using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iquchnia.Models;
using iquchnia.Services;
using iquchnia.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace iquchnia.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;

    public SearchViewModel(IRecipeService recipeService)
    {
        _recipeService = recipeService;
        Recipes = new ObservableCollection<Recipe>();
    }

    [ObservableProperty]
    private string ingredientsText = string.Empty;

    public ObservableCollection<Recipe> Recipes { get; }

    [RelayCommand]
    private void Search()
    {
        Recipes.Clear();

        var ingredients = IngredientsText
            .Split(',', System.StringSplitOptions.RemoveEmptyEntries)
            .Select(i => i.Trim())
            .ToList();

        var results = _recipeService.SearchRecipes(ingredients);

        foreach (var recipe in results)
            Recipes.Add(recipe);
    }
    [RelayCommand]
    private async Task RecipeTapped(Recipe recipe)
    {
        if (recipe == null)
            return;

        await Shell.Current.GoToAsync(nameof(RecipeDetailsPage), true,
            new Dictionary<string, object>
            {
                ["Recipe"] = recipe
            });
    }
}
