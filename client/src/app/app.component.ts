import {ChangeDetectionStrategy, Component} from '@angular/core';
import {IonApp, IonRouterOutlet} from "@ionic/angular/standalone";
import * as allIcons from 'ionicons/icons';
import {addIcons} from "ionicons";
import {IconModule} from "./shared/ui/icons/icon/icon-module";

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  imports: [
    IonApp,
    IonRouterOutlet,
    IconModule
  ],
  styleUrls: ['app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {

  constructor() {
    addIcons({
      ...allIcons,

    })
    fetch('/api/accounts/session');
  }
}
