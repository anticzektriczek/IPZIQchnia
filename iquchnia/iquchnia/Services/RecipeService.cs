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
            Ingredients = new() { "jajko", "mleko", "sól", "ser", "a" },    
            Description = "Rozbij jajka, dodaj mleko i sól, usmaż na patelni.",
            CzyWeganskie = true,
            CzyWegetarianskie = false,
            CzyOrzech = false,
            CzyNabial = true,
            CzasPrzygotowaniaMin = 20,
            PoziomTrudnosci = 2
        },
        new Recipe
        {
            Id = 2,
            Name = "Makaron z serem",
            Ingredients = new() { "makaron", "ser", "masło", "a" },
            Description = "Ugotuj makaron, dodaj masło i starty ser.",
            CzyWeganskie = false,
            CzyWegetarianskie = false,
            CzyOrzech = false,
            CzyNabial = false,
            CzasPrzygotowaniaMin = 30,
            PoziomTrudnosci = 1
        },
        new Recipe
        {
            Id = 3,
            Name = "Parówki",
            Ingredients = new() { "parówki", "ser", "masło", "a" },
            Description = "Ugotuj makaron, dodaj masło i starty ser.",
            CzyWeganskie = false,
            CzyWegetarianskie = false,
            CzyOrzech = false,
            CzyNabial = false,
            CzasPrzygotowaniaMin = 10,
            PoziomTrudnosci = 1
        },
        new Recipe
        {
            Name = "Jajecznica",
            Ingredients = new() { "jajko", "masło", "sól", "a" },
            Description = "Klasyczna jajecznica.",
            CzyWeganskie = false,
            CzyWegetarianskie = true,
            CzyOrzech = false,
            CzyNabial = true,
            CzasPrzygotowaniaMin = 10,
            PoziomTrudnosci = 1
        },
        new Recipe
        {
            Name = "Sałatka owocowa",
            Ingredients = new() { "jabłko", "banan", "pomarańcza", "a" },
            Description = "Lekka sałatka.",
            CzyWeganskie = true,
            CzyWegetarianskie = true,
            CzyOrzech = false,
            CzyNabial = false,
            CzasPrzygotowaniaMin = 40,
            PoziomTrudnosci = 3
        },
        new Recipe
        {
            Name = "Ciasto orzechowe",
            Ingredients = new() { "orzechy", "mąka", "cukier", "a" },
            Description = "Ciasto z orzechami.",
            CzyWeganskie = false,
            CzyWegetarianskie = true,
            CzyOrzech = true,
            CzyNabial = true,
            CzasPrzygotowaniaMin = 60,
            PoziomTrudnosci = 5
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
