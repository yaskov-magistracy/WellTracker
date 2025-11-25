import {ChangeDetectionStrategy, Component, computed, DestroyRef, inject, OnInit, output, signal} from '@angular/core';
import {IonCard, IonCardContent, IonSelect, IonSelectOption} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {createWeightStatisticLineOptions} from "./chart-options/create-weight-statistic-axis-settings";
import {DiaryStatisticService} from "../../services/diary-statistic.service";
import {DateRangeEnum} from "../../../../../../../../core/enums/DateRange";
import {rxResource, takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {RoundNumberPipe} from "../../../../../../../../shared/pipes/round.number.pipe";
import {IDiaryStatisticComponent} from "../../types/IDiaryStatisticComponent";
import {FoodDiaryTabService} from "../../../../services/food-diary-tab.service";

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
export class WeightStatisticAxisChartComponent implements OnInit, IDiaryStatisticComponent {

  #diaryStatisticS = inject(DiaryStatisticService);
  #foodDiaryTabS = inject(FoodDiaryTabService);
  #destroyRef = inject(DestroyRef);

  chartInit = output();

  protected currentDateRange = signal<DateRangeEnum>(DateRangeEnum.Week);

  weightStatisticResource = rxResource({
    params: () => this.currentDateRange(),
    stream: ({ params: dateRange }) => this.#diaryStatisticS.getWeightStatistic$(dateRange)
  })

  protected weightLineChartOptions = computed(() => {
    const weightStatisticData = this.weightStatisticResource.value();
    return weightStatisticData ?
      createWeightStatisticLineOptions(weightStatisticData) :
      null;
  });

  ngOnInit() {
    this.listenFoodDiaryTabActivated();
  }

  listenFoodDiaryTabActivated() {
    this.#foodDiaryTabS.foodDiaryTabActivated$
      .pipe(
        takeUntilDestroyed(this.#destroyRef)
      )
      .subscribe(() => this.weightStatisticResource.reload());
  }

  protected readonly DateRangeEnum = DateRangeEnum;
}
