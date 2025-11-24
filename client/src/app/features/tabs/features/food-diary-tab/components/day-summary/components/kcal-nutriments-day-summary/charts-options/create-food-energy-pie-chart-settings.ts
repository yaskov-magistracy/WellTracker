import {EChartsCoreOption} from "echarts/core";
import {ConsumedEnergyNutrientInfo} from "./types/EnergyNutrientOption";
import {roundNumber} from "../../../../../../../../../shared/utils/round-number";

export function createFoodEnergyPieChartSettings(kcalInfo: ConsumedEnergyNutrientInfo): EChartsCoreOption {
  const { required, consumed } = kcalInfo;
  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const left = Math.max(required - consumed, 0);
  const mainTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-100');
  const secondaryTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-500');
  return {
    title: {
      text: `${roundNumber(left)}`,
      top: 'center',
      left: 'center',
      textVerticalAlign: 'top',
      textStyle: {
        fontSize: 28,
        color: mainTextColor
      },
      subtext: 'осталось',
      subtextStyle: {
        fontSize: 16,
        color: secondaryTextColor
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
