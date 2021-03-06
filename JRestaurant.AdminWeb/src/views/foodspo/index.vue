<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" type="primary" size="mini" icon="el-icon-plus" @click="addFoodsPO">
        添加料理进货记录
      </el-button>
      <el-button-group style="float:right;margin-right:20px;">
        <el-button type="primary" icon="el-icon-arrow-left" :disabled="week===1" @click="goToPWeek">上一周</el-button>
        <el-button type="primary" @click="goToNWeek">下一周<i class="el-icon-arrow-right el-icon--right" /></el-button>
      </el-button-group>
    </div>
    <el-table
      :data="foodsPOList"
      style="width: 100%"
    >
      <el-table-column
        prop="WeekDay"
        label="星期"
        width="80"
      >
        <template slot-scope="{row}">
          <el-tag size="medium">{{ convertWeekDay(row.WeekDay) }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        prop="AddDate"
        label="进货日期"
        width="100"
      >
        <template slot-scope="{row}">
          <span>{{ dateformat(row.AddDate) }}</span>
        </template>
      </el-table-column>
      <el-table-column
        prop="VendorName"
        label="供应商"
      />
      <el-table-column
        prop="Amount"
        label="进货金额￥"
      >
        <template slot-scope="{row}">
          <template v-if="row.editable">
            <el-input-number v-model="row.Amount" :precision="2" :step="0.1" style="width:160px" />
            <el-button v-if="row.editable" type="warning" style="margin-left:8px" @click="cancelEdit(row)">取消</el-button>
          </template>
          <span v-else>{{ row.Amount }}</span>
        </template>
      </el-table-column>
      <el-table-column
        prop="UserName"
        label="添加人"
      />
      <el-table-column
        prop="Comments"
        label="备注"
      >
        <template slot-scope="{row}">
          <template v-if="row.editable">
            <el-input v-model="row.Comments" style="width:160px" />
            <el-button v-if="row.editable" type="warning" style="margin-left:8px" @click="cancelEdit(row)">取消</el-button>
          </template>
          <span v-else>{{ row.Comments }}</span>
        </template>
      </el-table-column>
      <el-table-column
        prop="AddDate"
        label="添加日期"
        width="150"
      >
        <template slot-scope="{row}">
          <span>{{ timeformat(row.AddDate) }}</span>
        </template>
      </el-table-column>
      <el-table-column
        width="100"
      >
        <template slot-scope="{row}">
          <el-button v-if="row.editable" type="success" icon="el-icon-check" circle @click="confirmEdit(row)" />
          <el-button v-else type="primary" icon="el-icon-edit" circle @click="edit(row)" />
          <el-popconfirm
            title="确认要删除这条进货记录吗？"
            @onConfirm="deleteFoodsPO(row.Id)"
          >
            <el-button slot="reference" type="danger" icon="el-icon-delete" circle style="margin-left:10px" />
          </el-popconfirm>
        </template>
      </el-table-column>
    </el-table>
    <el-drawer
      size="90%"
      :visible.sync="foodsPODialogVisible"
      :direction="direction"
      :destroy-on-close="true"
    >
      <template slot="title">
        <span style="text-align:center; font-weight:bold;font-size:20px">料理食材进货管理</span>
      </template>
      <addbatch @updateData="refreshData(1)" />
    </el-drawer>
  </div>
</template>
<script>
// import add from './add'
import addbatch from './addbatch'
import { listFoodsPOByWeek, deleteFoodsPO, updateFoodsPO } from '@/api/foodspo'
import Cookies from 'js-cookie'
export default {
  name: 'FoodsPO',
  components: {
    addbatch
  },
  data() {
    return {
      foodsPODialogVisible: false,
      loading: false,
      direction: 'ttb',
      foodsPOList: [],
      targetId: null,
      editable: false,
      week: 1
    }
  },
  mounted() {
    this.refreshData(1)
  },
  methods: {
    refreshData(week) {
      this.foodsPOList = []
      this.loading = true
      listFoodsPOByWeek(week).then(response => {
        response.forEach(item => {
          item.editable = false
          this.foodsPOList.push(item)
        })
        this.loading = false
      }).catch(error => {
        console.log(error)
      })
    },
    edit(row) {
      row.editable = true
    },
    confirmEdit(row) {
      updateFoodsPO({ VendorId: row.VendorId, Amount: row.Amount, AddDate: row.AddDate, Comments: row.Comments, OwnerId: Cookies.get('uid'), Id: row.Id }).then(response => {
        if (response) {
          this.$message.success('料理进货记录更新成功')
          this.refreshData(1)
        } else {
          this.$message.error('料理进货记录更新失败')
        }
      }).catch(error => {
        console.log(error)
      })
    },
    cancelEdit(row) {
      row.editable = false
    },
    deleteFoodsPO(id) {
      deleteFoodsPO(id).then(response => {
        if (response) {
          this.$message.success('料理进货记录删除成功')
          this.refreshData(1)
        } else {
          this.$message.error('删除失败')
        }
      }).catch(error => {
        console.log(error)
      })
    },
    addFoodsPO() {
      this.targetId = null
      this.foodsPODialogVisible = true
    },
    goToPWeek() {
      this.week--
      if (this.week > 0) {
        this.refreshData(this.week)
      }
    },
    goToNWeek() {
      this.week++
      if (this.week > 1) {
        this.refreshData(this.week)
      }
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
        case 7:
          day = '星期天'
          break
        default:
          day = '星期一'
          break
      }
      return day
    },
    dateformat(time) {
      var dtstr = time
      if (dtstr != null) {
        dtstr = dtstr.replace(/\D/g, ' ')
        var dtcomps = dtstr.split(' ')
        dtcomps[1]--
        var convdt = new Date(Date.UTC(dtcomps[0], dtcomps[1], dtcomps[2]))
        var date = convdt.toLocaleString()
        return date.substring(0, date.indexOf(','))
      }
      return ''
    },
    timeformat(time) {
      var dtstr = time
      if (dtstr != null) {
        dtstr = dtstr.replace(/\D/g, ' ')
        var dtcomps = dtstr.split(' ')
        dtcomps[1]--
        var convdt = new Date(Date.UTC(dtcomps[0], dtcomps[1], dtcomps[2], dtcomps[3], dtcomps[4], dtcomps[5]))
        return convdt.toLocaleString()
      }
      return ''
    }
  }
}
</script>
