<template>
  <el-card shadow="hover" style="height:150px;text-align:left;">
    <div slot="header" class="clearfix">
      <el-tag>{{ name }}</el-tag>
      <el-button v-if="delable" style="float: right;" type="danger" size="mini" @click="deleteUser">删除用户</el-button>
    </div>
    <el-row>
      <el-col :span="6">
        <svg-icon icon-class="avatar" class="modelImage" />
        <el-button type="text" @click="updateAdmin">编辑</el-button>
      </el-col>
      <el-col :span="18" style="font-size:12px;padding-left:5px;color:#606266">
        <el-row style="margin:5px;2px">权   限: {{ role }}</el-row>
        <el-row style="margin:5px;2px">电   话: {{ phone }}</el-row>
        <el-row style="margin:5px;2px">注册时间: {{ createtime != null? dateformat(createtime) : '' }}</el-row>
        <el-row style="margin:5px;2px">登录时间: {{ lastlogintime != null? dateformat(lastlogintime) : '' }}</el-row>
      </el-col>
    </el-row>
    <el-dialog
      title="用户信息管理"
      center
      top="5vh"
      :visible.sync="dialogVisible"
      width="30%"
    >
      <add
        :adminid="id"
        :username="name"
        :phone="phone"
        :roleid="roleid"
      />
    </el-dialog>
  </el-card>
</template>
<script>
import Cookies from 'js-cookie'
import add from './add'
export default {
  name: 'Item',
  components: {
    add
  },
  props: {
    id: {
      type: String,
      default: null
    },
    name: {
      type: String,
      default: null
    },
    role: {
      type: String,
      default: null
    },
    roleid: {
      type: Number,
      default: null
    },
    phone: {
      type: String,
      default: null
    },
    createtime: {
      type: String,
      default: null
    },
    lastlogintime: {
      type: String,
      default: null
    },
    rolekey: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      dialogVisible: false
    }
  },
  computed: {
    delable() {
      return (Cookies.get('phone') !== this.phone && this.rolekey !== 'admin')
    }
  },
  methods: {
    deleteUser() {
    },
    updateAdmin() {
      this.dialogVisible = true
    },
    dateformat(time) {
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
<style lang="scss" scoped>
  .modelImage {
    width: 60px;
    height: 60px;
    float: left;
  }
</style>
