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
      ai: 'assets/custom-icons/ai.svg',
      breakfast: 'assets/custom-icons/breakfast.svg',
      lunch: 'assets/custom-icons/lunch.svg',
      snack: 'assets/custom-icons/snack.svg',
      dinner: 'assets/custom-icons/dinner.svg'
    })
  }
}
