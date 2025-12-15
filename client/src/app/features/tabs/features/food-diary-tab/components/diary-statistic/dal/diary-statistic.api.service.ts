import {inject, Injectable} from '@angular/core';
import {catchError, forkJoin, map, of} from "rxjs";
import {ExerciseDiaryApiService} from "../../exercise-diary/dal/exercise-diary.api.service";
import {FoodDiaryApiService} from "../../../dal/food-diary.api.service";
import { pick } from 'lodash-es'
import {emptyFoodEnergy} from "../../../mocks/empty-food-energy";
import {emptyFoodNutriments} from "../../../mocks/empty-food-nutriments";
import {emptyFoodMeal} from "../../../mocks/empty-food-meal";
import {HttpClient} from "@angular/common/http";
import {ExerciseDiary} from "../../exercise-diary/types/ExerciseDiary";
import {FoodStatistic} from "../types/food/FoodStatistic";
import {WeightStatistic} from "../types/weight/WeightStatistic";

@Injectable({
  providedIn: 'root'
})
export class DiaryStatisticApiService {

  #httpClient = inject(HttpClient);

  #apiPath = 'Statistics';

  getFoodStatistic$(dateRange: [string, string]) {
    return this.#httpClient.get<FoodStatistic>(`/api/${this.#apiPath}/Food`,
      { params: { from: dateRange[0], to: dateRange[1] } }
    );
  }

  getWeightStatistic$(dateRange: [string, string]) {
    return this.#httpClient.get<WeightStatistic>(`/api/${this.#apiPath}/Weight`,
      { params: { from: dateRange[0], to: dateRange[1] } }
    );
  }
}
