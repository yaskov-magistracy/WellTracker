import {FoodStatisticRecord} from "./FoodStatisticRecord";
import {FoodEnergyNutriments} from "../../../../types/food/FoodEnergyNutriments";

export type FoodStatistic = {
  "records": FoodStatisticRecord[];
  average: FoodEnergyNutriments,
  target: FoodEnergyNutriments;
  total: FoodEnergyNutriments;
  "advices": string[];
};
