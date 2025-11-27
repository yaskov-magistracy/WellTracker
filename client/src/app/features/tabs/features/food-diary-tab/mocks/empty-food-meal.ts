import {emptyFoodNutriments} from "./empty-food-nutriments";
import {emptyFoodEnergy} from "./empty-food-energy";
import {FoodMeal} from "../types/food/FoodMeal";

export const emptyFoodMeal: FoodMeal = {
  eatenFoods: [],
  totalNutriments: emptyFoodNutriments,
  totalEnergy: emptyFoodEnergy
}
