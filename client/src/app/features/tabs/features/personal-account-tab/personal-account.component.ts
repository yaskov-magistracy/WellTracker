import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  Injector,
  linkedSignal,
  OnInit,
} from '@angular/core';
import {
  IonActionSheet,
  IonButton,
  IonContent,
  IonHeader,
  IonTitle,
  IonToolbar, ViewDidLeave,
} from "@ionic/angular/standalone";
import {PersonalAccountUserBuildComponent} from "./components/personal-account-user-build/personal-account-user-build.component";
import {FormBuilder, Validators} from "@angular/forms";
import {isEqual} from "lodash-es";
import {UserService} from "../../../../core/user/user.service";
import {GenderEnum} from "../../../../core/enums/GenderEnum";
import {toSignal} from "@angular/core/rxjs-interop";
import {AccountService} from "../../../../core/account/account.service";
import {Router} from "@angular/router";
import type { OverlayEventDetail } from '@ionic/core';

@Component({
  selector: 'app-personal-account-tab',
  templateUrl: './personal-account.component.html',
  styleUrls: ['./personal-account.component.scss'],
  imports: [
    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    PersonalAccountUserBuildComponent,
    IonButton,
    IonActionSheet,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class PersonalAccountComponent implements OnInit, ViewDidLeave {

  #userS = inject(UserService);
  #accountS = inject(AccountService);
  #fb = inject(FormBuilder);
  #router = inject(Router);
  #injector = inject(Injector);


  #userInfo = this.#userS.userInfo;

  protected userInfoForm = this.#fb.group({
    height: this.#fb.control(0, { validators: [Validators.required, Validators.min(100)], nonNullable: true }),
    weight: this.#fb.control(0, { validators: [Validators.required, Validators.min(30)], nonNullable: true }),
    gender: this.#fb.control(GenderEnum.Male, { validators: [Validators.required], nonNullable: true }),
    targetWeight: this.#fb.control(0, { validators: [Validators.required, Validators.min(30)], nonNullable: true })
  });

  protected saveBtnDisabled = this.#createSaveBtnDisabledLinkedSignal();

  protected   actionSheetButtons = [
    {
      text: 'Выйти',
      role: 'destructive',
      data: {
        action: 'logout',
      },
    },
    {
      text: 'Отмена',
      role: 'cancel',
      data: {
        action: 'cancel',
      },
    },
  ];


  ngOnInit() {
    this.#listenUserChanges();
  }

  protected saveChanges() {
    this.saveBtnDisabled.set(true);
    this.#userS.updateUserInfo$(this.userInfoForm.getRawValue()).subscribe();
  }

  protected logout(event: CustomEvent<OverlayEventDetail>) {
    if (event.detail.data.action === 'logout') {
      this.#accountS.logout$()
        .subscribe(() => this.#router.navigate(['auth']));
    }
  }

  #createSaveBtnDisabledLinkedSignal() {
    const userInfoFormChanges = toSignal(this.userInfoForm.valueChanges, { initialValue: this.userInfoForm.value });
    const userInfoStatusChanges = toSignal(this.userInfoForm.statusChanges, { initialValue: this.userInfoForm.status });
    return linkedSignal(() => {
      const userInfo = this.#userInfo();
      const userInfoFormStatus = userInfoStatusChanges();
      const userInfoFormValue = userInfoFormChanges();
      return userInfoFormStatus !== 'VALID' || isEqual(userInfo, userInfoFormValue);
    });
  }

  #listenUserChanges() {
    effect(() => {
      const userInfo = this.#userInfo();
      if (
        userInfo &&
        !isEqual(this.userInfoForm.value, userInfo)
      ) {
        this.userInfoForm.setValue(userInfo);
      }
    }, { injector: this.#injector });
  }

  ionViewDidLeave(): void {
    this.userInfoForm.setValue(this.#userInfo()!);
  }
}
