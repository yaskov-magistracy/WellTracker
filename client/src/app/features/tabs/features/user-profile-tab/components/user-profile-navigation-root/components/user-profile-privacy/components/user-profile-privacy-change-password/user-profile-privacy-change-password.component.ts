import {ChangeDetectionStrategy, Component, inject, linkedSignal} from '@angular/core';
import {
  IonBackButton, IonButton,
  IonButtons,
  IonContent,
  IonHeader,
  IonIcon, IonInput, IonInputPasswordToggle,
  IonItem,
  IonList, IonText,
  IonTitle, IonToolbar
} from "@ionic/angular/standalone";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {toSignal} from "@angular/core/rxjs-interop";
import {isEqual} from "lodash-es";
import {AccountService} from "../../../../../../../../../../core/account/account.service";
import {Router} from "@angular/router";

@Component({
    selector: 'app-user-profile-privacy-change-password',
    templateUrl: './user-profile-privacy-change-password.component.html',
    styleUrls: ['./user-profile-privacy-change-password.component.scss'],
  imports: [
    IonItem,
    IonList,
    ReactiveFormsModule,
    IonIcon,
    IonHeader,
    IonTitle,
    IonToolbar,
    IonContent,
    IonButtons,
    IonBackButton,
    IonInput,
    IonText,
    IonButton,
    IonInputPasswordToggle
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserProfilePrivacyChangePasswordComponent {

  #accountS = inject(AccountService);
  #fb = inject(FormBuilder);
  #router = inject(Router);

  changePasswordForm = this.#fb.group({
    oldPassword: this.#fb.control('', { validators: [Validators.required, Validators.minLength(1)], nonNullable: true }),
    newPassword: this.#fb.control('', { validators: [Validators.required, Validators.minLength(1)], nonNullable: true }),
  });

  protected saveBtnDisabled = this.#createSaveBtnDisabledLinkedSignal();

  changePassword() {
    this.saveBtnDisabled.set(true);
    const { oldPassword, newPassword } = this.changePasswordForm.getRawValue();
    this.#accountS.changePassword$(oldPassword, newPassword)
      .subscribe(() => this.#router.navigate(['/auth']));
  }

  #createSaveBtnDisabledLinkedSignal() {
    const changePasswordFormChanges = toSignal(
      this.changePasswordForm.valueChanges,
      { initialValue: this.changePasswordForm.value }
    );
    const changePasswordFormStatusChanges = toSignal(
      this.changePasswordForm.statusChanges,
      { initialValue: this.changePasswordForm.status }
    );
    return linkedSignal(() => {
      const changePasswordFormValue = changePasswordFormChanges();
      const changePasswordFormStatus = changePasswordFormStatusChanges();
      return changePasswordFormStatus !== 'VALID' ||
        isEqual(changePasswordFormValue.oldPassword, changePasswordFormValue.newPassword);
    });
  }
}

