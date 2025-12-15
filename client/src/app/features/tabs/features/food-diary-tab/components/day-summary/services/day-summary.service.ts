import {computed, inject, Injectable} from '@angular/core';
import {DaySummaryApiService} from "../dal/day-summary.api.service";
import {UserService} from "../../../../../../../core/user/user.service";
import {errorRxResource} from "../../../../../../../core/error-handling/errorRxResource";
import {Food} from "../../../types/food/Food";
import {FoodDiaryService} from "../../../services/food-diary.service";
import {UpdateFoodDTO} from "../../../dal/DTO/requests/UpdateFoodDiaryRequestDTO";
import {FoodMealType} from "../../../types/food/FoodMealType";
import {tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DaySummaryService {

  #userS = inject(UserService);
  #daySummaryApiS = inject(DaySummaryApiService);
  #foodDiaryS = inject(FoodDiaryService);

  #daySummary = errorRxResource({
    params: () => this.#userS.userInfo(),
    stream: () => this.#getDaySummary$()
  });

  daySummary = this.#daySummary.asReadonly();
  dayFoodDiary = computed(() => this.daySummary.value()?.foodDiary);
  dayMeals = computed(() => this.daySummary.value()?.mealsSummary);

  #getDaySummary$() {
    return this.#daySummaryApiS.getDaySummary$();
  }

  addProductToMeal$(product: UpdateFoodDTO, mealType: FoodMealType) {
    return this.#foodDiaryS.addFoodToDiaryMeal$(product, mealType, this.dayFoodDiary()!)
      .pipe(
        tap(() => this.#daySummary.reload())
      );
  }

  changeProductInMeal$(product: UpdateFoodDTO, mealType: FoodMealType) {
    return this.#foodDiaryS.changeFoodInDiaryMeal$(product, mealType, this.dayFoodDiary()!)
      .pipe(
        tap(() => this.#daySummary.reload())
      );
  }

  deleteProductMeal$(productId: string, mealType: FoodMealType) {
    return this.#foodDiaryS.deleteFoodFromDiaryMeal$(productId, mealType, this.dayFoodDiary()!)
      .pipe(
        tap(() => this.#daySummary.reload())
      );
  }
}
