import {EatenFood} from "./EatenFood";
import {FoodNutriments} from "./FoodNutriments";
import {FoodEnergy} from "./FoodEnergy";

export type FoodMeal = {
  eatenFoods: EatenFood[];
  totalNutriments: FoodNutriments;
  totalEnergy: FoodEnergy;
}
