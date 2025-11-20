namespace ExercisesParser.Models;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Category { get; set; }
    public string CategoryName { get; set; }
    public List<int> Muscles { get; set; } = new List<int>();
    public List<int> MusclesSecondary { get; set; } = new List<int>();
    public List<int> Equipment { get; set; } = new List<int>();
    
    // Дополнительные поля которые могут быть полезны
    public string LicenseAuthor { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public List<string> Videos { get; set; } = new List<string>();
}