import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {SearchFoodRequestDTO} from "./DTO/requests/SearchFoodRequestDTO";
import {Food} from "../../tabs/features/food-diary-tab/types/food/Food";
import {AddFoodRequestDTO} from "./DTO/requests/AddFoodRequestDTO";
import {SearchFoodResponseDTO} from "./DTO/responses/SearchFoodResponseDTO";

@Injectable({
  providedIn: 'root'
})
export class FoodApiService {

  #httpClient = inject(HttpClient);

  #apiPath = 'Foods';

  search$(searchParams: SearchFoodRequestDTO) {
    return this.#httpClient.post<SearchFoodResponseDTO>(`/api/${this.#apiPath}/search`, searchParams);
  }

  addFood$(addFoodRequestDTO: AddFoodRequestDTO) {
    return this.#httpClient.post<Food>(`/api/${this.#apiPath}`, addFoodRequestDTO);
  }

}
