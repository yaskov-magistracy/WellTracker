import {formatDate} from "./format-date";
import {DateRangeEnum} from "../../enums/DateRange";
import {DateRange} from "../../types/DateRange";

export function getDateRangeByEnumValue(dateRangeEnumValue: DateRangeEnum): DateRange {

  const today = new Date();
  const toDate = formatDate(today);
  let fromDate: string;

  if (dateRangeEnumValue === DateRangeEnum.Week) {
    fromDate = formatDate(new Date(new Date().setDate(today.getDate() - 7)));
  } else if (dateRangeEnumValue === DateRangeEnum.Month) {
    fromDate = formatDate(new Date(new Date().setDate(today.getDate() - 30)));
  } else {
    fromDate = formatDate(new Date(new Date().setDate(today.getDate() - 90)));
  }
  return [fromDate, toDate];
}
