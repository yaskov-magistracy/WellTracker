import { EChartsCoreOption } from "echarts/core";
import {FoodStatistic} from "../../../types/food/FoodStatistic";

export function createNutrientsStatisticPieChartOptions(foodStatistic: FoodStatistic): EChartsCoreOption {
  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const fatColor = documentElementComputedStyle.getPropertyValue('--fat');
  const proteinColor = documentElementComputedStyle.getPropertyValue('--protein');
  const carbColor = documentElementComputedStyle.getPropertyValue('--carb');
  const mainTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-100');
  const secondaryTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-500');

  const protein = { value: foodStatistic.totalNutriments.protein, goal: foodStatistic.targetNutriments.protein };
  const fats = { value: foodStatistic.totalNutriments.fat, goal: foodStatistic.targetNutriments.fat };
  const carbs = { value: foodStatistic.totalNutriments.carbohydrates, goal: foodStatistic.targetNutriments.carbohydrates };

  return {
    title: {
      text: 'БЖУ',
      left: 'left',
      top: 0,
      textStyle: {
        fontSize: 22,
        color: mainTextColor,
      }
    },
    legend: {
      top: '15%',
      left: 'center',
      textStyle: {
        color: mainTextColor
      }
    },
    series: [
      // ======== ВНЕШНИЙ ПОЛУКРУГ (ГОТОВОЕ, ЦЕЛЬ) ========
      {
        type: "pie",
        radius: ['100%', '60%'],
        center: ['50%', '90%'],
        startAngle: 180,
        endAngle: 360,
        label: {
          show: true,
          position: "inside",
          formatter: "{c}гр.",
          fontSize: 16,
          fontWeight: 600,
          color: "#fff",
          rotate: 'tangential'
        },
        data: [
          { value: protein.goal, name: 'Белки', itemStyle: { color: proteinColor } },
          { value: fats.goal, name: 'Жиры', itemStyle: { color: fatColor } },
          { value: carbs.goal, name: 'Углеводы', itemStyle: { color: carbColor } },
        ]
      },
      // ======== ВНУТРЕННИЙ ПОЛУКРУГ (ФАКТ) ========
      {
        type: "pie",
        radius: ["58%", "30%"],
        center: ['50%', '90%'],
        startAngle: 180,
        endAngle: 360,
        label: {
          show: true,
          position: "inside",
          formatter: "{c}гр.",
          fontSize: 16,
          fontWeight: 600,
          color: "#fff",
          rotate: 'tangential'
        },
        data: [
          { value: protein.value, name: 'Белки', itemStyle: { color: proteinColor } },
          { value: fats.value, name: 'Жиры', itemStyle: { color: fatColor } },
          { value: carbs.value, name: 'Углеводы', itemStyle: { color: carbColor } }
        ]
      }
    ],
    // graphic: [
    //   {
    //     type: 'text',
    //     left: '45%',
    //     top: '50%',
    //     style: {
    //       text: 'Тsdsdaест',
    //       fill: mainTextColor,
    //       fontSize: 20,
    //       fontWeight: 'bold',
    //       textAlign: 'center',
    //       textVerticalAlign: 'middle'
    //     }
    //   }
    // ],
  };
}
