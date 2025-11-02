import {ChangeDetectionStrategy, Component, inject, OnInit} from '@angular/core';
import {
  IonButton,
  IonContent,
  IonHeader,
  IonInput,
  IonInputPasswordToggle,
  IonText,
  IonTitle,
  IonToolbar,
  IonRouterLink, IonCard, IonCardContent
} from "@ionic/angular/standalone";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {AccountService} from "../../data-access/account.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
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
    IonCard,
    IonCardContent,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class LoginPage {

  private accountS = inject(AccountService);
  private router = inject(Router);

  private fb = inject(FormBuilder);

  protected loginForm = this.fb.group({
    login: this.fb.control('', { validators: [Validators.required, Validators.minLength(1)], nonNullable: true }),
    password: this.fb.control('', { validators: [Validators.required, Validators.minLength(1)], nonNullable: true }),
  });

  login() {
    this.accountS.login$(this.loginForm.getRawValue())
      .subscribe(() => this.router.navigate(['']));
  }

}
