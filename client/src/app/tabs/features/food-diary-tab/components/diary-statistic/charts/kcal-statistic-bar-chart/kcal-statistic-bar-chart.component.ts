import {ChangeDetectionStrategy, Component} from '@angular/core';
import {IonCard, IonCardContent} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {
  createKcalStatisticBarChartOptions,
} from "./chart-options/create-kcal-statistic-bar-chart-options";

@Component({
  selector: 'app-kcal-statistic-bar-chart',
  templateUrl: './kcal-statistic-bar-chart.component.html',
  styleUrls: ['./kcal-statistic-bar-chart.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    NgxEchartsDirective
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class KcalStatisticBarChartComponent {

  constructor() { }

  kcalStatisticBarChartOptions = createKcalStatisticBarChartOptions();

}
