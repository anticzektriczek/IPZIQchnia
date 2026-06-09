using iquchnia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iquchnia.Services;

public interface IRecipeService
{
    Task<IEnumerable<Recipe>> SearchRecipesAsync(List<string> ingredients);
    Task<List<Recipe>> GetRecipesAsync();
    Task<List<string>> GetAllIngredientsAsync();
}