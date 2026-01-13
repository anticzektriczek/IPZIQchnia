using iquchnia.Models;
using System.Collections.Generic;
using System.Linq;

namespace iquchnia.Services;

public class RecipeService : IRecipeService
{
    private readonly List<Recipe> _recipes = new()
    {
        new Recipe
        {
            Id = 1,
            Name = "Omlet",
            Ingredients = new() { "jajko", "mleko", "sól" },
            Description = "Rozbij jajka, dodaj mleko i sól, usmaż na patelni."
        },
        new Recipe
        {
            Id = 2,
            Name = "Makaron z serem",
            Ingredients = new() { "makaron", "ser", "masło" },
            Description = "Ugotuj makaron, dodaj masło i starty ser."
        }
    };

    public IEnumerable<Recipe> SearchRecipes(List<string> ingredients)
    {
        return _recipes.Where(r =>
            ingredients.All(i =>
                r.Ingredients.Any(ri =>
                    ri.Contains(i, System.StringComparison.OrdinalIgnoreCase))));
    }
}
