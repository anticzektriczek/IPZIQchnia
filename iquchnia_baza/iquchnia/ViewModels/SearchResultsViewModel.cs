using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iquchnia.Models;
using iquchnia.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iquchnia.ViewModels;

[QueryProperty(nameof(IngredientsText), "Ingredients")]
public partial class SearchResultsViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    private List<Recipe> _allFoundRecipes = new();

    public ObservableCollection<Recipe> Recipes { get; } = new();

    [ObservableProperty]
    private string ingredientsText;

    // WŁAŚCIWOŚCI DLA FILTRÓW
    [ObservableProperty] private bool areFiltersVisible;
    [ObservableProperty] private string filtersArrow = "▼";
    [ObservableProperty] private bool czyWeganskie;
    [ObservableProperty] private bool czyWegetarianskie;
    [ObservableProperty] private bool czyOrzech;
    [ObservableProperty] private bool czyNabial;

    // WŁAŚCIWOŚCI DLA SORTOWANIA
    [ObservableProperty] private bool areSortOptionsVisible;
    [ObservableProperty] private string sortArrow = "▼";
    [ObservableProperty] private bool sortByTimeAsc;
    [ObservableProperty] private bool sortByTimeDesc;
    [ObservableProperty] private bool sortByDifficultyAsc;
    [ObservableProperty] private bool sortByDifficultyDesc;

    public SearchResultsViewModel(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    // AUTOMATYCZNE ODŚWIEŻANIE PO ZMIANIE FILTRÓW/SORTOWANIA
    partial void OnCzyWeganskieChanged(bool value) => ApplyFiltersAndSort();
    partial void OnCzyWegetarianskieChanged(bool value) => ApplyFiltersAndSort();
    partial void OnCzyOrzechChanged(bool value) => ApplyFiltersAndSort();
    partial void OnCzyNabialChanged(bool value) => ApplyFiltersAndSort();

    partial void OnSortByTimeAscChanged(bool value) => ApplyFiltersAndSort();
    partial void OnSortByTimeDescChanged(bool value) => ApplyFiltersAndSort();
    partial void OnSortByDifficultyAscChanged(bool value) => ApplyFiltersAndSort();
    partial void OnSortByDifficultyDescChanged(bool value) => ApplyFiltersAndSort();

    [RelayCommand]
    private void ToggleFilters()
    {
        AreFiltersVisible = !AreFiltersVisible;
        FiltersArrow = AreFiltersVisible ? "▲" : "▼";
    }

    [RelayCommand]
    private void ToggleSort()
    {
        AreSortOptionsVisible = !AreSortOptionsVisible;
        SortArrow = AreSortOptionsVisible ? "▲" : "▼";
    }

    partial void OnIngredientsTextChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
            _ = LoadInitialRecipesAsync(value);
    }

    private async Task LoadInitialRecipesAsync(string ingredients)
    {
        var ingredientList = ingredients.Split(',').Select(i => i.Trim()).ToList();
        var results = await _recipeService.SearchRecipesAsync(ingredientList);

        _allFoundRecipes = results.ToList();
        ApplyFiltersAndSort();
    }

    private int GetDifficultyWeight(string difficulty)
    {
        return difficulty?.ToLower() switch
        {
            "łatwy" => 1,
            "średni" => 2,
            "trudny" => 3,
            _ => 4
        };
    }

    private void ApplyFiltersAndSort()
    {
        var filtered = _allFoundRecipes.AsEnumerable();

        if (CzyWeganskie) filtered = filtered.Where(r => r.CzyWeganskie);
        if (CzyWegetarianskie) filtered = filtered.Where(r => r.CzyWegetarianskie);
        if (CzyOrzech) filtered = filtered.Where(r => r.CzyOrzech);
        if (CzyNabial) filtered = filtered.Where(r => r.CzyNabial);

        if (SortByTimeAsc)
            filtered = filtered.OrderBy(r => r.CzasPrzygotowaniaMin);
        else if (SortByTimeDesc)
            filtered = filtered.OrderByDescending(r => r.CzasPrzygotowaniaMin);
        else if (SortByDifficultyAsc)
            filtered = filtered.OrderBy(r => GetDifficultyWeight(r.PoziomTrudnosci));
        else if (SortByDifficultyDesc)
            filtered = filtered.OrderByDescending(r => GetDifficultyWeight(r.PoziomTrudnosci));

        Recipes.Clear();
        foreach (var recipe in filtered)
            Recipes.Add(recipe);
    }

    // Używane przez TapGestureRecognizer w XAML (OpenRecipeCommand)
    [RelayCommand]
    private async Task OpenRecipe(Recipe recipe)
    {
        if (recipe is null) return;

        await Shell.Current.GoToAsync("RecipeDetailsPage", new Dictionary<string, object>
        {
            { "Recipe", recipe }
        });
    }

    // Zostawiam dla kompatybilności wstecznej
    [RelayCommand]
    private async Task GoToDetails(Recipe recipe)
    {
        await OpenRecipe(recipe);
    }
}