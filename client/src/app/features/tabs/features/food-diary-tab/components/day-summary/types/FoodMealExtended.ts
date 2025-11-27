import {FoodMeal} from "../../../types/food/FoodMeal";
import {FoodMealType} from "../../../types/food/FoodMealType";

export type FoodMealExtended = FoodMeal & { name: string; iconName: string, fieldName: FoodMealType };
