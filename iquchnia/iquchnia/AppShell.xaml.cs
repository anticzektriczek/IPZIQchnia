using Microsoft.Maui.Controls;
using iquchnia.Views;

namespace iquchnia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(SearchResultsPage), typeof(SearchResultsPage));
            Routing.RegisterRoute(nameof(RecipeDetailsPage), typeof(RecipeDetailsPage));
        }
    }
}