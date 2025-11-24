import { EChartsCoreOption } from "echarts/core";
import {FoodStatistic} from "../../../types/food/FoodStatistic";


export function createKcalStatisticBarChartOptions(foodStatistic: FoodStatistic): EChartsCoreOption {
  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const mainTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-100');
  const secondaryTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-500');

  const now = new Date();
  const daysCount = 30;
  const calorieData = [];
  for (let i = daysCount - 1; i >= 0; i--) {
    const date = new Date(now);
    date.setDate(now.getDate() - i);
    const formattedDate = date.toLocaleDateString('ru-RU', {
      day: '2-digit',
      month: '2-digit',
    });
    // Генерация случайных калорий в диапазоне 1800–2800
    const calories = Math.round(2200 + (Math.random() - 0.5) * 800);
    calorieData.push({ date: formattedDate, value: calories });
  }

  const dates = foodStatistic.records.map(record => record.date);
  const values = foodStatistic.records.map(record => record.energy);

  return {
    title: {
      text: 'Калории',
      left: 'left',
      top: 0,
      textStyle: {
        fontSize: 22,
        color: mainTextColor,
      },
    },
    grid: {
      left: '4%',
      right: '4%',
      bottom: '10%',
      top: '12%',
      containLabel: true,
    },
    xAxis: {
      type: 'category',
      boundaryGap: true,
      data: dates,
      axisTick: { show: false },
      axisLabel: {
        color: secondaryTextColor,
        formatter: (val: string) => val.slice(0, 5),
      },
    },
    yAxis: {
      type: 'value',
      axisLine: { show: false },
      axisLabel: { color: secondaryTextColor },
    },
    dataZoom: [
      {
        type: 'inside', // жестами на мобильных
        start: 70,      // показывать последние ~30%
        end: 100,
      },
      {
        type: 'slider',
        height: 15,
        bottom: 5
      },
    ],
    series: [
      {
        name: 'Ккал',
        type: 'bar',
        data: values,
        itemStyle: {
          borderRadius: [4, 4, 0, 0],
        }
      },
    ],
  };
}
