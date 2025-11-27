import {ChangeDetectionStrategy, Component, CUSTOM_ELEMENTS_SCHEMA, inject, viewChild} from '@angular/core';
import {
  WeightStatisticAxisChartComponent
} from "./charts/weight-statistic-axis-chart/weight-statistic-axis-chart.component";
import {KcalStatisticBarChartComponent} from "./charts/kcal-statistic-bar-chart/kcal-statistic-bar-chart.component";
import {
  NutrientStatisticPieChartComponent
} from "./charts/nutrients-statistic-pie-chart/nutrients-statistic-pie-chart.component";
import {ViewWillEnter} from "@ionic/angular";
@Component({
  selector: 'app-diary-statistic',
  templateUrl: './diary-statistic.component.html',
  styleUrls: ['./diary-statistic.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    WeightStatisticAxisChartComponent,
    KcalStatisticBarChartComponent,
    NutrientStatisticPieChartComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class DiaryStatisticComponent {}
