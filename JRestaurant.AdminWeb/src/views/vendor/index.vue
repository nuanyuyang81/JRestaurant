<template>
  <div class="app-container">
    <div class="filter-container">
      <el-radio-group v-model="AimType" @change="switchAimType">
        <el-radio-button v-for="(item, index) in aimList" :key="index" :label="item" />
      </el-radio-group>
      <el-button class="filter-item" type="primary" size="small" icon="el-icon-plus" style="margin-left:50px;margin-top:6px" @click="vendorDialogVisible = true">
        添加供应商
      </el-button>
    </div>
    <el-row v-loading="loadVendor" style="width:95%">
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
    <el-dialog
      title="供应商信息管理"
      center
      top="5vh"
      :visible.sync="vendorDialogVisible"
      width="30%"
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
      aimList: ['料理', '酒水']
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
