import {inject, Injectable} from '@angular/core';
import {catchError, forkJoin, map, of} from "rxjs";
import {ExerciseDiaryApiService} from "../../exercise-diary/dal/exercise-diary.api.service";
import {FoodDiaryApiService} from "../../../dal/food-diary.api.service";
import {DaySummary} from "../types/DaySummary";
import { pick } from 'lodash-es'
import {emptyFoodEnergy} from "../../../mocks/empty-food-energy";
import {emptyFoodNutriments} from "../../../mocks/empty-food-nutriments";
import {emptyFoodMeal} from "../../../mocks/empty-food-meal";
import {formatDate} from "../../../../../../../core/utils/dates/format-date";

@Injectable({
  providedIn: 'root'
})
export class DaySummaryApiService {

  private foodDiaryApiS = inject(FoodDiaryApiService);
  private exerciseDiaryApiS = inject(ExerciseDiaryApiService);

  getDaySummary$() {
    return forkJoin([
      this.foodDiaryApiS.getFoodDiaryByDate$(formatDate(new Date()))
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
      this.exerciseDiaryApiS.getExerciseDiaryByDate$(formatDate(new Date()))
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
            breakfast: { name: 'Завтрак', iconName: 'breakfast', ...todayFoodDiary.breakfast },
            lunch: { name: 'Обед', iconName: 'lunch', ...todayFoodDiary.lunch },
            snack: { name: 'Перекус', iconName: 'snack', ...todayFoodDiary.snack },
            dinner: { name: 'Ужин', iconName: 'dinner', ...todayFoodDiary.dinner }
          }
        }) as DaySummary)
      );
  }
}
