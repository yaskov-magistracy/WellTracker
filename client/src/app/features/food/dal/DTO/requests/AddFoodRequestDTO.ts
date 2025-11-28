import {Food} from "../../../../tabs/features/food-diary-tab/types/food/Food";

export type AddFoodRequestDTO = Omit<Food, 'id'>;
