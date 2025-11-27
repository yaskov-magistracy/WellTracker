import {ChangeDetectionStrategy, Component, input, OnInit} from '@angular/core';
import {
  IonButton,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonIcon,
  IonText
} from "@ionic/angular/standalone";
import {RoundNumberPipe} from "../../../../../../../../../../shared/pipes/round.number.pipe";
import {FoodMealExtended} from "../../../../types/FoodMealExtended";

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
    IonCardContent
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MealItemComponent {

  meal = input.required<FoodMealExtended>();

}
