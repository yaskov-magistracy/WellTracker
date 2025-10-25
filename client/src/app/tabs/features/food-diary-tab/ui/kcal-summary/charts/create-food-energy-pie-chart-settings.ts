import {EChartsCoreOption} from "echarts/core";
import {ConsumedEnergyNutrientInfo} from "./types/EnergyNutrientOption";

export function createFoodEnergyPieChartSettings(kcalInfo: ConsumedEnergyNutrientInfo): EChartsCoreOption {
  const left = kcalInfo.required - kcalInfo.consumed;
  return {
    title: {
      text: `${left}`,
      top: 'center',
      left: 'center',
      textStyle: {
        fontSize: 28,
      },
      subtext: 'осталось',
      subtextStyle: {
        fontSize: 16,
      },
      itemGap: 0
    },
    series: [
      {
        type: 'pie',
        radius: ['100%', '70%'],
        label: {
          show: false
        },
        data: [
          { name: 'consumed', value: kcalInfo.consumed, itemStyle: { color: '#0163aa' } },
          { name: 'left', value: left, itemStyle: { color: '#C4C4C4' } },
        ]
      }
    ]
  };
}
