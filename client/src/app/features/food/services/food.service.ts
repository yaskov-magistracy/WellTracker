import {inject, Injectable} from '@angular/core';
import {FoodApiService} from "../dal/food.api.service";
import {AddFoodRequestDTO} from "../dal/DTO/requests/AddFoodRequestDTO";
import {SearchFoodRequestDTO} from "../dal/DTO/requests/SearchFoodRequestDTO";
import {map} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  private foodApiS = inject(FoodApiService);

  search$(searchParams: SearchFoodRequestDTO) {
    return this.foodApiS.search$(searchParams);
  }

  addFood$(addFoodRequestDTO: AddFoodRequestDTO) {
    return this.foodApiS.addFood$(addFoodRequestDTO);
  }
}
