import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {DaySummaryService} from "./services/day-summary.service";
import {AsyncPipe} from "@angular/common";
import {
  KcalNutrimentsDaySummaryComponent
} from "./components/kcal-nutriments-day-summary/kcal-nutriments-day-summary.component";
import {MealsDaySummaryComponent} from "./components/meals-day-summary/meals-day-summary.component";

@Component({
  selector: 'app-day-summary',
  templateUrl: './day-summary.component.html',
  styleUrls: ['./day-summary.component.scss'],
  imports: [
    AsyncPipe,
    KcalNutrimentsDaySummaryComponent,
    MealsDaySummaryComponent
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DaySummaryComponent {

  #daySummaryS = inject(DaySummaryService);
  protected daySummary$ = this.#daySummaryS.getDaySummary$();
}

