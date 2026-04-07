using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iquchnia.Models;
using iquchnia.Services;
using System.Collections.ObjectModel;

namespace iquchnia.ViewModels;

[QueryProperty(nameof(IngredientsText), "Ingredients")]
public partial class SearchResultsViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    private List<Recipe> _allRecipes = new();

    public ObservableCollection<Recipe> Recipes { get; } = new();

    [ObservableProperty]
    private string ingredientsText;

    [ObservableProperty] private bool czyWeganskie;
    [ObservableProperty] private bool czyWegetarianskie;
    [ObservableProperty] private bool czyOrzech;
    [ObservableProperty] private bool czyNabial;

    [ObservableProperty] private bool areFiltersVisible;
    public string FiltersArrow => AreFiltersVisible ? "▲" : "▼";

    [ObservableProperty] private bool areSortOptionsVisible;
    public string SortArrow => AreSortOptionsVisible ? "▲" : "▼";

    [ObservableProperty] private bool sortByTimeAsc;
    [ObservableProperty] private bool sortByTimeDesc;
    [ObservableProperty] private bool sortByDifficultyAsc;
    [ObservableProperty] private bool sortByDifficultyDesc;

    public SearchResultsViewModel(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    partial void OnIngredientsTextChanged(string value)
    {
        LoadRecipes(value);
    }

    partial void OnCzyWeganskieChanged(bool value) => ApplyFilters();
    partial void OnCzyWegetarianskieChanged(bool value) => ApplyFilters();
    partial void OnCzyOrzechChanged(bool value) => ApplyFilters();
    partial void OnCzyNabialChanged(bool value) => ApplyFilters();

    partial void OnSortByTimeAscChanged(bool value) { if (value) ResetSortExcept(nameof(SortByTimeAsc)); ApplyFilters(); }
    partial void OnSortByTimeDescChanged(bool value) { if (value) ResetSortExcept(nameof(SortByTimeDesc)); ApplyFilters(); }
    partial void OnSortByDifficultyAscChanged(bool value) { if (value) ResetSortExcept(nameof(SortByDifficultyAsc)); ApplyFilters(); }
    partial void OnSortByDifficultyDescChanged(bool value) { if (value) ResetSortExcept(nameof(SortByDifficultyDesc)); ApplyFilters(); }

    private void ResetSortExcept(string propertyName)
    {
        if (propertyName != nameof(SortByTimeAsc)) sortByTimeAsc = false;
        if (propertyName != nameof(SortByTimeDesc)) sortByTimeDesc = false;
        if (propertyName != nameof(SortByDifficultyAsc)) sortByDifficultyAsc = false;
        if (propertyName != nameof(SortByDifficultyDesc)) sortByDifficultyDesc = false;
    }

    [RelayCommand]
    private void ToggleFilters()
    {
        if (AreSortOptionsVisible) AreSortOptionsVisible = false;
        AreFiltersVisible = !AreFiltersVisible;
    }

    [RelayCommand]
    private void ToggleSort()
    {
        if (AreFiltersVisible) AreFiltersVisible = false;
        AreSortOptionsVisible = !AreSortOptionsVisible;
    }

    private void LoadRecipes(string ingredientsText)
    {
        _allRecipes.Clear();
        Recipes.Clear();
        if (string.IsNullOrWhiteSpace(ingredientsText)) return;

        var ingredients = ingredientsText
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(i => i.Trim())
            .ToList();

        _allRecipes = _recipeService.SearchRecipes(ingredients).ToList();
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        Recipes.Clear();
        IEnumerable<Recipe> filtered = _allRecipes;

        if (CzyWeganskie) filtered = filtered.Where(r => r.CzyWeganskie);
        if (CzyWegetarianskie) filtered = filtered.Where(r => r.CzyWegetarianskie);
        if (CzyOrzech) filtered = filtered.Where(r => r.CzyOrzech);
        if (CzyNabial) filtered = filtered.Where(r => r.CzyNabial);

        // sortowanie
        if (SortByTimeAsc) filtered = filtered.OrderBy(r => r.CzasPrzygotowaniaMin);
        else if (SortByTimeDesc) filtered = filtered.OrderByDescending(r => r.CzasPrzygotowaniaMin);
        else if (SortByDifficultyAsc) filtered = filtered.OrderBy(r => r.PoziomTrudnosci);
        else if (SortByDifficultyDesc) filtered = filtered.OrderByDescending(r => r.PoziomTrudnosci);

        foreach (var r in filtered) Recipes.Add(r);
    }
}