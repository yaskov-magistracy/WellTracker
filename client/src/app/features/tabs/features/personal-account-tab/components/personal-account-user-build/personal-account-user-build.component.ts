import {ChangeDetectionStrategy, Component, input} from '@angular/core';
import {IonIcon, IonInput, IonItem, IonList, IonSelect, IonSelectOption, IonText} from "@ionic/angular/standalone";
import {ReactiveFormsModule} from "@angular/forms";
import {FormGroupTyped} from "../../../../../../core/types/FormGroupTyped";
import {UserInfo} from "../../../../../../core/user/types/UserInfo";
import {GenderEnum} from "../../../../../../core/enums/GenderEnum";

@Component({
    selector: 'app-personal-account-user-build',
    templateUrl: './personal-account-user-build.component.html',
    styleUrls: ['./personal-account-user-build.component.scss'],
  imports: [
    IonInput,
    IonItem,
    IonList,
    ReactiveFormsModule,
    IonSelect,
    IonSelectOption,
    IonText,
    IonIcon
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PersonalAccountUserBuildComponent {
  userBuildFormGroup = input.required<FormGroupTyped<UserInfo>>();
  protected readonly GenderEnum = GenderEnum;
}
