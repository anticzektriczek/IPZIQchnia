using Microsoft.Extensions.Logging;
using iquchnia.Services;
using iquchnia.ViewModels;
using iquchnia.Views;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 1. USTALENIE ŚCIEŻKI DO BAZY DANYCH
        // Ścieżka wskazuje na bezpieczny folder lokalny aplikacji
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");

        // 2. REJESTRACJA SERWISU BAZY DANYCH
        // Rejestrujemy IRecipeService jako Singleton, przekazując mu ścieżkę do pliku
        builder.Services.AddSingleton<IRecipeService>(s =>
            ActivatorUtilities.CreateInstance<RecipeService>(s, dbPath));

        // 3. REJESTRACJA VIEWMODELI
        // Musisz zarejestrować wszystkie ViewModele ze źródeł, aby mogły przyjąć serwis w konstruktorze
        builder.Services.AddTransient<SearchViewModel>();           // [4]
        builder.Services.AddTransient<SearchResultsViewModel>();    // [3]
        builder.Services.AddTransient<RecipesViewModel>();          // [2]
        builder.Services.AddTransient<RecipeDetailsViewModel>();    // [5]

        // 4. REJESTRACJA STRON (OPCJONALNIE)
        // Dobre praktyki sugerują również rejestrację samych stron
        //builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}