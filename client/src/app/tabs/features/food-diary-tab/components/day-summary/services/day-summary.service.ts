import {inject, Injectable} from '@angular/core';
import {DaySummaryApiService} from "../dal/day-summary.api.service";

@Injectable({
  providedIn: 'root'
})
export class DaySummaryService {

  private daySummaryApiS = inject(DaySummaryApiService);

  getDaySummary$() {
    return this.daySummaryApiS.getDaySummary$();
  }
}
