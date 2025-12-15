import {ChangeDetectionStrategy, Component } from '@angular/core';
import {
  IonBackButton,
  IonButtons,
  IonContent,
  IonHeader,
  IonIcon,
  IonItem, IonLabel,
  IonList, IonNavLink,
  IonTitle, IonToolbar
} from "@ionic/angular/standalone";
import {ReactiveFormsModule} from "@angular/forms";
import {
  UserProfilePrivacyChangePasswordComponent
} from "./components/user-profile-privacy-change-password/user-profile-privacy-change-password.component";

@Component({
    selector: 'app-user-profile-privacy',
    templateUrl: './user-profile-privacy.component.html',
    styleUrls: ['./user-profile-privacy.component.scss'],
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
    IonLabel,
    IonNavLink
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserProfilePrivacyComponent {
  protected readonly UserProfilePrivacyChangePasswordComponent = UserProfilePrivacyChangePasswordComponent;
}

