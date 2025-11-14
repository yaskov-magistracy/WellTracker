import {ChangeDetectionStrategy, Component, signal} from '@angular/core';
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
    WeightTableComponent
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class FoodDiaryComponent {

  protected kcalRequired = signal(3708);
  protected kcalConsumed = signal(2144);
  protected kcalBurnt = signal(908);

  protected fatRequired = signal(130);
  protected fatConsumed = signal(94);

  protected proteinRequired = signal(80);
  protected proteinConsumed = signal(94);

  protected carbsRequired = signal(450);
  protected carbsConsumed = signal(173);
  protected readonly diaryMock = diaryMock;
}
