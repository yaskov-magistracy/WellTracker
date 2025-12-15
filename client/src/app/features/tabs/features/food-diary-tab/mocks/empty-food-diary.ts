import {FoodDiary} from "../types/FoodDiary";
import {emptyFoodMeal} from "./empty-food-meal";
import {emptyFoodNutriments} from "./empty-food-nutriments";
import {emptyFoodEnergy} from "./empty-food-energy";

export const emptyFoodDiary: FoodDiary = {
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "date": "2025-10-27",
  "breakfast": emptyFoodMeal,
  lunch: emptyFoodMeal,
  snack: emptyFoodMeal,
  dinner: emptyFoodMeal,
  totalNutriments: emptyFoodNutriments,
  totalEnergy: emptyFoodEnergy,
  targetEnergy: emptyFoodEnergy,
  targetNutriments: emptyFoodNutriments
}
