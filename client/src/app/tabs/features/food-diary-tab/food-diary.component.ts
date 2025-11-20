import {ChangeDetectionStrategy, Component, inject, signal} from '@angular/core';
import {
  IonButtons,
  IonContent,
  IonHeader, IonMenuButton,
  IonTitle,
  IonToolbar
} from "@ionic/angular/standalone";
import {KcalSummaryComponent} from "./components/kcal-summary/kcal-summary.component";
import {TodayMealsComponent} from "./components/today-meals/today-meals.component";
import {diaryMock} from "./mocks/diary";
import {DiaryStatisticComponent} from "./components/diary-statistic/diary-statistic.component";
import {WeightTableComponent} from "./components/weight-table/weight-table.component";
import {FoodDiaryService} from "./services/food-diary.service";
import {AsyncPipe} from "@angular/common";

@Component({
  selector: 'app-main-tab',
  templateUrl: './food-diary.component.html',
  styleUrls: ['./food-diary.component.scss'],
  imports: [
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    KcalSummaryComponent,
    IonButtons,
    IonMenuButton,
    TodayMealsComponent,
    DiaryStatisticComponent,
    WeightTableComponent,
    AsyncPipe
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class FoodDiaryComponent {

  private _foodDiaryS = inject(FoodDiaryService);

  todayFoodDiary$ = this._foodDiaryS.getFoodDiaryByDate$(new Date().toLocaleDateString());

  protected readonly diaryMock = diaryMock;
}
