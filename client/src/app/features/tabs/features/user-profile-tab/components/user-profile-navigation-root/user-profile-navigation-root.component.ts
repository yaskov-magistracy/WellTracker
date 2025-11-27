import {Component, inject } from '@angular/core';
import {
  IonActionSheet,
  IonButton,
  IonContent,
  IonHeader, IonIcon, IonItem, IonLabel,
  IonList,
  IonNavLink,
  IonTitle,
  IonToolbar
} from "@ionic/angular/standalone";
import type {OverlayEventDetail} from "@ionic/core";
import {AccountService} from "../../../../../../core/account/account.service";
import {Router} from "@angular/router";
import {UserProfileBuildInfoComponent} from "./components/user-profile-build-info/user-profile-build-info.component";
import {logoutActionSheetConstant} from "./constants/logout-action-sheet.constant";
import {UserProfilePrivacyComponent} from "./components/user-profile-privacy/user-profile-privacy.component";

@Component({
  selector: 'app-user-profile-navigation-root',
  templateUrl: './user-profile-navigation-root.component.html',
  styleUrls: ['./user-profile-navigation-root.component.scss'],
  imports: [
    IonHeader,
    IonTitle,
    IonToolbar,
    IonContent,
    IonList,
    IonNavLink,
    IonItem,
    IonLabel,
    IonIcon,
    IonButton,
    IonActionSheet
  ]
})
export class UserProfileNavigationRootComponent {

  #accountS = inject(AccountService);
  #router = inject(Router);

  protected logout(event: CustomEvent<OverlayEventDetail>) {
    if (event.detail.data.action === 'logout') {
      this.#accountS.logout$()
        .subscribe(() => this.#router.navigate(['auth']));
    }
  }

  protected readonly UserProfileBuildInfoComponent = UserProfileBuildInfoComponent;
  protected readonly logoutActionSheetConstant = logoutActionSheetConstant;
  protected readonly UserProfilePrivacyComponent = UserProfilePrivacyComponent;
}
