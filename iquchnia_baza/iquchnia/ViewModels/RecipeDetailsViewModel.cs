using CommunityToolkit.Mvvm.ComponentModel;
using iquchnia.Models;

namespace iquchnia.ViewModels;

[QueryProperty(nameof(Recipe), "Recipe")]
public partial class RecipeDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private Recipe recipe;
       

    public string IngredientsString => Recipe == null ? string.Empty : string.Join(", ", Recipe.Ingredients);

}
