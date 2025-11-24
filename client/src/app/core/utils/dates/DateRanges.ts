import {formatDate} from "./format-date";
import {DateRangeEnum} from "../../enums/DateRange";
import {DateRange} from "../../types/DateRange";

export function getDateRangeByEnumValue(dateRangeEnumValue: DateRangeEnum): DateRange {

  const today = new Date();
  const fromDate = formatDate(today);
  let toDate: string;

  if (dateRangeEnumValue === DateRangeEnum.Week) {
    toDate = formatDate(new Date(new Date().setDate(today.getDate() - 7)));
  } else if (dateRangeEnumValue === DateRangeEnum.Month) {
    toDate = formatDate(new Date(new Date().setDate(today.getDate() - 30)));
  } else {
    toDate = formatDate(new Date(new Date().setDate(today.getDate() - 90)));
  }
  return [fromDate, toDate];
}
