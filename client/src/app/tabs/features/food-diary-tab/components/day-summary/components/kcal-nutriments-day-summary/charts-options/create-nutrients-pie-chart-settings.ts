import {EChartsCoreOption} from "echarts/core";
import {ConsumedEnergyNutrientInfo} from "./types/EnergyNutrientOption";
import {roundNumber} from "../../../../../../../../shared/utils/round-number";

export function createNutrientsPieChartSettings(fatInfo: ConsumedEnergyNutrientInfo,
                                                proteinInfo: ConsumedEnergyNutrientInfo,
                                                carbInfo: ConsumedEnergyNutrientInfo): EChartsCoreOption {
  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const fatColor = documentElementComputedStyle.getPropertyValue('--fat');
  const proteinColor = documentElementComputedStyle.getPropertyValue('--protein');
  const carbColor = documentElementComputedStyle.getPropertyValue('--carb');
  const mainTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-100');
  const secondaryTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-500');

  return {
    title: [
      createNutrientTitle('Жиры', fatColor, '16.66%'),
      createNutrientTitle('Белки', proteinColor, '49.5%'),
      createNutrientTitle('Углеводы', carbColor, '83.33%'),
    ],
    series: [
      createNutrientPieSeries(fatInfo, fatColor, ['16.66%', '60%'], mainTextColor, secondaryTextColor),
      createNutrientPieSeries(proteinInfo, proteinColor, ['50%', '60%%'], mainTextColor, secondaryTextColor),
      createNutrientPieSeries(carbInfo, carbColor, ['83.33%', '60%%'], mainTextColor, secondaryTextColor)
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

const createNutrientPieSeries = (nutrientInfo: ConsumedEnergyNutrientInfo,
                                 nutrientColor: string, center: [string, string],
                                 mainTextColor: string, secondaryTextColor: string
) => {
  return {
    type: 'pie',
    radius: ['70%', '50%'],
    label: createNutrientLabel(nutrientInfo, mainTextColor, secondaryTextColor),
    center,
    data: [
      { name: 'consumed', value: nutrientInfo.consumed, itemStyle: { color: nutrientColor } },
      { name: 'left', value: nutrientInfo.required - nutrientInfo.consumed, itemStyle: { color: '#C4C4C4' } },
    ]
  };
}

const createNutrientLabel = (nutrientInfo: ConsumedEnergyNutrientInfo,
                             mainTextColor: string, secondaryTextColor: string
) => {
  const { consumed, required } = nutrientInfo;
  return {
    fontSize: '16',
    position: 'center',
    formatter: [
      `{a|${roundNumber(consumed)}}`,
      `{b|/${roundNumber(required)}гр.}`
    ].join('\n'),
    rich: {
      a: {
        color: mainTextColor,
        fontWeight: 'bolder'
      },
      b: {
        color: secondaryTextColor,
        fontSize: 10
      },
    }
  }
}
