import {inject, Injectable} from '@angular/core';
import {DaySummaryApiService} from "../dal/day-summary.api.service";
import {rxResource} from "@angular/core/rxjs-interop";
import {UserService} from "../../../../../../../core/user/user.service";

@Injectable({
  providedIn: 'root'
})
export class DaySummaryService {

  #userS = inject(UserService);
  #daySummaryApiS = inject(DaySummaryApiService);

  #daySummary = rxResource({
    params: () => this.#userS.userInfo(),
    stream: () => this.#getDaySummary$()
  });

  daySummary = this.#daySummary.asReadonly();

  #getDaySummary$() {
    return this.#daySummaryApiS.getDaySummary$();
  }
}
