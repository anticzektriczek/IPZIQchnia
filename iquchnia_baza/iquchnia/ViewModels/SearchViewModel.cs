using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using iquchnia.Services;
using iquchnia.Views;

namespace iquchnia.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    private List<string> _allIngredients = new();

    [ObservableProperty]
    private string currentIngredient = string.Empty;

    [ObservableProperty]        
    private bool isSuggestionsVisible;

    public ObservableCollection<string> Ingredients { get; } = new();
    public ObservableCollection<string> Suggestions { get; } = new();

    public SearchViewModel(IRecipeService recipeService)
    {
        _recipeService = recipeService;
        _ = LoadIngredientsAsync();
    }

    private async Task LoadIngredientsAsync()
    {
        _allIngredients = await _recipeService.GetAllIngredientsAsync();
    }

    // Wywoływane automatycznie gdy CurrentIngredient się zmienia
    partial void OnCurrentIngredientChanged(string value)
    {
        UpdateSuggestions(value);
    }

    private void UpdateSuggestions(string input)
    {
        Suggestions.Clear();

        if (string.IsNullOrWhiteSpace(input) || input.Length < 2)
        {
            IsSuggestionsVisible = false;
            return;
        }

        var matches = _allIngredients
            .Where(i => i.StartsWith(input, StringComparison.OrdinalIgnoreCase)
                     && !Ingredients.Contains(i, StringComparer.OrdinalIgnoreCase))
            .Take(6);

        foreach (var match in matches)
            Suggestions.Add(match);

        IsSuggestionsVisible = Suggestions.Count > 0;
    }

    [RelayCommand]
    private void SelectSuggestion(string suggestion)
    {
        if (string.IsNullOrWhiteSpace(suggestion)) return;
        if (!Ingredients.Contains(suggestion, StringComparer.OrdinalIgnoreCase))
            Ingredients.Add(suggestion);

        CurrentIngredient = string.Empty;
        Suggestions.Clear();
        IsSuggestionsVisible = false;
    }

    [RelayCommand]
    private void AddIngredient()
    {
        // Zezwól tylko na składniki z bazy
        var match = _allIngredients
            .FirstOrDefault(i => i.Equals(CurrentIngredient.Trim(), StringComparison.OrdinalIgnoreCase));

        if (match is null) return;
        if (!Ingredients.Contains(match, StringComparer.OrdinalIgnoreCase))
            Ingredients.Add(match);

        CurrentIngredient = string.Empty;
        Suggestions.Clear();
        IsSuggestionsVisible = false;
    }

    [RelayCommand]
    private void RemoveIngredient(string ingredient)
    {
        if (Ingredients.Contains(ingredient))
            Ingredients.Remove(ingredient);
    }

    [RelayCommand]
    private async Task Search()
    {
        if (Ingredients.Count == 0) return;

        var ingredientsText = string.Join(",", Ingredients);
        await Shell.Current.GoToAsync(nameof(SearchResultsPage), true,
            new Dictionary<string, object>
            {
                ["Ingredients"] = ingredientsText
            });
    }
}