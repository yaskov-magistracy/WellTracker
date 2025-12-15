import {ChangeDetectionStrategy, Component, computed, DestroyRef, inject, OnInit, signal} from '@angular/core';
import {IonCard, IonCardContent, IonSelect, IonSelectOption, IonText} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {
  createNutrientsStatisticPieChartOptions,
} from "./chart-options/create-nutrients-statistic-pie-chart-options";
import {DiaryStatisticService} from "../../services/diary-statistic.service";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import { DateRangeEnum } from "src/app/core/enums/DateRange";
import {IDiaryStatisticComponent} from "../../types/IDiaryStatisticComponent";
import {FoodDiaryTabService} from "../../../../services/food-diary-tab.service";
import {errorRxResource} from "../../../../../../../../core/error-handling/errorRxResource";

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
export class NutrientStatisticPieChartComponent implements OnInit, IDiaryStatisticComponent {

  #diaryStatisticS = inject(DiaryStatisticService);
  #foodDiaryTabS = inject(FoodDiaryTabService);
  #destroyRef = inject(DestroyRef);

  protected currentDateRange = signal<DateRangeEnum>(DateRangeEnum.Week);

  foodStatisticResource = errorRxResource({
    params: () => this.currentDateRange(),
    stream: ({ params: dateRange }) => this.#diaryStatisticS.getFoodStatistic$(dateRange)
  })

  protected nutrientsStatisticPieChartOptions = computed(() => {
    const foodStatisticData = this.foodStatisticResource.value();
    return foodStatisticData ?
      createNutrientsStatisticPieChartOptions(foodStatisticData) :
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
