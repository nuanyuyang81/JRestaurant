<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" type="primary" size="mini" icon="el-icon-plus" @click="typeDialogVisible = true">
        添加营业额分类
      </el-button>
    </div>
    <el-row v-loading="loadType" style="width:95%">
      <el-col v-for="item in typeList" :key="item.Id" style="margin-top:10px;margin-left:10px;width:320px">
        <item
          :id="item.Id"
          :aim="item.AimType"
          :name="item.Name"
          :comments="item.Comments"
          :createtime="item.CreateTime"
          @updateData="refreshData"
        />
      </el-col>
    </el-row>
    <el-dialog
      title="营业额分类管理"
      center
      top="5vh"
      :visible.sync="typeDialogVisible"
      :width="ratio"
    >
      <add @updateData="refreshData" />
    </el-dialog>
  </div>
</template>
<script>
import { listAllTypes } from '@/api/turnovertype'
import item from './item'
import add from './add'
export default {
  name: 'TurnOverType',
  components: {
    item, add
  },
  data() {
    return {
      typeList: [],
      loadType: false,
      typeDialogVisible: false
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
      this.loadTypes = true
      listAllTypes().then(response => {
        this.typeList = response
        this.loadTypes = false
      }).catch(error => {
        console.log(error)
      })
    }
  }
}
</script>
