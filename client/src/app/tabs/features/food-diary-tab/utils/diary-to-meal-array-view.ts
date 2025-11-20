import {FoodDiary} from "../types/FoodDiary";

export function diaryToMealArrayView(diary: FoodDiary) {
  return [
    {...diary.breakfast, name: 'Завтрак' },
    {...diary.lunch, name: 'Перекус' },
    {...diary.snack, name: 'Обед' },
    {...diary.dinner, name: 'Ужин' },
  ];
}
