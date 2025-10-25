import {EChartsCoreOption} from "echarts/core";
import {ConsumedEnergyNutrientInfo} from "./types/EnergyNutrientOption";

export function createNutrientsPieChartSettings(fatInfo: ConsumedEnergyNutrientInfo,
                                                proteinInfo: ConsumedEnergyNutrientInfo,
                                                carbInfo: ConsumedEnergyNutrientInfo): EChartsCoreOption {
  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const fatColor = documentElementComputedStyle.getPropertyValue('--fat');
  const proteinColor = documentElementComputedStyle.getPropertyValue('--protein');
  const carbColor = documentElementComputedStyle.getPropertyValue('--carb');

  return {
    title: [
      {
        text: 'Жиры',
        left: '46px',
        textAlign: 'center',
        textStyle: {
          color: fatColor
        }
      },
      {
        text: 'Белки',
        left: '146px',
        textAlign: 'center',
        textStyle: {
          color: proteinColor
        }
      },
      {
        text: 'Углеводы',
        left: '246px',
        textAlign: 'center',
        textStyle: {
          color: carbColor
        }
      }
    ],
    series: [
      createNutrientPieSeries(fatInfo, fatColor, ['10%', '50%']),
      createNutrientPieSeries(proteinInfo, proteinColor, ['33%', '50%']),
      createNutrientPieSeries(carbInfo, carbColor, ['56%', '50%'])
    ]
  };
}

const createNutrientPieSeries = (nutrientInfo: ConsumedEnergyNutrientInfo, nutrientColor: string, center: [string, string]) => {
  return {
    type: 'pie',
    radius: ['40%', '70%'],
    label: createNutrientLabel(nutrientInfo),
    center,
    data: [
      { name: 'consumed', value: nutrientInfo.consumed, itemStyle: { color: nutrientColor } },
      { name: 'left', value: nutrientInfo.required - nutrientInfo.consumed, itemStyle: { color: '#C4C4C4' } },
    ]
  };
}

const createNutrientLabel = (nutrientInfo: ConsumedEnergyNutrientInfo) => {
  return {
    fontSize: '16',
    position: 'center',
    formatter: () => {
      return `${nutrientInfo.consumed}\n/${nutrientInfo.required}гр.`; // Use sum variable here
    },
  }
}
