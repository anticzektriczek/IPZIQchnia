using CommunityToolkit.Mvvm.ComponentModel;
using iquchnia.Models;
using iquchnia.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iquchnia.ViewModels
{
    public class RecipesViewModel : ObservableObject
    {
        private readonly RecipeService _recipeService;

        public RecipesViewModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
            Recipes = new ObservableCollection<Recipe>();
        }

        public ObservableCollection<Recipe> Recipes { get; }

        public void SetIngredients(List<string> ingredients)
        {
            Recipes.Clear();
            var results = _recipeService.SearchRecipes(ingredients);
            foreach (var r in results)
                Recipes.Add(r);
        }
    }
}
