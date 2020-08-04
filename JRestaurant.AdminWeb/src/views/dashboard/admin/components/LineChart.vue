<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/macarons') // echarts theme
import resize from './mixins/resize'
import { listFTByWeek } from '@/api/foodsturnover'
import { listDTByWeek } from '@/api/drinksturnover'
export default {
  mixins: [resize],
  props: {
    className: {
      type: String,
      default: 'chart'
    },
    width: {
      type: String,
      default: '100%'
    },
    height: {
      type: String,
      default: '350px'
    },
    autoResize: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      chart: null,
      week: 1
    }
  },
  mounted() {
    var chartdata = {
      FtData: [100, 120, 161, 134, 105, 160, 165],
      DtData: [120, 82, 91, 154, 162, 140, 145]
    }
    listFTByWeek(this.week).then(response => {
      console.log(response)
    }).catch(error => {
      console.log(error)
    })
    listDTByWeek(this.week).then(response => {
      console.log(response)
    }).catch(error => {
      console.log(error)
    })
    this.$nextTick(() => {
      this.initChart(chartdata)
    })
  },
  beforeDestroy() {
    if (!this.chart) {
      return
    }
    this.chart.dispose()
    this.chart = null
  },
  methods: {
    initChart(chartData) {
      this.chart = echarts.init(this.$el, 'macarons')
      this.setOptions(chartData)
    },
    setOptions({ FtData, DtData } = {}) {
      this.chart.setOption({
        xAxis: {
          data: ['星期一', '星期二', '星期三', '星期四', '星期五', '星期六', '星期天'],
          boundaryGap: false,
          axisTick: {
            show: false
          }
        },
        grid: {
          left: 10,
          right: 10,
          bottom: 20,
          top: 30,
          containLabel: true
        },
        tooltip: {
          trigger: 'axis',
          axisPointer: {
            type: 'cross'
          },
          padding: [5, 10]
        },
        yAxis: {
          axisTick: {
            show: false
          }
        },
        legend: {
          data: ['料理营业额', '酒水营业额']
        },
        series: [{
          name: '料理营业额', itemStyle: {
            normal: {
              color: '#FF005A',
              lineStyle: {
                color: '#FF005A',
                width: 2
              }
            }
          },
          smooth: true,
          type: 'line',
          data: FtData,
          animationDuration: 2800,
          animationEasing: 'cubicInOut'
        },
        {
          name: '酒水营业额',
          smooth: true,
          type: 'line',
          itemStyle: {
            normal: {
              color: '#3888fa',
              lineStyle: {
                color: '#3888fa',
                width: 2
              },
              areaStyle: {
                color: '#f3f8ff'
              }
            }
          },
          data: DtData,
          animationDuration: 2800,
          animationEasing: 'quadraticOut'
        }]
      })
    }
  }
}
</script>
