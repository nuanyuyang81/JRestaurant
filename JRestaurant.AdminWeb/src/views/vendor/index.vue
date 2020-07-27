<template>
  <div class="app-container">
    <div class="filter-container">
      <el-radio-group v-model="AimType" @change="switchAimType">
        <el-radio-button v-for="(item, index) in aimList" :key="index" :label="item" />
      </el-radio-group>
      <el-button class="filter-item" type="primary" icon="el-icon-plus" style="margin-left:50px;margin-top:6px" @click="vendorDialogVisible = true">
        添加供应商
      </el-button>

    </div>
    <el-card>
      <div slot="header" class="clearfix">
        <el-switch
          v-model="showType"
          active-text="卡片显示"
          inactive-text="列表显示"
        />
      </div>
      <el-row v-if="showType" v-loading="loadVendor" style="width:95%">
        <el-col v-for="item in vendorList" :key="item.Id" style="margin-top:10px;margin-left:10px;width:320px">
          <item
            :id="item.Id"
            :aimtype="formatAimType(item.AimType)"
            :name="item.Name"
            :comments="item.Comments"
            :createtime="item.CreateTime"
            @updateData="refreshData"
          />
        </el-col>
      </el-row>
      <el-row v-else>
        <el-table :data="vendorList">
          <el-table-column
            label="NO"
            type="index"
            width="50"
          />
          <el-table-column
            prop="Name"
            label="供应商"
          >
            <template slot-scope="{row}">
              <el-tag size="medium">{{ row.Name }}</el-tag>
            </template>
          </el-table-column>
          <el-table-column
            prop="Comments"
            label="备注"
          >
            <template slot-scope="{row}">
              <span>{{ row.Comments }}</span>
            </template>
          </el-table-column>
          <el-table-column
            prop="CreateTime"
            label="添加日期"
            width="120"
          >
            <template slot-scope="{row}">
              <span>{{ dateformat(row.CreateTime) }}</span>
            </template>
          </el-table-column>
          <el-table-column
            prop="UserName"
            label="添加人"
            width="100"
          >
            <template slot-scope="{row}">
              <span>{{ row.UserName }}</span>
            </template>
          </el-table-column>
        </el-table>
      </el-row>
    </el-card>
    <el-dialog
      title="供应商信息管理"
      center
      top="5vh"
      :visible.sync="vendorDialogVisible"
      :width="ratio"
    >
      <add @updateData="refreshData(0)" />
    </el-dialog>
  </div>
</template>
<script>
import { ListVendorByAim } from '@/api/vendor'
import item from './item'
import add from './add'
export default {
  name: 'Vendor',
  components: {
    item, add
  },
  data() {
    return {
      vendorList: [],
      loadVendor: false,
      vendorDialogVisible: false,
      AimType: '料理',
      showType: true,
      aimList: ['料理', '酒水']
    }
  },
  computed: {
    ratio() {
      var width = window.screen.width
      if (width < 1200) {
        return '90%'
      } else {
        return '30%'
      }
    }
  },
  mounted() {
    this.refreshData()
  },
  methods: {
    refreshData() {
      this.loadVendor = true
      var aim = 0
      if (this.AimType === '酒水') {
        aim = 1
      }
      ListVendorByAim(aim).then(response => {
        this.vendorList = response
        this.loadVendor = false
      }).catch(error => {
        console.log(error)
      })
    },
    switchAimType() {
      this.refreshData()
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
    formatAimType(aimtype) {
      var aim = ''
      switch (aimtype) {
        case 0:
          aim = '料理'
          break
        case 1:
          aim = '酒水'
          break
        case 2:
          aim = '通用'
          break
        default:
          aim = '通用'
          break
      }
      return aim
    }
  }
}
</script>
