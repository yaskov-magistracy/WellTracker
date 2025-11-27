
export function getAllDatesFromRecords(records: Array<{ from: string, to: string }>, formatted = true): string[] {
  const allDates: string[] = [];

  records.forEach(record => {
    const fromDate = new Date(record.from);
    const toDate = new Date(record.to);
    const currentDate = new Date(fromDate);

    while (currentDate <= toDate) {
      if (formatted) {
        const day = currentDate.getDate().toString().padStart(2, '0');
        const month = (currentDate.getMonth() + 1).toString().padStart(2, '0');
        allDates.push(`${day}.${month}`);
      } else {
        allDates.push(currentDate.toISOString().split('T')[0]); // "2025-11-10"
      }
      currentDate.setDate(currentDate.getDate() + 1);
    }
  });

  return allDates;
}
