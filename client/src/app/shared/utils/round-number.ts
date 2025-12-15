
export function roundNumber(value: number): number;
export function roundNumber(value: number, precision: number): number;
export function roundNumber<T extends boolean>(value: number, precision: number, asNumber: T): T extends false ? string : number;
export function roundNumber(value: number, precision = 1, asNumber = true): string | number  {
  if (asNumber) {
    return Number.isInteger(value) ? value : parseFloat(value.toFixed(precision));
  } else {
    return value.toFixed(precision);
  }
}
