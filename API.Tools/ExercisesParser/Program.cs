using ExercisesParser;

var exerciseService = new WgerParser();
await exerciseService.DisplayExercisesWithDetails(10);