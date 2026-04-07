using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace iquchnia.Models;

[Table("Recipe")]
public class Recipe
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Column("Ingredients")]
    public string IngredientsString { get; set; } = string.Empty;

    [Ignore]
    public List<string> Ingredients
    {
        get => IngredientsString?.Split(';').Select(s => s.Trim()).ToList() ?? new List<string>();
        set => IngredientsString = string.Join(";", value);
    }

    public bool CzyWeganskie { get; set; }
    public bool CzyWegetarianskie { get; set; }
    public bool CzyOrzech { get; set; }
    public bool CzyNabial { get; set; }

    public int CzasPrzygotowaniaMin { get; set; }

    public string PoziomTrudnosci { get; set; } = string.Empty;
}