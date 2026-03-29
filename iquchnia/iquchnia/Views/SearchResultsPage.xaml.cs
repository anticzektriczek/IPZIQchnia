using iquchnia.ViewModels;
using iquchnia.Models;   // WAŻNE
using System.Collections.Generic;
using System.Linq;

namespace iquchnia.Views;

public partial class SearchResultsPage : ContentPage
{
    public SearchResultsPage(SearchResultsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedRecipe = e.CurrentSelection.FirstOrDefault() as Recipe;

        if (selectedRecipe == null)
            return;

        await Shell.Current.GoToAsync(nameof(RecipeDetailsPage), true,
            new Dictionary<string, object>
            {
                ["Recipe"] = selectedRecipe
            });

        ((CollectionView)sender).SelectedItem = null;
    }
}