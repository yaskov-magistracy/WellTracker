import {inject, Injectable} from '@angular/core';
import {ExerciseDiaryApiService} from "../dal/exercise-diary.api.service";

@Injectable({
  providedIn: 'root'
})
export class ExerciseDiaryService {

  private exerciseDiaryApiS = inject(ExerciseDiaryApiService);

  getExerciseDiaryByDate$(date: string) {
    return this.exerciseDiaryApiS.getExerciseDiaryByDate$(date);
  }
}
