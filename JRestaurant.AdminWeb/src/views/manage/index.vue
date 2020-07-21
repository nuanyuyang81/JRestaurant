<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" type="primary" size="mini" icon="el-icon-plus" @click="UserDialogVisible = true">
        添加新用户
      </el-button>
    </div>
    <el-row v-loading="loadAdmin" style="width:95%">
      <el-col v-for="item in adminList" :key="item.Id" style="margin-top:10px;margin-left:10px;width:320px">
        <item
          :id="item.Id"
          :name="item.UserName"
          :roleid="item.RoleId"
          :role="item.RoleName"
          :phone="item.PhoneNumber"
          :createtime="item.CreateTime"
          :lastlogintime="item.LastLoginTime"
          :rolekey="item.RoleKey"
          @updateData="refreshData"
        />
      </el-col>
    </el-row>
    <el-dialog
      title="用户信息管理"
      center
      top="5vh"
      :visible.sync="UserDialogVisible"
      width="30%"
    >
      <add @updateData="refreshData" />
    </el-dialog>
  </div>
</template>
<script>
import { listAllAdmin } from '@/api/manage'
import item from './item'
import add from './add'
export default {
  name: 'Manage',
  components: {
    item, add
  },
  data() {
    return {
      adminList: [],
      loadAdmin: false,
      UserDialogVisible: false
    }
  },
  mounted() {
    this.refreshData()
  },
  methods: {
    refreshData() {
      this.loadAdmin = true
      listAllAdmin().then(response => {
        this.adminList = response
        this.loadAdmin = false
      }).catch(error => {
        console.log(error)
      })
    }
  }
}
</script>
