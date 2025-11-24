import { Component, OnInit } from '@angular/core';
import {
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonCol,
  IonGrid,
  IonIcon,
  IonRow
} from "@ionic/angular/standalone";

@Component({
  selector: 'app-weight-table',
  templateUrl: './weight-table.component.html',
  styleUrls: ['./weight-table.component.scss'],
  imports: [
    IonCard,
    IonCardTitle,
    IonCardHeader,
    IonCardContent,
    IonRow,
    IonGrid,
    IonCol,
    IonIcon
  ]
})
export class WeightTableComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
