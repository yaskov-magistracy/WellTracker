using DAL.Exercises.Models;
using Domain.Exercises;
using Domain.Exercises.DTO;

namespace DAL.Exercises;

internal static class ExercisesMapper
{
    public static ExerciseEntity ToEntity(ExerciseCreateEntity createEntity)
        => new()
        {
            Name = createEntity.Name,
            Description = createEntity.Description,
            Type = ToEntity(createEntity.Type),
            Measurement = ToEntity(createEntity.Measurement),
            Muscles = createEntity.Muscles.Select(ToEntity).ToArray(),
            Equipments = createEntity.Equipments.Select(ToEntity).ToArray(),
        };
    
    public static Exercise ToDomain(ExerciseEntity entity)
        => new(entity.Id,
            entity.Name,
            entity.Description,
            ToDomain(entity.Type),
            ToDomain(entity.Measurement),
            entity.Muscles.Select(ToDomain).ToArray(),
            entity.Equipments.Select(ToDomain).ToArray());

    public static ExerciseTypeEntity ToEntity(ExerciseType domain)
        => domain switch
        {
            ExerciseType.Strength => ExerciseTypeEntity.Strength,
            ExerciseType.Cardio => ExerciseTypeEntity.Cardio,
            _ => throw new ArgumentException($"Unknown ExerciseType: {domain}"),
        };
    
    public static ExerciseType ToDomain(ExerciseTypeEntity entity)
        => entity switch
        {
            ExerciseTypeEntity.Strength => ExerciseType.Strength,
            ExerciseTypeEntity.Cardio => ExerciseType.Cardio,
            _ => throw new ArgumentException($"Unknown ExerciseTypeEntity: {entity}"),
        };
    
    public static ExerciseMeasurementEntity ToEntity(ExerciseMeasurement domain)
        => domain switch
        {
            ExerciseMeasurement.Time => ExerciseMeasurementEntity.Time,
            ExerciseMeasurement.Repeats => ExerciseMeasurementEntity.Repeats,
            _ => throw new ArgumentException($"Unknown ExerciseMeasurement: {domain}"),
        };
    
    public static ExerciseMeasurement ToDomain(ExerciseMeasurementEntity entity)
        => entity switch
        {
            ExerciseMeasurementEntity.Time => ExerciseMeasurement.Time,
            ExerciseMeasurementEntity.Repeats => ExerciseMeasurement.Repeats,
            _ => throw new ArgumentException($"Unknown ExerciseMeasurementEntity: {entity}"),
        };
    
    public static MuscleTypeEntity ToEntity(MuscleType domain)
        => domain switch
        {
            MuscleType.Biceps => MuscleTypeEntity.Biceps,
            MuscleType.Triceps => MuscleTypeEntity.Triceps,
            MuscleType.Back => MuscleTypeEntity.Back,
            MuscleType.Chest => MuscleTypeEntity.Chest,
            MuscleType.Shoulders => MuscleTypeEntity.Shoulders,
            MuscleType.Forearms => MuscleTypeEntity.Forearms,
            MuscleType.Glutes => MuscleTypeEntity.Glutes,
            MuscleType.Quadriceps => MuscleTypeEntity.Quadriceps,
            MuscleType.LegsBiceps => MuscleTypeEntity.LegsBiceps,
            MuscleType.Calves => MuscleTypeEntity.Calves,
            MuscleType.Abs => MuscleTypeEntity.Abs,
            _ => throw new ArgumentException($"Unknown MuscleType: {domain}"),
        };
    
    public static MuscleType ToDomain(MuscleTypeEntity entity)
        => entity switch
        {
            MuscleTypeEntity.Biceps => MuscleType.Biceps,
            MuscleTypeEntity.Triceps => MuscleType.Triceps,
            MuscleTypeEntity.Back => MuscleType.Back,
            MuscleTypeEntity.Chest => MuscleType.Chest,
            MuscleTypeEntity.Shoulders => MuscleType.Shoulders,
            MuscleTypeEntity.Forearms => MuscleType.Forearms,
            MuscleTypeEntity.Glutes => MuscleType.Glutes,
            MuscleTypeEntity.Quadriceps => MuscleType.Quadriceps,
            MuscleTypeEntity.LegsBiceps => MuscleType.LegsBiceps,
            MuscleTypeEntity.Calves => MuscleType.Calves,
            MuscleTypeEntity.Abs => MuscleType.Abs,
            _ => throw new ArgumentException($"Unknown MuscleTypeEntity: {entity}"),
        };
    
    public static EquipmentTypeEntity ToEntity(EquipmentType domain)
        => domain switch
        {
            EquipmentType.BodyWeight => EquipmentTypeEntity.BodyWeight,
            EquipmentType.Bands => EquipmentTypeEntity.Bands,
            EquipmentType.Bench => EquipmentTypeEntity.Bench,
            EquipmentType.Dumbbell => EquipmentTypeEntity.Dumbbell,
            EquipmentType.Barbell => EquipmentTypeEntity.Barbell,
            EquipmentType.EzBarbell => EquipmentTypeEntity.EzBarbell,
            EquipmentType.Kettlebell => EquipmentTypeEntity.Kettlebell,
            EquipmentType.ExerciseBall => EquipmentTypeEntity.ExerciseBall,
            EquipmentType.CardioMachine => EquipmentTypeEntity.CardioMachine,
            EquipmentType.StrengthMachine => EquipmentTypeEntity.StrengthMachine,
            EquipmentType.PullupBar => EquipmentTypeEntity.PullupBar,
            EquipmentType.WeightPlate => EquipmentTypeEntity.WeightPlate,
            _ => throw new ArgumentException($"Unknown EquipmentType: {domain}"),
        };
    
    public static EquipmentType ToDomain(EquipmentTypeEntity entity)
        => entity switch
        {
            EquipmentTypeEntity.BodyWeight => EquipmentType.BodyWeight,
            EquipmentTypeEntity.Bands => EquipmentType.Bands,
            EquipmentTypeEntity.Bench => EquipmentType.Bench,
            EquipmentTypeEntity.Dumbbell => EquipmentType.Dumbbell,
            EquipmentTypeEntity.Barbell => EquipmentType.Barbell,
            EquipmentTypeEntity.EzBarbell => EquipmentType.EzBarbell,
            EquipmentTypeEntity.Kettlebell => EquipmentType.Kettlebell,
            EquipmentTypeEntity.ExerciseBall => EquipmentType.ExerciseBall,
            EquipmentTypeEntity.CardioMachine => EquipmentType.CardioMachine,
            EquipmentTypeEntity.StrengthMachine => EquipmentType.StrengthMachine,
            EquipmentTypeEntity.PullupBar => EquipmentType.PullupBar,
            EquipmentTypeEntity.WeightPlate => EquipmentType.WeightPlate,
            _ => throw new ArgumentException($"Unknown EquipmentTypeEntity: {entity}"),
        };
}