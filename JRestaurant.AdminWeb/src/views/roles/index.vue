<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" type="primary" size="mini" icon="el-icon-plus" @click="roleDialogVisible = true">
        添加权限角色
      </el-button>
    </div>
    <el-row v-loading="loadRole" style="width:95%">
      <el-col v-for="item in roleList" :key="item.Id" style="margin-top:10px;margin-left:10px;width:320px">
        <item
          :id="item.Id"
          :keyname="item.Key"
          :owner="item.Owner"
          :name="item.Name"
          :comments="item.Comments"
          :createtime="item.CreateTime"
          @updateData="refreshData"
        />
      </el-col>
    </el-row>
    <el-dialog
      title="权限角色管理"
      center
      top="5vh"
      :visible.sync="roleDialogVisible"
      :width="ratio"
    >
      <add @updateData="refreshData" />
    </el-dialog>
  </div>
</template>
<script>
import { listAllRoles } from '@/api/role'
import item from './item'
import add from './add'
export default {
  name: 'Role',
  components: {
    item, add
  },
  data() {
    return {
      roleList: [],
      loadRole: false,
      roleDialogVisible: false
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
      this.loadRole = true
      listAllRoles().then(response => {
        this.roleList = response
        this.loadRole = false
      }).catch(error => {
        console.log(error)
      })
    }
  }
}
</script>
