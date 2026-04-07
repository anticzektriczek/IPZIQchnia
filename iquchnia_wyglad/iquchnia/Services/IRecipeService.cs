using iquchnia.Models;
using System.Collections.Generic;

namespace iquchnia.Services;

public interface IRecipeService
{
    IEnumerable<Recipe> SearchRecipes(List<string> ingredients);
}
