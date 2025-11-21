import { Pipe, PipeTransform } from '@angular/core';
import {roundNumber} from "../utils/round-number";

@Pipe({
  name: 'roundNumber'
})
export class RoundNumberPipe implements PipeTransform {

  transform(value: number, precision = 2, asNumber = true) {
    return roundNumber(value, precision, asNumber);
  }
}
