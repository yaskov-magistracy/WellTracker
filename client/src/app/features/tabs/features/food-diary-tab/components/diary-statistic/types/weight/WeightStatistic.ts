import {WeightStatisticRecord} from "./WeightStatisticRecord";

export type WeightStatistic = {
  currentWeight: number;
  targetWeight: number;
  statistics: {
    userId: string;
    records: WeightStatisticRecord[]
  }
}
