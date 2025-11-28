import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FoodDiary} from "../types/food/FoodDiary";
import {Food} from "../types/food/Food";
import {FoodMeal} from "../types/food/FoodMeal";
import {FoodMealExtended} from "../components/day-summary/types/FoodMealExtended";
import {UpdateFoodDiaryRequestDTO} from "./DTO/requests/UpdateFoodDiaryRequestDTO";

@Injectable({
  providedIn: 'root'
})
export class FoodDiaryApiService {

  private _httpClient = inject(HttpClient);

  private _apiPath = 'FoodsDiary';

  getFoodDiaryByDate$(date: string) {
    return this._httpClient.get<FoodDiary>(`/api/${this._apiPath}/${date}`)
  }

  updateFoodDiary$(updateFoodDiaryRequestDTO: UpdateFoodDiaryRequestDTO, date: string) {
    return this._httpClient.post<FoodDiary>(`/api/${this._apiPath}/${date}`, updateFoodDiaryRequestDTO);
  }
}
