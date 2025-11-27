import {ChangeDetectionStrategy, Component, computed, inject, input, signal, viewChild} from '@angular/core';
import {
  ActionSheetController,
  IonActionSheet,
  IonButton,
  IonButtons,
  IonContent, IonFab, IonFabButton,
  IonHeader, IonIcon, IonInput, IonItem, IonLabel, IonList, IonModal, IonNavLink, IonNote,
  IonTitle, IonToggle, IonToolbar, ModalController
} from "@ionic/angular/standalone";
import {FoodMealExtended} from "../../../../../../types/FoodMealExtended";
import {FoodListComponent} from "../food-list/food-list.component";
import {RoundNumberPipe} from "../../../../../../../../../../../../shared/pipes/round.number.pipe";
import {
  logoutActionSheetConstant
} from "../../../../../../../../../user-profile-tab/components/user-profile-navigation-root/constants/logout-action-sheet.constant";
import type {OverlayEventDetail} from "@ionic/core";
import {FoodDiaryService} from "../../../../../../../../services/food-diary.service";
import {DaySummaryService} from "../../../../../../services/day-summary.service";
import {deleteFoodActionSheetConstant} from "./constants/deleteFoodActionSheet.constant";
import {Food} from "../../../../../../../../types/food/Food";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {FoodNutriments} from "../../../../../../../../types/food/FoodNutriments";
import {toSignal} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-meal-food-items-list',
  templateUrl: './meal-food-items-list.component.html',
  styleUrls: ['./meal-food-items-list.component.scss'],
  imports: [
    IonButtons,
    IonHeader,
    IonTitle,
    IonToolbar,
    IonButton,
    IonIcon,
    IonList,
    IonFab,
    IonContent,
    IonFabButton,
    IonNavLink,
    IonItem,
    IonLabel,
    RoundNumberPipe,
    IonActionSheet,
    IonInput,
    IonModal,
    IonToggle,
    ReactiveFormsModule,
    IonNote
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MealFoodItemsListComponent {

  #modalCtrl = inject(ModalController);
  #daySummaryS = inject(DaySummaryService);
  #actionSheetCtrl = inject(ActionSheetController);

  modal = viewChild.required(IonModal);

  meal = input.required<FoodMealExtended>();
  foodList = computed(() => this.meal().eatenFoods);
  readonly selectedFood = signal<Food | null>(null);
  readonly gramsOrPortionsControl = new FormControl<number>(
    100,
    { validators: [Validators.required, Validators.min(1)], nonNullable: true }
  );
  readonly usePortionsInput = signal(false);
  readonly totalKcalInAddedProduct =
    this.createTotalKcalInSelectedFood();
  readonly totalProteinInAddedProduct =
    this.createTotalNutrimentInSelectedFood('protein');
  readonly totalFatInAddedProduct =
    this.createTotalNutrimentInSelectedFood('fat');
  readonly totalCarbsInAddedProduct =
    this.createTotalNutrimentInSelectedFood('carbohydrates');

  protected async closeModal() {
    await this.#modalCtrl.dismiss();
  }

  protected readonly FoodListComponent = FoodListComponent;

  protected async selectFoodForChange(product: Food) {
    this.selectedFood.set(product);
    await this.modal().present();
  }

  protected changePortionsValue(usePortionsInput: boolean) {
    this.usePortionsInput.set(usePortionsInput);
    if (usePortionsInput) { this.gramsOrPortionsControl.setValue(1); }
    else {
      if (usePortionsInput) { this.gramsOrPortionsControl.setValue(100); }
    }
  }

  protected changeProduct() {
    this.#daySummaryS.changeProductInMeal$({
      foodId: this.selectedFood()!.id,
      grams: this.usePortionsInput() ?
        this.gramsOrPortionsControl.value * this.selectedFood()!.gramsInPortion :
        this.gramsOrPortionsControl.value
    }, this.meal().fieldName)
      .subscribe();
  }

  protected async deleteProduct(productId: string) {
    const actionSheet = await this.#actionSheetCtrl.create({
      header: 'Удаление продукта',
      subHeader: 'Вы уверены, что хотите удалить продукт?',
      buttons: deleteFoodActionSheetConstant,
    });

    await actionSheet.present();
    actionSheet.onWillDismiss()
      .then(ev => {
        if (ev.data.action === 'delete') {
          this.#daySummaryS.deleteProductMeal$(productId, this.meal().fieldName)
            .subscribe();
        }
      });
  }

  private createTotalNutrimentInSelectedFood(nutriment: keyof FoodNutriments) {
    const gramsOrPortionsValueSignal = toSignal(
      this.gramsOrPortionsControl.valueChanges,
      { initialValue: this.gramsOrPortionsControl.value }
    );
    return computed(() => {
      const selectedFood = this.selectedFood();
      const usePortionsInput = this.usePortionsInput();
      const gramsOrPortionsValue = gramsOrPortionsValueSignal();
      if (!selectedFood) { return 0; }
      const nutrimentsIn100GValue = selectedFood.nutriments[nutriment];
      const foodGrams = usePortionsInput ?
        selectedFood.gramsInPortion * gramsOrPortionsValue :
        gramsOrPortionsValue;
      return nutrimentsIn100GValue / 100 * foodGrams;
    })
  }

  private createTotalKcalInSelectedFood() {
    const gramsOrPortionsValueSignal = toSignal(
      this.gramsOrPortionsControl.valueChanges,
      { initialValue: this.gramsOrPortionsControl.value }
    );
    return computed(() => {
      const selectedFood = this.selectedFood();
      const usePortionsInput = this.usePortionsInput();
      const gramsOrPortionsValue = gramsOrPortionsValueSignal();
      if (!selectedFood) { return 0; }
      const kcalIn100GValue = selectedFood.energy.kcal;
      const foodGrams = usePortionsInput ?
        selectedFood.gramsInPortion * gramsOrPortionsValue :
        gramsOrPortionsValue;
      return kcalIn100GValue / 100 * foodGrams;
    })
  }
}
