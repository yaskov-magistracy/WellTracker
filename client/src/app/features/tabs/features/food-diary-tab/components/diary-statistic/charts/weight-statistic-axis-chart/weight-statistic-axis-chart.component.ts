import {ChangeDetectionStrategy, Component, computed, EventEmitter, inject, output, signal} from '@angular/core';
import {IonCard, IonCardContent, IonSelect, IonSelectOption} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {createWeightStatisticLineOptions} from "./chart-options/create-weight-statistic-axis-settings";
import {DiaryStatisticService} from "../../services/diary-statistic.service";
import {DateRangeEnum} from "../../../../../../../../core/enums/DateRange";
import {rxResource} from "@angular/core/rxjs-interop";
import {RoundNumberPipe} from "../../../../../../../../shared/pipes/round.number.pipe";

@Component({
  selector: 'app-weight-statistic-axis-chart',
  templateUrl: './weight-statistic-axis-chart.component.html',
  styleUrls: ['./weight-statistic-axis-chart.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    NgxEchartsDirective,
    IonSelect,
    IonSelectOption,
    RoundNumberPipe
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class WeightStatisticAxisChartComponent {

  #diaryStatisticS = inject(DiaryStatisticService);

  chartInit = output();

  protected currentDateRange = signal<DateRangeEnum>(DateRangeEnum.Week);

  protected weightStatisticResource = rxResource({
    params: () => this.currentDateRange(),
    stream: ({ params: dateRange }) => this.#diaryStatisticS.getWeightStatistic$(dateRange)
  })

  protected weightLineChartOptions = computed(() => {
    const weightStatisticData = this.weightStatisticResource.value();
    return weightStatisticData ?
      createWeightStatisticLineOptions(weightStatisticData) :
      null;
  });


  protected readonly DateRangeEnum = DateRangeEnum;
}
