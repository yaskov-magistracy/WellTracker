import {ChangeDetectionStrategy, Component, CUSTOM_ELEMENTS_SCHEMA, inject, signal} from '@angular/core';
import {
  IonButton, IonCard, IonCardContent,
  IonContent,
  IonHeader, IonIcon,
  IonInput,
  IonInputPasswordToggle, IonItem, IonList, IonSelect, IonSelectOption, IonText,
  IonTitle,
  IonToolbar,
} from "@ionic/angular/standalone";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {AccountService} from "../../../../core/account/account.service";
import {GenderEnum} from "../../../../core/enums/GenderEnum";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.page.html',
  styleUrls: ['./signup.page.scss'],
  imports: [
    IonHeader,
    IonTitle,
    IonToolbar,
    IonContent,
    IonInput,
    IonInputPasswordToggle,
    IonButton,
    ReactiveFormsModule,
    RouterLink,
    IonSelect,
    IonSelectOption,
    IonCard,
    IonCardContent,
    IonIcon,
    IonItem,
    IonText,
    IonList,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export default class SignupPage {

  private accountS = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);

  protected signUpFirstStepForm = this.fb.group({
    login: this.fb.control('', { validators: [Validators.required, Validators.minLength(1)], nonNullable: true }),
    password: this.fb.control('', { validators: [Validators.required, Validators.minLength(1)], nonNullable: true }),
  });

  protected signUpSecondStepForm = this.fb.group({
    height: this.fb.control(0, { validators: [Validators.required, Validators.min(100)], nonNullable: true }),
    weight: this.fb.control(0, { validators: [Validators.required, Validators.min(30)], nonNullable: true }),
    gender: this.fb.control(GenderEnum.Male, { validators: [Validators.required], nonNullable: true }),
    targetWeight: this.fb.control(0, { validators: [Validators.required, Validators.min(30)], nonNullable: true }),
    tgChatId: this.fb.control(null, { validators: [Validators.min(1)] }),
  });

  protected signUp() {
    this.accountS.register$({
      ...this.signUpFirstStepForm.getRawValue(),
      ...this.signUpSecondStepForm.getRawValue()
    })
      .subscribe(() => this.router.navigate(['/', 'auth', 'login']));
  }

  protected readonly GenderEnum = GenderEnum;
}
