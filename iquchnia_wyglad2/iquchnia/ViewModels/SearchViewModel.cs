using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using iquchnia.Views;

namespace iquchnia.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    [ObservableProperty]
    private string currentIngredient;

    public ObservableCollection<string> Ingredients { get; } = new();

    [RelayCommand]
    private void AddIngredient()
    {
        if (string.IsNullOrWhiteSpace(CurrentIngredient))
            return;

        var ingredient = CurrentIngredient.Trim();

        if (!Ingredients.Contains(ingredient))
            Ingredients.Add(ingredient);

        CurrentIngredient = string.Empty;
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
        if (Ingredients.Count == 0)
            return;

        // zamiana listy na string do przekazania
        var ingredientsText = string.Join(",", Ingredients);

        await Shell.Current.GoToAsync(nameof(SearchResultsPage), true,
            new Dictionary<string, object>
            {
                ["Ingredients"] = ingredientsText
            });
    }
}