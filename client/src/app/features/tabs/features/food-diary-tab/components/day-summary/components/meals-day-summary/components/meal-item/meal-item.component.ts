import {ChangeDetectionStrategy, Component, inject, input, OnInit} from '@angular/core';
import {
  IonButton,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle, IonContent,
  IonIcon, IonModal, IonNav,
  IonText, ModalController
} from "@ionic/angular/standalone";
import {RoundNumberPipe} from "../../../../../../../../../../shared/pipes/round.number.pipe";
import {FoodMealExtended} from "../../../../types/FoodMealExtended";
import {MealFoodItemsListComponent} from "./components/meal-food-items-list/meal-food-items-list.component";
import {FoodDiaryTabService} from "../../../../../../services/food-diary-tab.service";

@Component({
  selector: 'app-meal-item',
  templateUrl: './meal-item.component.html',
  styleUrls: ['./meal-item.component.scss'],
  imports: [
    IonCard,
    IonCardHeader,
    IonIcon,
    IonText,
    IonCardTitle,
    IonButton,
    RoundNumberPipe,
    IonCardContent,
    IonModal,
    IonNav,
    IonContent
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MealItemComponent {

  #foodDiaryTabS = inject(FoodDiaryTabService);
  #modalCtrl = inject(ModalController);

  meal = input.required<FoodMealExtended>();

  foodDiaryTabActivated$ = this.#foodDiaryTabS.foodDiaryTabActivated$;

  protected readonly MealFoodItemsListComponent = MealFoodItemsListComponent;
}
