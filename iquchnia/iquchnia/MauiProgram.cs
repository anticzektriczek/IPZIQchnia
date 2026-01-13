using iquchnia.Services;
using iquchnia.ViewModels;
using iquchnia.Views;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace iquchnia;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Rejestracja usług
        builder.Services.AddSingleton<IRecipeService, RecipeService>();

        // Rejestracja ViewModeli
        builder.Services.AddTransient<SearchViewModel>();
        builder.Services.AddTransient<RecipeDetailsViewModel>();

        // Rejestracja stron
        builder.Services.AddTransient<SearchPage>();
        builder.Services.AddTransient<RecipeDetailsPage>();

        return builder.Build();
    }
}
