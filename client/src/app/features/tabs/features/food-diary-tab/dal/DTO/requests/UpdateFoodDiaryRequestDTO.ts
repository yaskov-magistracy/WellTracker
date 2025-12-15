
export type UpdateFoodDiaryRequestDTO = {
  breakfast: UpdateFoodDTO[];
  lunch: UpdateFoodDTO[];
  snack: UpdateFoodDTO[];
  dinner: UpdateFoodDTO[];
}

export type UpdateFoodDTO = { foodId: string; grams: number; }
