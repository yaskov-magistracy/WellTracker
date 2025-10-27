import {FoodNutriments} from "./FoodNutriments";
import {FoodEnergy} from "./FoodEnergy";
import {FoodMeal} from "./FoodMeal";

export type FoodDiary = {
  id: string;
  userId: string;
  date: string;
  breakfast: FoodMeal[];
  lunch: FoodMeal[];
  snack: FoodMeal[];
  dinner: FoodMeal[];
  totalNutriments: FoodNutriments;
  totalEnergy: FoodEnergy;
}
