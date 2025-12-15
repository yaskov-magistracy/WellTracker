import {FoodEnergy} from "../../../../types/food/FoodEnergy";
import {FoodNutriments} from "../../../../types/food/FoodNutriments";

export type FoodStatisticRecord = {
  date: string;
  energy: FoodEnergy;
  nutriments: FoodNutriments;
}
