import {inject, Injectable} from '@angular/core';
import {catchError, forkJoin, map, of} from "rxjs";
import {ExerciseDiaryApiService} from "../../exercise-diary/dal/exercise-diary.api.service";
import {FoodDiaryApiService} from "../../../dal/food-diary.api.service";
import {DaySummary} from "../types/DaySummary";
import { pick } from 'lodash-es'
import {emptyFoodEnergy} from "../../../mocks/empty-food-energy";
import {emptyFoodNutriments} from "../../../mocks/empty-food-nutriments";
import {emptyFoodMeal} from "../../../mocks/empty-food-meal";

@Injectable({
  providedIn: 'root'
})
export class DaySummaryApiService {

  private foodDiaryApiS = inject(FoodDiaryApiService);
  private exerciseDiaryApiS = inject(ExerciseDiaryApiService);

  getDaySummary$() {
    return forkJoin([
      this.foodDiaryApiS.getFoodDiaryByDate$(new Date().toLocaleDateString())
        .pipe(
          map(foodDiary => ({
            ...foodDiary,
            totalEnergy: foodDiary.totalEnergy ?? emptyFoodEnergy,
            totalNutriments: foodDiary.totalNutriments ?? emptyFoodNutriments,
            breakfast: foodDiary.lunch ?? emptyFoodMeal,
            lunch: foodDiary.lunch ?? emptyFoodMeal,
            snack: foodDiary.lunch ?? emptyFoodMeal,
            dinner: foodDiary.lunch ?? emptyFoodMeal
          }))
        )
      ,
      this.exerciseDiaryApiS.getExerciseDiaryByDate$(new Date().toLocaleDateString())
        .pipe(
          catchError(() => of({ target: { info: { totalKcalBurnt: 100 } } }))
        ),
    ])
      .pipe(
        map(([todayFoodDiary, todayExerciseDiary]) => ({
          kcalNutrimentsSummary: {
            ...pick(todayFoodDiary, ['totalNutriments', 'targetNutriments', 'targetEnergy']),
            totalEnergy: { ...todayFoodDiary.totalEnergy , kcalBurnt: todayExerciseDiary.target.info.totalKcalBurnt }
          },
          mealsSummary: {
            breakfast: { name: 'Завтрак', ...todayFoodDiary.breakfast },
            lunch: { name: 'Обед', ...todayFoodDiary.lunch },
            snack: { name: 'Перекус', ...todayFoodDiary.snack },
            dinner: { name: 'Ужин', ...todayFoodDiary.dinner }
          }
        }) as DaySummary)
      );
  }
}
