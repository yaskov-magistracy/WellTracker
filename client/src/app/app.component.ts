import { Component } from '@angular/core';
import {IonApp, IonIcon, IonRouterOutlet} from "@ionic/angular/standalone";
import {addIcons} from "ionicons";
import {ellipse, square, triangle} from "ionicons/icons";

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  imports: [
    IonApp,
    IonRouterOutlet,
    IonIcon
  ],
  styleUrls: ['app.component.scss']
})
export class AppComponent {
  constructor() {
    addIcons({ square, triangle, ellipse })
    fetch('/api/accounts/session');
  }
}
