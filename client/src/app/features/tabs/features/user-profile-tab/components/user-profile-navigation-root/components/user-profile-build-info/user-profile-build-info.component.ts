import {ChangeDetectionStrategy, Component, effect, inject, Injector, input, linkedSignal, OnInit} from '@angular/core';
import {
  IonBackButton, IonButton,
  IonButtons,
  IonContent,
  IonHeader,
  IonIcon,
  IonInput,
  IonItem,
  IonList,
  IonSelect,
  IonSelectOption,
  IonText, IonTitle, IonToolbar
} from "@ionic/angular/standalone";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {GenderEnum} from "../../../../../../../../core/enums/GenderEnum";
import {toSignal} from "@angular/core/rxjs-interop";
import {isEqual} from "lodash-es";
import {UserService} from "../../../../../../../../core/user/user.service";

@Component({
    selector: 'app-user-profile-build-info',
    templateUrl: './user-profile-build-info.component.html',
    styleUrls: ['./user-profile-build-info.component.scss'],
  imports: [
    IonInput,
    IonItem,
    IonList,
    ReactiveFormsModule,
    IonSelect,
    IonSelectOption,
    IonText,
    IonIcon,
    IonHeader,
    IonTitle,
    IonToolbar,
    IonContent,
    IonButtons,
    IonBackButton,
    IonButton
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserProfileBuildInfoComponent implements OnInit {

  #userS = inject(UserService);
  #fb = inject(FormBuilder);
  #injector = inject(Injector);

  protected userBuildFormGroup = this.#createUserBuildFormGroup();
  protected saveBtnDisabled = this.#createSaveBtnDisabledLinkedSignal();

  #userInfo = this.#userS.userInfo;

  ngOnInit() {
    this.#listenUserChanges();
  }

  protected saveChanges() {
    this.saveBtnDisabled.set(true);
    this.#userS.updateUserInfo$(this.userBuildFormGroup.getRawValue()).subscribe();
  }

  #createSaveBtnDisabledLinkedSignal() {
    const userInfoFormChanges = toSignal(
      this.userBuildFormGroup.valueChanges,
      { initialValue: this.userBuildFormGroup.value }
    );
    const userInfoStatusChanges = toSignal(
      this.userBuildFormGroup.statusChanges,
      { initialValue: this.userBuildFormGroup.status }
    );
    return linkedSignal(() => {
      const userInfo = this.#userInfo();
      const userInfoFormStatus = userInfoStatusChanges();
      const userInfoFormValue = userInfoFormChanges();
      return userInfoFormStatus !== 'VALID' || isEqual(userInfo, userInfoFormValue);
    });
  }

  #createUserBuildFormGroup() {
    return this.#fb.group({
      height: this.#fb.control(0, { validators: [Validators.required, Validators.min(100)], nonNullable: true }),
      weight: this.#fb.control(0, { validators: [Validators.required, Validators.min(30)], nonNullable: true }),
      gender: this.#fb.control(GenderEnum.Male, { validators: [Validators.required], nonNullable: true }),
      targetWeight: this.#fb.control(0, { validators: [Validators.required, Validators.min(30)], nonNullable: true })
    });
  }

  #listenUserChanges() {
    effect(() => {
      const userInfo = this.#userInfo();
      if (
        userInfo &&
        !isEqual(this.userBuildFormGroup.value, userInfo)
      ) {
        this.userBuildFormGroup.setValue(userInfo);
      }
    }, { injector: this.#injector });
  }

  protected readonly GenderEnum = GenderEnum;
}
