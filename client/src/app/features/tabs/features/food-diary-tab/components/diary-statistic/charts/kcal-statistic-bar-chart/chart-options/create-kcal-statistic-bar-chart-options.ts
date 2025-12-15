import { EChartsCoreOption } from "echarts/core";
import {FoodStatistic} from "../../../types/food/FoodStatistic";
import {formatDate} from "../../../../../../../../../core/utils/dates/format-date";
import {roundNumber} from "../../../../../../../../../shared/utils/round-number";


export function createKcalStatisticBarChartOptions(foodStatistic: FoodStatistic): EChartsCoreOption {
  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const mainTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-100');
  const secondaryTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-500');
  const bgColor = documentElementComputedStyle.getPropertyValue('--ion-background-color');

  const dates = foodStatistic.records.map(record => formatDate(new Date(record.date), true));
  const values = foodStatistic.records.map(record => roundNumber(record.energy.kcal));

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
    tooltip: {
      trigger: 'item', // срабатывает при наведении на точку
      formatter: function (params: any) {
        return `Дата: <b>${params.name}</b><br> Вес: <b>${params.value} ккал.</b>`;
      },
      backgroundColor: bgColor,
      textStyle: {
        color: mainTextColor,
      },
      borderColor: secondaryTextColor,
    },
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
