import {EChartsCoreOption} from "echarts/core";
import {ConsumedEnergyNutrientInfo} from "../../kcal-summary/charts/types/EnergyNutrientOption";

export function createFoodEnergyProgressBarSettings(kcalInfo: ConsumedEnergyNutrientInfo): EChartsCoreOption {
  const  { required, consumed } = kcalInfo;
  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const secondaryTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-500');
  return {
    xAxis: {
      type: 'value',
      max: required,
      name: `${ (consumed / required * 100).toFixed(0) }%`,
      nameLocation: 'center',
      nameTextStyle: {
        fontSize: 16,
        color: secondaryTextColor
      },
      axisLabel: {
        show: false
      },
      splitLine: {
        show: false
      }
    },
    yAxis: {
      type: 'category',
      show: false
    },
    grid: {
      left: 0,
      right: 0,
      top: 0,
      bottom: 0
    },
    series: [
      {
        type: 'bar',
        data: [consumed],
        barWidth: '100%',
        roundCap: true,
        showBackground: true,
        itemStyle: {
          borderRadius: [50,50,50,50]
        }
      }
    ]
  };
}
