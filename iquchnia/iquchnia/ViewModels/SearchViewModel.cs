using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using iquchnia.Views;

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

        await Shell.Current.GoToAsync(nameof(SearchResultsPage), true,
            new Dictionary<string, object>
            {
                ["Ingredients"] = IngredientsText
            });
    }
}