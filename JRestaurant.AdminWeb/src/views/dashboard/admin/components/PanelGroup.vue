<template>
  <el-row :gutter="40" class="panel-group">
    <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-people">
          <svg-icon icon-class="peoples" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            用户总数
          </div>
          <count-to :start-val="0" :end-val="totalAdmin" :duration="2600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
    <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-money">
          <svg-icon icon-class="money" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            当日营业额
          </div>
          <count-to :start-val="0" :end-val="totalTurnOver" :duration="2600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
    <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-shopping">
          <svg-icon icon-class="shopping" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            当日采购额
          </div>
          <count-to :start-val="0" :end-val="totalPO" :duration="2600" class="card-panel-num" />
        </div>
      </div>
    </el-col>
    <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
      <div class="card-panel">
        <div class="card-panel-icon-wrapper icon-message">
          <svg-icon icon-class="vendor" class-name="card-panel-icon" />
        </div>
        <div class="card-panel-description">
          <div class="card-panel-text">
            供应商总数
          </div>
          <count-to :start-val="0" :end-val="totalVendor" :duration="3000" class="card-panel-num" />
        </div>
      </div>
    </el-col>
  </el-row>
</template>

<script>
import CountTo from 'vue-count-to'
import { getAdminTotalCount } from '@/api/manage'
import { getVendorTotalCount } from '@/api/vendor'
import { listFTToday } from '@/api/foodsturnover'
import { listDTToday } from '@/api/drinksturnover'
import { listFoodsPOToday } from '@/api/foodspo'
import { listDrinksPOToday } from '@/api/drinkspo'
import { listFTByWeek } from '@/api/foodsturnover'
import { listDTByWeek } from '@/api/drinksturnover'
import { listFoodsPOByWeek } from '@/api/foodspo'
import { listDrinksPOByWeek } from '@/api/drinkspo'

export default {
  components: {
    CountTo
  },
  data() {
    return {
      totalAdmin: 0,
      totalVendor: 0,
      todayPOAmount: 0,
      totalTunerOverAmount: 0,
      ftToday: [],
      dtToday: [],
      fpoToday: [],
      dpoToday: []
    }
  },
  computed: {
    totalPO() {
      var totalFPO = 0
      this.fpoToday.forEach(item => {
        totalFPO = totalFPO + parseFloat(item.Amount)
      })
      var totalDPO = 0
      this.dpoToday.forEach(item => {
        totalDPO = totalDPO + parseFloat(item.Amount)
      })
      return totalFPO + totalDPO
    },
    totalTurnOver() {
      var totalFT = 0
      this.ftToday.forEach(item => {
        if (!item.Discount) {
          totalFT = totalFT + parseFloat(item.Amount)
        }
      })
      var totalDT = 0
      this.dtToday.forEach(item => {
        if (!item.Discount) {
          totalDT = totalDT + parseFloat(item.Amount)
        }
      })
      return totalFT + totalDT
    },
    POList() {
      var list = [0, 0, 0, 0, 0, 0, 0]
      this.fPOList.forEach(item => {
        var index = parseInt(item.AddDate)
        list[index] += item.Amount
      })
      this.dPOList.forEach(item => {
        var index = parseInt(item.AddDate)
        list[index] += item.Amount
      })
      return list
    },
    TOList() {
      var list = [0, 0, 0, 0, 0, 0, 0]
      this.ftList.forEach(item => {
        if (!item.Discount) {
          var index = parseInt(item.AddDate)
          list[index] += item.Amount
        }
      })
      this.dtList.forEach(item => {
        var index = parseInt(item.AddDate)
        list[index] += item.Amount
      })
      return list
    }
  },
  mounted() {
    getAdminTotalCount().then(response => {
      this.totalAdmin = response
    }).catch(error => {
      console.log(error)
    })
    getVendorTotalCount().then(response => {
      this.totalVendor = response
    }).catch(error => {
      console.log(error)
    })
    listFTToday().then(response => {
      this.ftToday = response
    }).catch(error => {
      console.log(error)
    })
    listDTToday().then(response => {
      this.dtToday = response
    }).catch(error => {
      console.log(error)
    })
    listFoodsPOToday().then(response => {
      this.fpoToday = response
    }).catch(error => {
      console.log(error)
    })
    listDrinksPOToday().then(response => {
      this.dpoToday = response
    }).catch(error => {
      console.log(error)
    })
    listFTByWeek(1).then(response => {
      this.ftList = response
    }).catch(error => {
      console.log(error)
    })
    listDTByWeek(1).then(response => {
      this.dtList = response
    }).catch(error => {
      console.log(error)
    })
    listFoodsPOByWeek(1).then(response => {
      this.fPOList = response
    }).catch(error => {
      console.log(error)
    })
    listDrinksPOByWeek(1).then(response => {
      this.dPOList = response
    }).catch(error => {
      console.log(error)
    })
  },
  methods: {
    handleSetLineChartData(type) {
      this.$emit('handleSetLineChartData', type)
    },
    convertWeekDay(weekday) {
      var day = ''
      switch (weekday) {
        case 1:
          day = '星期一'
          break
        case 2:
          day = '星期二'
          break
        case 3:
          day = '星期三'
          break
        case 4:
          day = '星期四'
          break
        case 5:
          day = '星期五'
          break
        case 6:
          day = '星期六'
          break
        case 0:
          day = '星期天'
          break
        default:
          day = '星期一'
          break
      }
      return day
    }
  }
}
</script>

<style lang="scss" scoped>
.panel-group {
  margin-top: 18px;

  .card-panel-col {
    margin-bottom: 32px;
  }

  .card-panel {
    height: 108px;
    cursor: pointer;
    font-size: 12px;
    position: relative;
    overflow: hidden;
    color: #666;
    background: #fff;
    box-shadow: 4px 4px 40px rgba(0, 0, 0, .05);
    border-color: rgba(0, 0, 0, .05);

    &:hover {
      .card-panel-icon-wrapper {
        color: #fff;
      }

      .icon-people {
        background: #40c9c6;
      }

      .icon-message {
        background: #36a3f7;
      }

      .icon-money {
        background: #f4516c;
      }

      .icon-shopping {
        background: #34bfa3
      }
    }

    .icon-people {
      color: #40c9c6;
    }

    .icon-message {
      color: #36a3f7;
    }

    .icon-money {
      color: #f4516c;
    }

    .icon-shopping {
      color: #34bfa3
    }

    .card-panel-icon-wrapper {
      float: left;
      margin: 14px 0 0 14px;
      padding: 16px;
      transition: all 0.38s ease-out;
      border-radius: 6px;
    }

    .card-panel-icon {
      float: left;
      font-size: 48px;
    }

    .card-panel-description {
      float: right;
      font-weight: bold;
      margin: 26px;
      margin-left: 0px;

      .card-panel-text {
        line-height: 18px;
        color: rgba(0, 0, 0, 0.45);
        font-size: 16px;
        margin-bottom: 12px;
      }

      .card-panel-num {
        font-size: 20px;
      }
    }
  }
}

@media (max-width:550px) {
  .card-panel-description {
    display: none;
  }

  .card-panel-icon-wrapper {
    float: none !important;
    width: 100%;
    height: 100%;
    margin: 0 !important;

    .svg-icon {
      display: block;
      margin: 14px auto !important;
      float: none !important;
    }
  }
}
</style>
