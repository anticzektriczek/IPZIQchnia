using iquchnia.Models;
using iquchnia.ViewModels;
using Microsoft.Maui.Controls;

namespace iquchnia.Views;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        // Obsługa kliknięcia przepisu
        RecipesCollection.SelectionChanged += RecipesCollection_SelectionChanged;
    }

    private async void RecipesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Recipe selectedRecipe)
        {
            await Shell.Current.GoToAsync(nameof(RecipeDetailsPage), true,
                new Dictionary<string, object>
                {
                    ["Recipe"] = selectedRecipe
                });

            // Reset zaznaczenia, żeby można było kliknąć ponownie
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
