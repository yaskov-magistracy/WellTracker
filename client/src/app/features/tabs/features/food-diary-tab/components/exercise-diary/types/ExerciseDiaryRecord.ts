import {CompletedExercise} from "./CompletedExercise";
import {ExerciseRecordInfo} from "./ExerciseRecordInfo";

export type ExerciseDiaryRecord = {
  steps: number;
  completedExercises: CompletedExercise[];
  info: ExerciseRecordInfo;
}
