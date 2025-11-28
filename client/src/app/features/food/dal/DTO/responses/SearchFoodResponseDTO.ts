import {Food} from "../../../../tabs/features/food-diary-tab/types/food/Food";

export type SearchFoodResponseDTO = {
  items: Food[];
  totalCount: number;
}
