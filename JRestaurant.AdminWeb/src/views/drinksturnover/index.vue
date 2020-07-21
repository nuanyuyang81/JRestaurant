<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" type="primary" size="mini" icon="el-icon-plus" @click="addFT">
        添加酒水营业额记录
      </el-button>
    </div>
    <el-table
      :data="dtList"
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
        prop="Amount"
        label="营收金额"
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
        prop="Discount"
        label="折扣"
      >
        <template slot-scope="{row}">
          <template v-if="row.editable">
            <el-input-number v-model="row.Discount" controls-position="right" :min="0.01" :max="1" :precision="2" :step="0.01" />
            <el-button v-if="row.editable" type="warning" style="margin-left:8px" @click="cancelEdit(row)">取消</el-button>
          </template>
          <span v-else>{{ row.Discount }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="折后营收"
        width="100"
      >
        <template slot-scope="{row}">
          <span>{{ row.Amount * row.Discount }}</span>
        </template>
      </el-table-column>
      <el-table-column
        prop="TypeName"
        label="营收类型"
        width="120"
      >
        <template slot-scope="{row}">
          <el-tag size="medium">{{ row.TypeName }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        prop="UserName"
        label="添加人"
      />
      <el-table-column
        prop="Comments"
        label="备注"
      />
      <el-table-column
        prop="CreateTime"
        label="添加时间"
        width="150"
      >
        <template slot-scope="{row}">
          <span>{{ timeformat(row.CreateTime) }}</span>
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
            @onConfirm="deleteDT(row.Id)"
          >
            <el-button slot="reference" type="danger" icon="el-icon-delete" circle style="margin-left:10px" />
          </el-popconfirm>
        </template>
      </el-table-column>
    </el-table>
    <el-drawer
      size="90%"
      :visible.sync="dtDialogVisible"
      :direction="direction"
      :destroy-on-close="true"
    >
      <template slot="title">
        <span style="text-align:center; font-weight:bold;font-size:20px">酒水营业额管理</span>
      </template>
      <add @updateData="refreshData(1)" />
    </el-drawer>
  </div>
</template>
<script>
// import add from './add'
import add from './add'
import { listDTByWeek, deleteDT, updateDT } from '@/api/drinksturnover'
import Cookies from 'js-cookie'
export default {
  name: 'DrinksTurnOver',
  components: {
    add
  },
  data() {
    return {
      dtDialogVisible: false,
      loading: false,
      direction: 'ttb',
      dtList: [],
      targetId: null,
      editable: false
    }
  },
  mounted() {
    this.refreshData(1)
  },
  methods: {
    refreshData(week) {
      this.dtList = []
      this.loading = true
      listDTByWeek(week).then(response => {
        response.forEach(item => {
          item.editable = false
          this.dtList.push(item)
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
      updateDT({ Id: row.Id, Amount: row.Amount, Discount: row.Discount, TypeId: row.TypeId, AddDate: row.AddDate, OwnerId: Cookies.get('uid'), Comments: row.Comments }).then(response => {
        if (response) {
          this.$message.success('料理营业额记录更新成功')
          this.refreshData(1)
        } else {
          this.$message.error('料理营业额记录更新失败')
        }
      }).catch(error => {
        console.log(error)
      })
    },
    cancelEdit(row) {
      row.editable = false
    },
    deleteDT(id) {
      deleteDT(id).then(response => {
        if (response) {
          this.$message.success('酒水营业额记录删除成功')
          this.refreshData(1)
        } else {
          this.$message.error('删除失败')
        }
      }).catch(error => {
        console.log(error)
      })
    },
    addFT() {
      this.targetId = null
      this.dtDialogVisible = true
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
    },
    dateformat(time) {
      var dtstr = time
      if (dtstr != null) {
        dtstr = dtstr.replace(/\D/g, ' ')
        var dtcomps = dtstr.split(' ')
        dtcomps[1]--
        var convdt = new Date(Date.UTC(dtcomps[0], dtcomps[1], dtcomps[2]))
        return convdt.toLocaleDateString()
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
