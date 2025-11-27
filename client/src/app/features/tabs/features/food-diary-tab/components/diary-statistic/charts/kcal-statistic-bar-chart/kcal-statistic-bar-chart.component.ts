import {ChangeDetectionStrategy, Component, computed, DestroyRef, inject, OnInit, signal} from '@angular/core';
import {IonCard, IonCardContent, IonSelect, IonSelectOption, IonText} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {
  createKcalStatisticBarChartOptions,
} from "./chart-options/create-kcal-statistic-bar-chart-options";
import {DateRangeEnum} from "../../../../../../../../core/enums/DateRange";
import {DiaryStatisticService} from "../../services/diary-statistic.service";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {RoundNumberPipe} from "../../../../../../../../shared/pipes/round.number.pipe";
import {IDiaryStatisticComponent} from "../../types/IDiaryStatisticComponent";
import {FoodDiaryTabService} from "../../../../services/food-diary-tab.service";
import {errorRxResource} from "../../../../../../../../core/error-handling/errorRxResource";

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
    RoundNumberPipe
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class KcalStatisticBarChartComponent implements OnInit, IDiaryStatisticComponent {

  #diaryStatisticS = inject(DiaryStatisticService);
  #foodDiaryTabS = inject(FoodDiaryTabService);
  #destroyRef = inject(DestroyRef);

  protected currentDateRange = signal<DateRangeEnum>(DateRangeEnum.Week);

  foodStatisticResource = errorRxResource({
    params: () => this.currentDateRange(),
    stream: ({ params: dateRange }) => this.#diaryStatisticS.getFoodStatistic$(dateRange)
  })

  protected kcalStatisticBarChartOptions = computed(() => {
    const foodStatisticData = this.foodStatisticResource.value();
    return foodStatisticData ?
      createKcalStatisticBarChartOptions(foodStatisticData) :
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
      .subscribe(() => this.foodStatisticResource.reload());
  }

  protected readonly DateRangeEnum = DateRangeEnum;
}
