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
import {Food} from "../../../types/food/Food";

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
            breakfast: foodDiary.breakfast ?? emptyFoodMeal,
            lunch: foodDiary.lunch ?? emptyFoodMeal,
            snack: foodDiary.snack ?? emptyFoodMeal,
            dinner: foodDiary.dinner ?? emptyFoodMeal
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
            breakfast: { fieldName: 'breakfast', name: 'Завтрак', iconName: 'breakfast', ...todayFoodDiary.breakfast },
            lunch: { fieldName: 'lunch', name: 'Обед', iconName: 'lunch', ...todayFoodDiary.lunch },
            snack: { fieldName: 'snack', name: 'Перекус', iconName: 'snack', ...todayFoodDiary.snack },
            dinner: { fieldName: 'dinner', name: 'Ужин', iconName: 'dinner', ...todayFoodDiary.dinner }
          },
          foodDiary: todayFoodDiary
        }) as DaySummary)
      );
  }

  addProductToMeal$(product: Food, mealType: 'breakfast' | 'lunch' | 'snack' | 'dinner') {
  }
}
