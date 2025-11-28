import {Injectable} from "@angular/core";
import {EatenFood} from "../../features/tabs/features/food-diary-tab/types/food/EatenFood";

@Injectable({
  providedIn: 'root'
})
export class FoodMapper {

  eatenFoodToUpdateFoodDTO(food: EatenFood) {
    return { foodId: food.food.id, grams: food.grams };
  }
}
