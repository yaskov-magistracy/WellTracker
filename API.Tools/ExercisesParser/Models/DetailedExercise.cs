namespace ExercisesParser.Models;

public class DetailedExercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<string> MuscleNames { get; set; } = new List<string>();
    public List<string> SecondaryMuscleNames { get; set; } = new List<string>();
    public List<string> EquipmentNames { get; set; } = new List<string>();
}