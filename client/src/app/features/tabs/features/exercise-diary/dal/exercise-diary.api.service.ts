import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ExerciseDiary} from "../types/ExerciseDiary";

@Injectable({
  providedIn: 'root'
})
export class ExerciseDiaryApiService {

  private _httpClient = inject(HttpClient);

  private _apiPath = 'ExerciseDiary';

  getExerciseDiaryByDate$(date: string) {
    return this._httpClient.get<ExerciseDiary>(`/api/${this._apiPath}/${date}`)
  }
}
