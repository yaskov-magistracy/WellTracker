import {inject, Injectable} from "@angular/core";
import {FoodMapper} from "../../../../../core/food/food.mapper";
import {FoodDiary} from "../types/food/FoodDiary";
import {UpdateFoodDiaryRequestDTO} from "../dal/DTO/requests/UpdateFoodDiaryRequestDTO";
import {pick} from "lodash-es";
import {FoodMealType} from "../types/food/FoodMealType";

@Injectable({
  providedIn: 'root'
})
export class FoodDiaryMapper {

  #foodMapper = inject(FoodMapper);

  foodDiaryToUpdateFoodDiaryDTO(foodDiary: FoodDiary): UpdateFoodDiaryRequestDTO {
    const meals = pick<FoodDiary, FoodMealType>(foodDiary, ['breakfast', 'lunch', 'snack', 'dinner']);
    return Object.keys(meals)
      .reduce<UpdateFoodDiaryRequestDTO>((acc, meal) => ({
        ...acc,
        [meal]: meals[meal as unknown as FoodMealType]!.eatenFoods.map(f => this.#foodMapper.eatenFoodToUpdateFoodDTO(f))
      }), {} as UpdateFoodDiaryRequestDTO);
  }
}
