using iquchnia.Views;
using Microsoft.Maui.Controls;

namespace iquchnia;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        Routing.RegisterRoute(nameof(RecipeDetailsPage), typeof(RecipeDetailsPage));
    }
}
