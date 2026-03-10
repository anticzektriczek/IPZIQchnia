using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iquchnia.ViewModels;

public partial class SearchViewModel : ObservableObject
{
    [ObservableProperty]
    private string ingredientsText;

    [RelayCommand]
    private async Task Search()
    {
        if (string.IsNullOrWhiteSpace(IngredientsText))
            return;

        // Przekazujemy składniki do strony z wynikami
        await Shell.Current.GoToAsync(nameof(Views.SearchResultsPage), true,
            new Dictionary<string, object>
            {
                ["Ingredients"] = IngredientsText
            });
    }
}