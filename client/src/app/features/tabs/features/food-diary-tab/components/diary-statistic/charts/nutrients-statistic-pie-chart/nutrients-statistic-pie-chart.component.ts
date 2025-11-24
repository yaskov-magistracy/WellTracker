import {ChangeDetectionStrategy, Component, computed, inject, signal} from '@angular/core';
import {IonCard, IonCardContent, IonSelect, IonSelectOption, IonText} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {
  createNutrientsStatisticPieChartOptions,
} from "./chart-options/create-nutrients-statistic-pie-chart-options";
import {DiaryStatisticService} from "../../services/diary-statistic.service";
import {rxResource} from "@angular/core/rxjs-interop";
import {
  createKcalStatisticBarChartOptions
} from "../kcal-statistic-bar-chart/chart-options/create-kcal-statistic-bar-chart-options";
import { DateRangeEnum } from "src/app/core/enums/DateRange";

@Component({
  selector: 'app-nutrient-statistic-pie-chart',
  templateUrl: './nutrients-statistic-pie-chart.component.html',
  styleUrls: ['./nutrients-statistic-pie-chart.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    NgxEchartsDirective,
    IonSelect,
    IonSelectOption,
    IonText
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NutrientStatisticPieChartComponent {

  #diaryStatisticS = inject(DiaryStatisticService);

  protected currentDateRange = signal<DateRangeEnum>(DateRangeEnum.Week);

  protected foodStatisticResource = rxResource({
    params: () => this.currentDateRange(),
    stream: ({ params: dateRange }) => this.#diaryStatisticS.getFoodStatistic$(dateRange)
  })

  protected nutrientsStatisticPieChartOptions = computed(() => {
    const foodStatisticData = this.foodStatisticResource.value();
    return foodStatisticData ?
      createNutrientsStatisticPieChartOptions(foodStatisticData) :
      null;
  });

  protected readonly DateRangeEnum = DateRangeEnum;
}
