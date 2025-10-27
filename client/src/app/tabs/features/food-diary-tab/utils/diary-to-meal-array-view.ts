import {FoodDiary} from "../types/FoodDiary";
import {FoodDiarySimplified} from "../types/FoodDiarySimplified";

export function diaryToMealArrayView (diary: FoodDiary): FoodDiarySimplified[] {
  return [
    { name: 'Завтрак', kcalConsumed: 800 },
    { name: 'Обед', kcalConsumed: 576 },
    { name: 'Перекус', kcalConsumed: 400 },
    { name: 'Ужин', kcalConsumed: 1000 }
  ];
}
