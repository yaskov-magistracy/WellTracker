import {ChangeDetectionStrategy, Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import {
  WeightStatisticAxisChartComponent
} from "./charts/weight-statistic-axis-chart/weight-statistic-axis-chart.component";
import {KcalStatisticBarChartComponent} from "./charts/kcal-statistic-bar-chart/kcal-statistic-bar-chart.component";
@Component({
  selector: 'app-diary-statistic',
  templateUrl: './diary-statistic.component.html',
  styleUrls: ['./diary-statistic.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    WeightStatisticAxisChartComponent,
    KcalStatisticBarChartComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class DiaryStatisticComponent {
}
