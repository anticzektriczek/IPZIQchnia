using System.Collections.Generic;

namespace iquchnia.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Ingredients { get; set; } = new();
    public string Description { get; set; } = string.Empty;
}
