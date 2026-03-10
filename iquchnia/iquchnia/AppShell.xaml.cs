using iquchnia.Views;
using Microsoft.Maui.Controls;

namespace iquchnia;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();


        Routing.RegisterRoute(nameof(SearchResultsPage), typeof(SearchResultsPage));
        Routing.RegisterRoute(nameof(RecipeDetailsPage), typeof(RecipeDetailsPage));
    }
}
