import {inject, Injectable} from '@angular/core';
import {DaySummaryApiService} from "../dal/day-summary.api.service";
import {UserService} from "../../../../../../../core/user/user.service";
import {errorRxResource} from "../../../../../../../core/error-handling/errorRxResource";

@Injectable({
  providedIn: 'root'
})
export class DaySummaryService {

  #userS = inject(UserService);
  #daySummaryApiS = inject(DaySummaryApiService);

  #daySummary = errorRxResource({
    params: () => this.#userS.userInfo(),
    stream: () => this.#getDaySummary$()
  });

  daySummary = this.#daySummary.asReadonly();

  #getDaySummary$() {
    return this.#daySummaryApiS.getDaySummary$();
  }
}
