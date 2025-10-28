import {FoodNutriments} from "./FoodNutriments";
import {FoodEnergy} from "./FoodEnergy";

export type Food = {
  id: string;
  name: string;
  brandName: string;
  gramsInPortion: number;
  nutriments: FoodNutriments;
  energy: FoodEnergy;
}
