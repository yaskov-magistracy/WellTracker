import {FoodEnergy} from "./FoodEnergy";

export type FoodEnergyExtended = FoodEnergy & {
  kcalBurnt: number;
}
