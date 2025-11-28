import {formatDate} from "./format-date";

export function getAllDatesFromRecords(records: Array<{ from: string, to: string }>, localeFormat = false): string[] {
  const allDates: string[] = [];

  records.forEach(record => {
    const fromDate = new Date(record.from);
    const toDate = new Date(record.to);
    const currentDate = new Date(fromDate);

    while (currentDate <= toDate) {
      allDates.push(formatDate(currentDate, localeFormat));
      currentDate.setDate(currentDate.getDate() + 1);
    }
  });

  return allDates;
}
