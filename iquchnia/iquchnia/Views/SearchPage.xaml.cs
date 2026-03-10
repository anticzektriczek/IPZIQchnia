using iquchnia.ViewModels;
using Microsoft.Maui.Controls;

namespace iquchnia.Views;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        // Nic więcej nie trzeba – żadnego RecipesCollection
    }
}