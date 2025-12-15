import {
  ChangeDetectionStrategy,
  Component,
} from '@angular/core';
import { IonNav } from "@ionic/angular/standalone";
import {
  UserProfileNavigationRootComponent
} from "./components/user-profile-navigation-root/user-profile-navigation-root.component";

@Component({
  selector: 'app-user-profile-tab',
  templateUrl: './user-profile-tab.component.html',
  styleUrls: ['./user-profile-tab.component.scss'],
  imports: [
    IonNav
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class UserProfileTabComponent {
  protected readonly UserProfileNavigationRootComponent = UserProfileNavigationRootComponent;
}
