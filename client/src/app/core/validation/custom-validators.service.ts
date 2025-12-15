import {Injectable} from "@angular/core";
import {EatenFood} from "../../features/tabs/features/food-diary-tab/types/food/EatenFood";
import {AbstractControl} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class CustomValidatorsService {

  nonEmptyTrimmedValidator(control: AbstractControl) {
    const value = control.value;
    if (value == null) return { emptyTrimmed: true };
    // Убираем все пробелы (включая табы и переносы строк)
    const trimmedLength = value.replace(/\s+/g, '').length;
    return trimmedLength > 0 ? null : { emptyTrimmed: true };  }
}
