import {ChangeDetectionStrategy, Component, computed, CUSTOM_ELEMENTS_SCHEMA, input, OnInit} from '@angular/core';
import {IonButton, IonCard, IonCardContent, IonCardHeader, IonCardTitle, IonIcon} from "@ionic/angular/standalone";
import {createFoodEnergyProgressBarSettings} from "./charts-options/create-food-energy-progress-bar-settings";
import {NgxEchartsDirective} from "ngx-echarts";
import {MealsSummary} from "../../types/MealsSummary";
import {RoundNumberPipe} from "../../../../../../../shared/pipes/round.number.pipe";

@Component({
  selector: 'app-meals-day-summary',
  templateUrl: './meals-day-summary.component.html',
  styleUrls: ['./meals-day-summary.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    IonCardHeader,
    IonCardTitle,
    IonIcon,
    NgxEchartsDirective,
    IonButton,
    RoundNumberPipe,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class MealsDaySummaryComponent {

  mealsSummary = input.required<MealsSummary>();
  mealsSummaryArray = computed(() => Object.values(this.mealsSummary()));

  todayMealsKcalProgressBarSettings = computed(() => {
    const mealsSummaryArray = this.mealsSummaryArray();
    return mealsSummaryArray.map(meal => createFoodEnergyProgressBarSettings({
      consumed: meal.totalEnergy.kcal,
      required: 1000
    }))
  });
}
