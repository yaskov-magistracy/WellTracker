import {ChangeDetectionStrategy, Component, computed, input, OnInit} from '@angular/core';
import {IonCard, IonCardContent} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {KcalNutrimentsSummary} from "../../types/KcalNutrimentsSummary";
import {createFoodEnergyPieChartSettings} from "./charts-options/create-food-energy-pie-chart-settings";
import {createNutrientsPieChartSettings} from "./charts-options/create-nutrients-pie-chart-settings";

@Component({
  selector: 'app-kcal-nutriments-day-summary',
  templateUrl: './kcal-nutriments-day-summary.component.html',
  styleUrls: ['./kcal-nutriments-day-summary.component.scss'],
  imports: [
        IonCard,
        IonCardContent,
        NgxEchartsDirective
    ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class KcalNutrimentsDaySummaryComponent {

  kcalNutrimentsSummary = input.required<KcalNutrimentsSummary>();

  protected kcalPieOptions = computed(() =>
    createFoodEnergyPieChartSettings({
      required: this.kcalNutrimentsSummary().targetEnergy.kcal,
      consumed: this.kcalNutrimentsSummary().totalEnergy.kcal
    }));

  protected nutrientsPieOptions = this.createNutrientsPieOptionsComputed();

  private createNutrientsPieOptionsComputed() {
    return computed(() => createNutrientsPieChartSettings(
      { required: this.kcalNutrimentsSummary().targetNutriments.fat, consumed: this.kcalNutrimentsSummary().totalNutriments.fat },
      { required: this.kcalNutrimentsSummary().targetNutriments.protein, consumed: this.kcalNutrimentsSummary().totalNutriments.protein },
      { required: this.kcalNutrimentsSummary().targetNutriments.carbohydrates, consumed: this.kcalNutrimentsSummary().totalNutriments.carbohydrates },
    ))
  }
}
