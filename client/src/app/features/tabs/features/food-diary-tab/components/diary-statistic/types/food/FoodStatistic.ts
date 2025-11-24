import {FoodStatisticRecord} from "./FoodStatisticRecord";
import {FoodEnergy} from "../../../../types/food/FoodEnergy";
import {FoodNutriments} from "../../../../types/food/FoodNutriments";

export type FoodStatistic = {
  "records": FoodStatisticRecord[];
  "averageEnergy": FoodEnergy,
  "targetEnergy": FoodEnergy;
  totalNutriments: FoodNutriments;
  targetNutriments: FoodNutriments;
  "advices": string[];
};
