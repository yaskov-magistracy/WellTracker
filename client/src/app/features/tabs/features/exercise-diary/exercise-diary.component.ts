import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {
  IonButton,
  IonCard, IonCardContent,
  IonContent,
  IonHeader, IonIcon, IonItem, IonLabel, IonList, IonListHeader,
  IonTitle,
  IonToolbar
} from "@ionic/angular/standalone";
import {NgClass} from "@angular/common";
import {ExerciseDiary} from "./types/ExerciseDiary";
import {errorRxResource} from "../../../../core/error-handling/errorRxResource";
import {ExerciseDiaryService} from "./services/exercise-diary.service";
import {formatDate} from "../../../../core/utils/dates/format-date";
import {toSignal} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-exercise-diary-tab',
  templateUrl: './exercise-diary.component.html',
  styleUrls: ['./exercise-diary.component.scss'],
  imports: [
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonCard,
    IonItem,
    IonCardContent,
    IonLabel,
    IonIcon,
    IonList,
    IonListHeader,
    IonButton,
    NgClass
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class ExerciseDiaryComponent {

  #exerciseDiaryS = inject(ExerciseDiaryService);


  diarySig = toSignal(this.#exerciseDiaryS.getExerciseDiaryByDate$(formatDate(new Date())));

}
