import {inject, Injectable} from '@angular/core';
import {FoodDiaryApiService} from "../dal/food-diary.api.service";

@Injectable({
  providedIn: 'root'
})
export class FoodDiaryService {

  private foodDiaryApiS = inject(FoodDiaryApiService);

  getFoodDiaryByDate$(date: string) {
    return this.foodDiaryApiS.getFoodDiaryByDate$(date);
  }
}
