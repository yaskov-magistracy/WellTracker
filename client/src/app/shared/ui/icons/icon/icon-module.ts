import { NgModule } from '@angular/core';
import * as allIcons from 'ionicons/icons';
import {addIcons} from "ionicons";


@NgModule({
  declarations: [],
  imports: []
})
export class IconModule {
  constructor() {
    addIcons({
      ...allIcons,
      ai: 'assets/custom-icons/ai.svg'
    })
  }
}
