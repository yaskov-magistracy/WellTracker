import {inject, Injectable} from '@angular/core';
import {DiaryStatisticApiService} from "../dal/diary-statistic.api.service";
import {DateRangeEnum} from "../../../../../../../core/enums/DateRange";
import {getDateRangeByEnumValue} from "../../../../../../../core/utils/dates/DateRanges";

@Injectable({
  providedIn: 'root'
})
export class DiaryStatisticService {

  #diaryStatisticApiS = inject(DiaryStatisticApiService);

  getFoodStatistic$(dateRange: DateRangeEnum) {
    return this.#diaryStatisticApiS.getFoodStatistic$(getDateRangeByEnumValue(dateRange));
  }

  getWeightStatistic$(dateRange: DateRangeEnum) {
    return this.#diaryStatisticApiS.getWeightStatistic$(getDateRangeByEnumValue(dateRange));
  }
}
