import {ChangeDetectionStrategy, Component} from '@angular/core';
import {
  IonButtons,
  IonContent,
  IonHeader, IonMenuButton,
  IonTitle,
  IonToolbar
} from "@ionic/angular/standalone";
import {DiaryStatisticComponent} from "./components/diary-statistic/diary-statistic.component";
import {WeightTableComponent} from "./components/weight-table/weight-table.component";
import {DaySummaryComponent} from "./components/day-summary/day-summary.component";

@Component({
  selector: 'app-main-tab',
  templateUrl: './food-diary.component.html',
  styleUrls: ['./food-diary.component.scss'],
  imports: [
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    DaySummaryComponent,
    IonButtons,
    IonMenuButton,
    DiaryStatisticComponent,
    WeightTableComponent
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class FoodDiaryComponent {
}
