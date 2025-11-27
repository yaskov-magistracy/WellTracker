import {FormControl, FormGroup} from "@angular/forms";

export type FormGroupTyped<T> = FormGroup<{
  [K in keyof T]: FormControl<T[K]>;
}>;
