using iquchnia.ViewModels;
using Microsoft.Maui.Controls;

namespace iquchnia.Views;

public partial class RecipeDetailsPage : ContentPage
{
    public RecipeDetailsPage(RecipeDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
