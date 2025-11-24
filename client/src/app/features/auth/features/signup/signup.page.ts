import {ChangeDetectionStrategy, Component, CUSTOM_ELEMENTS_SCHEMA, inject, signal} from '@angular/core';
import {
  IonButton, IonCard, IonCardContent,
  IonContent,
  IonHeader,
  IonInput,
  IonInputPasswordToggle, IonSelect, IonSelectOption,
  IonTitle,
  IonToolbar,
} from "@ionic/angular/standalone";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {UserTargetEnum} from "../../types/UserTargetEnum";
import {UserGenderEnum} from "../../types/UserGenderEnum";
import {AccountService} from "../../data-access/account.service";

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
    gender: this.fb.control(UserGenderEnum.Male, { validators: [Validators.required], nonNullable: true }),
    target: this.fb.control(UserTargetEnum.KeepWeight, { validators: [Validators.required], nonNullable: true }),
  });

  protected signUp() {
    this.accountS.register$({
      ...this.signUpFirstStepForm.getRawValue(),
      ...this.signUpSecondStepForm.getRawValue()
    })
      .subscribe(() => this.router.navigate(['/', 'auth', 'login']));
  }

  protected readonly UserGenderEnum = UserGenderEnum;
  protected readonly UserTargetEnum = UserTargetEnum;
}
