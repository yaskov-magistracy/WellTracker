using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DAL.Exercises.Models;
using NpgsqlTypes;

namespace DAL.Exercises;

internal class ExerciseEntity
{
    [Key] public Guid Id { get; set; }
    [JsonIgnore] public NpgsqlTsVector SearchVector { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ExerciseTypeEntity Type { get; set; }
    public ExerciseMeasurementEntity Measurement { get; set; }
    public MuscleTypeEntity[] Muscles { get; set; }
    public EquipmentTypeEntity[] Equipments { get; set; }
}