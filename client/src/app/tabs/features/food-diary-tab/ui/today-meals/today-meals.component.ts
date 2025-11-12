import {ChangeDetectionStrategy, Component, computed, CUSTOM_ELEMENTS_SCHEMA, input, OnInit} from '@angular/core';
import {IonButton, IonCard, IonCardContent, IonCardHeader, IonCardTitle, IonIcon} from "@ionic/angular/standalone";
import {FoodDiary} from "../../types/FoodDiary";
import {diaryToMealArrayView} from "../../utils/diary-to-meal-array-view";
import {createFoodEnergyProgressBarSettings} from "./charts-options/create-food-energy-progress-bar-settings";
import {NgxEchartsDirective} from "ngx-echarts";

@Component({
  selector: 'app-today-meals',
  templateUrl: './today-meals.component.html',
  styleUrls: ['./today-meals.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    IonCardHeader,
    IonCardTitle,
    IonIcon,
    NgxEchartsDirective,
    IonButton,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class TodayMealsComponent {

  diary = input.required<FoodDiary>();
  mealsArray = computed(() => diaryToMealArrayView(this.diary()));

  todayMealsKcalProgressBarSettings = createFoodEnergyProgressBarSettings({ required: 1000, consumed: 500 });
}
