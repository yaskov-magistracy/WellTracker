import { EChartsCoreOption } from "echarts/core";

export function createWeightStatisticLineOptions(): EChartsCoreOption {
  const documentElementComputedStyle = window.getComputedStyle(document.documentElement);
  const mainTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-100');
  const secondaryTextColor = documentElementComputedStyle.getPropertyValue('--ion-text-color-step-500');

  const now = new Date();
  const daysCount = 30;
  let currentWeight = 62.8;
  const weightData = [];

  for (let i = daysCount - 1; i >= 0; i--) {
    const date = new Date(now);
    date.setDate(now.getDate() - i);
    const formattedDate = date.toLocaleDateString('ru-RU', {
      day: '2-digit',
      month: '2-digit',
    });

    const delta = (Math.random() - 0.5) * 0.4; // колебания ±0.2 кг
    currentWeight = Math.max(61.5, Math.min(66, currentWeight + delta));
    weightData.push({date: formattedDate, value: +currentWeight.toFixed(1)});
  }
  const dates = weightData.map(d => d.date);
  const values = weightData.map(d => d.value);

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
      min: (Math.min(...values.filter(v => v != null)) ?? 60) - 1,
      max: (Math.max(...values.filter(v => v != null)) ?? 70) + 1,
      interval: 0.5,
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
