import {ExerciseDiaryRecord} from "../../food-diary-tab/components/exercise-diary/types/ExerciseDiaryRecord";

export type ExerciseDiary = {
  id: string;
  userId: string;
  date: string;
  current: ExerciseDiaryRecord;
  target: ExerciseDiaryRecord;
}
