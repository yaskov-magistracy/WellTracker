
export function formatDate(date: Date | string | number, localeFormat = false) {
  const dateObj = date instanceof Date ? date : new Date(date);
  return localeFormat ?
    dateObj.toLocaleDateString() :
    dateObj.toISOString().split('T')[0];
}
