import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FoodDiary} from "../types/FoodDiary";

@Injectable({
  providedIn: 'root'
})
export class FoodDiaryService {

  private _httpClient = inject(HttpClient);

  private _apiPath = 'FoodsDiary';

  getFoodDiaryByDate$(date: string) {
    return this._httpClient.get<FoodDiary>(`/api/${this._apiPath}/${date}`);
  }
}
