import {ChangeDetectionStrategy, Component, computed, input} from '@angular/core';
import {IonCard, IonCardContent} from "@ionic/angular/standalone";
import {NgxEchartsDirective} from "ngx-echarts";
import {
  createFoodEnergyPieChartSettings,
} from "./charts-options/create-food-energy-pie-chart-settings";
import {createNutrientsPieChartSettings} from "./charts-options/create-nutrients-pie-chart-settings";

@Component({
  selector: 'app-kcal-summary',
  templateUrl: './kcal-summary.component.html',
  styleUrls: ['./kcal-summary.component.scss'],
  imports: [
    IonCard,
    IonCardContent,
    NgxEchartsDirective
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class KcalSummaryComponent  {

  kcalRequired = input.required<number>();
  kcalConsumed = input.required<number>();
  kcalBurnt = input.required<number>();

  fatRequired = input.required<number>();
  fatConsumed = input.required<number>();

  proteinRequired = input.required<number>();
  proteinConsumed = input.required<number>();

  carbsRequired = input.required<number>();
  carbsConsumed = input.required<number>();

  protected kcalPieOptions = computed(() =>
    createFoodEnergyPieChartSettings({ required: this.kcalRequired(), consumed: this.kcalConsumed() }));

  protected nutrientsPieOptions = this.createNutrientsPieOptionsComputed();

  private createNutrientsPieOptionsComputed() {
    return computed(() => createNutrientsPieChartSettings(
      { required: this.fatRequired(), consumed: this.fatConsumed() },
      { required: this.proteinRequired(), consumed: this.proteinConsumed() },
      { required: this.carbsRequired(), consumed: this.carbsConsumed() },
    ))
  }
}

