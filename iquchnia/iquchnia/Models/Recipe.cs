using System.Collections.Generic;

namespace iquchnia.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Ingredients { get; set; } = new();
    public string Description { get; set; } = string.Empty;

    public bool CzyWeganskie { get; set; }
    public bool CzyWegetarianskie { get; set; }
    public bool CzyOrzech { get; set; }
    public bool CzyNabial { get; set; }
    public int CzasPrzygotowaniaMin { get; set; }   
    public int PoziomTrudnosci { get; set; }
}
