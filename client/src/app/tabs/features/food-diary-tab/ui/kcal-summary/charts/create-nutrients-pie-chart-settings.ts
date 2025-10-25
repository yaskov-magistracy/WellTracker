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
      createNutrientTitle('Жиры', fatColor, '16.66%'),
      createNutrientTitle('Белки', proteinColor, '49.5%'),
      createNutrientTitle('Углеводы', carbColor, '83.33%'),
    ],
    series: [
      createNutrientPieSeries(fatInfo, fatColor, ['16.66%', '60%']),
      createNutrientPieSeries(proteinInfo, proteinColor, ['50%', '60%%']),
      createNutrientPieSeries(carbInfo, carbColor, ['83.33%', '60%%'])
    ]
  };
}

const createNutrientTitle = (nutrientName: string, nutrientColor: string, leftPadding: string) => {
  return {
    text: nutrientName,
    left: leftPadding,
    top: 0,
    textAlign: 'center',
    textStyle: {
      color: nutrientColor
    }
  }
}

const createNutrientPieSeries = (nutrientInfo: ConsumedEnergyNutrientInfo, nutrientColor: string, center: [string, string]) => {
  return {
    type: 'pie',
    radius: ['70%', '50%'],
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
    formatter: [
      `{a|${nutrientInfo.consumed}}`,
      `{b|/${nutrientInfo.required}гр.}`
    ].join('\n'),
    rich: {
      a: {
        fontWeight: 'bolder'
      },
      b: {
        color: '#aaa',
        fontSize: 10
      },
    }
  }
}
