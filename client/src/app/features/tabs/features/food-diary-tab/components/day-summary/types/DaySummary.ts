import {KcalNutrimentsSummary} from "./KcalNutrimentsSummary";
import {MealsSummary} from "./MealsSummary";
import {FoodDiary} from "../../../types/food/FoodDiary";

export type DaySummary = {
  kcalNutrimentsSummary: KcalNutrimentsSummary,
  mealsSummary: MealsSummary;
  foodDiary: FoodDiary;
}
