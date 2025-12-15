import {FoodNutriments} from "../../../types/food/FoodNutriments";
import {FoodEnergy} from "../../../types/food/FoodEnergy";
import {FoodEnergyExtended} from "../../../types/food/FoodEnergyWithBurnt";

export type KcalNutrimentsSummary = {
  totalNutriments: FoodNutriments;
  totalEnergy: FoodEnergyExtended;
  targetNutriments: FoodNutriments;
  targetEnergy: FoodEnergy;
}
