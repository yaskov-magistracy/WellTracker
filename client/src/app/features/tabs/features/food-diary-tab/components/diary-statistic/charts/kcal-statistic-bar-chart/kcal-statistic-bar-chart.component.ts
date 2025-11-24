import {ChangeDetectionStrategy, Component, computed, inject, signal} from '@angular/core';
import {IonCard, IonCardContent, IonSelect, IonSelectOption, IonText} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {
  createKcalStatisticBarChartOptions,
} from "./chart-options/create-kcal-statistic-bar-chart-options";
import {DateRangeEnum} from "../../../../../../../../core/enums/DateRange";
import {DiaryStatisticService} from "../../services/diary-statistic.service";
import {rxResource} from "@angular/core/rxjs-interop";
import {RoundNumberPipe} from "../../../../../../../../shared/pipes/round.number.pipe";

@Component({
  selector: 'app-kcal-statistic-bar-chart',
  templateUrl: './kcal-statistic-bar-chart.component.html',
  styleUrls: ['./kcal-statistic-bar-chart.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    NgxEchartsDirective,
    IonSelect,
    IonSelectOption,
    RoundNumberPipe,
    IonText
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class KcalStatisticBarChartComponent {

  #diaryStatisticS = inject(DiaryStatisticService);

  protected currentDateRange = signal<DateRangeEnum>(DateRangeEnum.Week);

  protected foodStatisticResource = rxResource({
    params: () => this.currentDateRange(),
    stream: ({ params: dateRange }) => this.#diaryStatisticS.getFoodStatistic$(dateRange)
  })

  protected kcalStatisticBarChartOptions = computed(() => {
    const foodStatisticData = this.foodStatisticResource.value();
    return foodStatisticData ?
      createKcalStatisticBarChartOptions(foodStatisticData) :
      null;
  });

  protected readonly DateRangeEnum = DateRangeEnum;
}
