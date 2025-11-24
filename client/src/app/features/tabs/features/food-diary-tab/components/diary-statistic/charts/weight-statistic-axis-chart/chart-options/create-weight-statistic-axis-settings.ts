import { EChartsCoreOption } from "echarts/core";
import {WeightStatistic} from "../../../types/weight/WeightStatistic";
import {getAllDatesFromRecords} from "../../../../../../../../../core/utils/dates/get-all-dates-from-record";

export function createWeightStatisticLineOptions(weightStatistic: WeightStatistic): EChartsCoreOption {

  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const mainTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-100');
  const secondaryTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-500');

  const dates = getAllDatesFromRecords(weightStatistic.statistics.records);
  const values = weightStatistic.statistics.records.map(record => {
    const datesInterval = getAllDatesFromRecords([record]);
    return new Array(datesInterval.length).fill(record.weight) as number[];
  }).flat();

  return {
    title: {
      text: 'Вес',
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
      bottom: '15%', // больше места под ползунок
      top: '12%',
      containLabel: true,
    },
    xAxis: {
      type: 'category',
      boundaryGap: false,
      data: dates,
      axisLine: { lineStyle: { color: secondaryTextColor } },
      axisTick: { show: false },
      axisLabel: { color: secondaryTextColor },
    },
    yAxis: {
      type: 'value',
      axisLine: { show: false },
      splitLine: { lineStyle: { color: '#e0e0e0' } },
      axisLabel: { color: secondaryTextColor },
    },
    dataZoom: [
      {
        type: 'inside', // жестами на мобильных
        start: 70,      // показывать последние ~30%
        end: 100,
      },
      {
        type: 'slider', // нижний ползунок
        height: 15,
        bottom: 5,
      },
    ],
    series: [
      {
        name: 'Вес',
        type: 'line',
        smooth: true,
        symbol: 'circle',
        symbolSize: 6,
        itemStyle: { color: '#7A1FA2' },
        lineStyle: {
          color: '#7A1FA2',
          width: 3,
        },
        areaStyle: {
          color: 'rgba(122, 31, 162, 0.15)',
        },
        data: values,
      },
    ],
  };
}
