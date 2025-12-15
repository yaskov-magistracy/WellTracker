import {ExerciseDiaryRecord} from "./ExerciseDiaryRecord";

export type ExerciseDiary = {
  "id": string,
  "userId": string,
  "date": string,
  "current": ExerciseDiaryRecord,
  "target": ExerciseDiaryRecord
}
