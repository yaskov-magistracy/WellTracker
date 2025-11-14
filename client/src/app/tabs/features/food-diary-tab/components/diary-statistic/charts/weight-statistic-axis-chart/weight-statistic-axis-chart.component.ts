import {ChangeDetectionStrategy, Component} from '@angular/core';
import {IonCard, IonCardContent} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {createWeightStatisticLineOptions} from "./chart-options/create-weight-statistic-axis-settings";

@Component({
  selector: 'app-weight-statistic-axis-chart',
  templateUrl: './weight-statistic-axis-chart.component.html',
  styleUrls: ['./weight-statistic-axis-chart.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    NgxEchartsDirective
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class WeightStatisticAxisChartComponent {

  constructor() { }

  weightLineChartOptions = createWeightStatisticLineOptions();

}
