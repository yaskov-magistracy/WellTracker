import {
  ChangeDetectionStrategy,
  Component, computed,
  DestroyRef,
  inject,
  Injector,
  input,
  linkedSignal,
  signal, viewChild
} from '@angular/core';
import {
  IonAvatar,
  IonBackButton, IonButton,
  IonButtons,
  IonContent,
  IonHeader, IonImg,
  IonInfiniteScroll, IonInfiniteScrollContent, IonInput, IonItem, IonLabel,
  IonList, IonModal, IonNote, IonSearchbar, IonText,
  IonTitle, IonToggle,
  IonToolbar, ToastController
} from "@ionic/angular/standalone";
import {errorRxResource} from "../../../../../../../../../../../../core/error-handling/errorRxResource";
import {FoodService} from "../../../../../../../../../../../food/services/food.service";
import {map, skip, take, tap} from "rxjs";
import {takeUntilDestroyed, toObservable, toSignal} from "@angular/core/rxjs-interop";
import {Food} from "../../../../../../../../types/food/Food";
import {FoodMealExtended} from "../../../../../../types/FoodMealExtended";
import {RoundNumberPipe} from "../../../../../../../../../../../../shared/pipes/round.number.pipe";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {FoodNutriments} from "../../../../../../../../types/food/FoodNutriments";
import {DaySummaryService} from "../../../../../../services/day-summary.service";

@Component({
  selector: 'app-food-list',
  templateUrl: './food-list.component.html',
  styleUrls: ['./food-list.component.scss'],
  imports: [
    IonButtons,
    IonContent,
    IonHeader,
    IonList,
    IonTitle,
    IonToolbar,
    IonBackButton,
    IonInfiniteScroll,
    IonInfiniteScrollContent,
    IonItem,
    IonLabel,
    IonSearchbar,
    IonModal,
    IonButton,
    IonInput,
    IonToggle,
    RoundNumberPipe,
    ReactiveFormsModule,
    IonText,
    IonNote
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FoodListComponent {

  #foodS = inject(FoodService);
  #destroyRef = inject(DestroyRef);
  #injector = inject(Injector);
  #toastController = inject(ToastController);
  #daySummaryS = inject(DaySummaryService);

  modal = viewChild.required(IonModal);

  meal = input.required<FoodMealExtended>();

  readonly searchText = signal('');
  readonly #take = 20;
  readonly #skip = signal(0);
  readonly #excludedIds = linkedSignal(
    () => this.meal().eatenFoods.map(food => food.food.id)
  );
  noMore = false;
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

  #foodResource = errorRxResource({
    params: () => ({
      searchText: this.searchText(),
      skip: this.#skip(),
      excludedIds: this.#excludedIds()
    }),
    stream: ({ params }) =>
      this.#foodS.search$({
        searchText: params.searchText,
        take: this.#take,
        skip: params.skip,
        excludedIds: params.excludedIds
    }).pipe(
        tap(res => this.noMore = res.items.length < this.#take),
        map(res => res.items)
      )
  });

  foodList = linkedSignal<
    { food: Food[], searchText: string }, Food[]>({
    source: () => ({
      food: this.#foodResource.value()!,
      searchText: this.searchText()
    }),
    computation: (source, previous) => {
      if (
        (!source.food && previous?.value) &&
        source.searchText === previous.source.searchText
      ) { return previous.value; }
      const previousFoodItems = previous?.value ?? [];
      const newFoodItems = source.searchText !== previous?.source.searchText ?
        source.food :
        [...previousFoodItems, ...source.food];
      return newFoodItems ?? [];
    },
  });

  protected async selectFoodForAdd(food: Food) {
    this.selectedFood.set(food);
    await this.modal().present();
  }

  protected updateSearchTextValue(newSearchText: string) {
    this.searchText.set(newSearchText.toLowerCase().trim());
    this.#skip.set(0);
    this.noMore = false;
  }

  protected async scrollReachedEnd(event: CustomEvent) {
    const infiniteScroll = (event.target as unknown as IonInfiniteScroll)
    if (this.#foodResource.isLoading() || this.noMore) {
      await infiniteScroll.complete()
      return;
    }
    this.#skip.update(currentSkip => currentSkip + 20);
    toObservable(this.#foodResource.isLoading, { injector: this.#injector })
      .pipe(
        skip(1),
        take(1),
        takeUntilDestroyed(this.#destroyRef)
      ).subscribe(() => infiniteScroll.complete())
  };

  protected changePortionsValue(usePortionsInput: boolean) {
    this.usePortionsInput.set(usePortionsInput);
    if (usePortionsInput) { this.gramsOrPortionsControl.setValue(1); }
    else {
      if (usePortionsInput) { this.gramsOrPortionsControl.setValue(100); }
    }
  }

  protected async addProductToMeal(product: Food) {
    this.#daySummaryS.addProductToMeal$({
      foodId: product.id,
      grams: this.usePortionsInput() ?
        this.gramsOrPortionsControl.value * this.selectedFood()!.gramsInPortion :
        this.gramsOrPortionsControl.value
    }, this.meal().fieldName)
      .subscribe(async () => {
        this.#excludedIds.update(excludedIds => [...excludedIds, product.id]);
        const toast = await this.#toastController.create({
          message: 'Продукт успешно добавлен',
          duration: 1500,
          position: 'bottom',
          color: 'success'
        });
        await toast.present();
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
