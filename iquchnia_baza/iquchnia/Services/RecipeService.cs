using SQLite;
using iquchnia.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace iquchnia.Services;

public class RecipeService : IRecipeService
{
    private SQLiteAsyncConnection _database;
    private readonly string _dbPath;

    public RecipeService(string dbPath)
    {
        _dbPath = dbPath;
    }

    private async Task Init()
    {
        if (_database is not null)
            return;

        // LINIA DO DODANIA: Usuwa starą bazę przy każdym uruchomieniu (tylko do testów!)
        //if (File.Exists(_dbPath)) File.Delete(_dbPath); 

        if (!File.Exists(_dbPath))
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("recipes.db");
            using var newStream = File.Create(_dbPath);
            await stream.CopyToAsync(newStream);
        }

        _database = new SQLiteAsyncConnection(_dbPath);
        await _database.CreateTableAsync<Recipe>();
    }

    public async Task<List<Recipe>> GetRecipesAsync()
    {
        await Init();
        return await _database.Table<Recipe>().ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> SearchRecipesAsync(List<string> ingredients)
    {
        await Init();
        var allRecipes = await _database.Table<Recipe>().ToListAsync();

        return allRecipes.Where(r => ingredients.All(i =>
            r.IngredientsString.Contains(i, StringComparison.OrdinalIgnoreCase)));
    }
}