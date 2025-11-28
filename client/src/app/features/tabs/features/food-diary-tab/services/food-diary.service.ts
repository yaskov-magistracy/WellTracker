import {inject, Injectable} from '@angular/core';
import {FoodDiaryApiService} from "../dal/food-diary.api.service";
import {Food} from "../types/food/Food";
import {FoodDiary} from "../types/food/FoodDiary";
import {pick, set} from "lodash-es";
import {FoodMealType} from "../types/food/FoodMealType";
import {UpdateFoodDTO} from "../dal/DTO/requests/UpdateFoodDiaryRequestDTO";
import {FoodMapper} from "../../../../../core/food/food.mapper";
import {FoodDiaryMapper} from "./food-diary.mapper";
import {formatDate} from "../../../../../core/utils/dates/format-date";

@Injectable({
  providedIn: 'root'
})
export class FoodDiaryService {

  #foodDiaryApiS = inject(FoodDiaryApiService);
  #foodDiaryMapper = inject(FoodDiaryMapper);

  getFoodDiaryByDate$(date: string) {
    return this.#foodDiaryApiS.getFoodDiaryByDate$(date);
  }

  addFoodToDiaryMeal$(food: UpdateFoodDTO, mealType: FoodMealType, diaryToAddFoodTo: FoodDiary) {
    const meals = this.#foodDiaryMapper.foodDiaryToUpdateFoodDiaryDTO(diaryToAddFoodTo);
    const updatedMeals = set(
      meals,
      mealType,
      [...meals[mealType], food]
    );
    return this.#foodDiaryApiS.updateFoodDiary$(updatedMeals, formatDate(diaryToAddFoodTo.date));
  }

  changeFoodInDiaryMeal$(food: UpdateFoodDTO, mealType: FoodMealType, diaryToAddFoodTo: FoodDiary) {
    const meals = this.#foodDiaryMapper.foodDiaryToUpdateFoodDiaryDTO(diaryToAddFoodTo);
    const updatedMeals = set(
      meals,
      mealType,
      meals[mealType].map(f => {
        if (f.foodId !== food.foodId) { return f; }
        return food;
      })
    );
    return this.#foodDiaryApiS.updateFoodDiary$(updatedMeals, formatDate(diaryToAddFoodTo.date));
  }

  deleteFoodFromDiaryMeal$(foodId: string, mealType: FoodMealType, diaryToAddFoodTo: FoodDiary) {
    const meals = this.#foodDiaryMapper.foodDiaryToUpdateFoodDiaryDTO(diaryToAddFoodTo);
    const updatedMeals = set(
      meals,
      mealType,
      meals[mealType].filter(f => f.foodId !== foodId)
    );
    return this.#foodDiaryApiS.updateFoodDiary$(updatedMeals, formatDate(diaryToAddFoodTo.date));
  }
}
