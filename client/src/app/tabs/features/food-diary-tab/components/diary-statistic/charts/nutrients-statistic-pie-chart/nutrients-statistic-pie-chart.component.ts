import {ChangeDetectionStrategy, Component} from '@angular/core';
import {IonCard, IonCardContent, IonSelect, IonSelectOption} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {
  createNutrientsStatisticPieChartOptions,
} from "./chart-options/create-nutrients-statistic-pie-chart-options";

@Component({
  selector: 'app-nutrient-statistic-pie-chart',
  templateUrl: './nutrients-statistic-pie-chart.component.html',
  styleUrls: ['./nutrients-statistic-pie-chart.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    NgxEchartsDirective,
    IonSelect,
    IonSelectOption
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NutrientStatisticPieChartComponent {

  constructor() { }

  nutrientsStatisticPieChartOptions = createNutrientsStatisticPieChartOptions();

}
