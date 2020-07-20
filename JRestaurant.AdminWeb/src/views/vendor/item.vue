<template>
  <el-card shadow="hover" style="height:150px;text-align:left;">
    <div slot="header" class="clearfix">
      <el-tag>{{ name }}</el-tag>
      <el-button style="float: right;" type="danger" size="mini" @click="deleteVendor">删除供应商</el-button>
    </div>
    <el-row>
      <el-col :span="6">
        <svg-icon icon-class="vendor" class="modelImage" />
        <el-button type="text" @click="updateVendor">编辑</el-button>
      </el-col>
      <el-col :span="18" style="font-size:12px;padding-left:5px;color:#606266">
        <el-row style="margin:5px;2px">供货类型: {{ aimtype }}</el-row>
        <el-row style="margin:5px;2px">备注: {{ comments }}</el-row>
        <el-row style="margin:5px;2px">添加时间: {{ createtime != null? dateformat(createtime) : '' }}</el-row>
      </el-col>
    </el-row>
    <el-dialog
      title="供应商信息管理"
      center
      top="5vh"
      :visible.sync="dialogVisible"
      width="30%"
    >
      <add
        :vendorid="id"
        :aimtype="aimtype"
        :name="name"
        :comments="comments"
      />
    </el-dialog>
  </el-card>
</template>
<script>
import add from './add'
import { deleteVendor } from '@/api/vendor'
import Cookies from 'js-cookie'
export default {
  name: 'Item',
  components: {
    add
  },
  props: {
    id: {
      type: Number,
      default: null
    },
    aimtype: {
      type: String,
      default: null
    },
    name: {
      type: String,
      default: null
    },
    comments: {
      type: String,
      default: null
    },
    createtime: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      dialogVisible: false
    }
  },
  methods: {
    deleteVendor() {
      deleteVendor(this.id, Cookies.get('uid')).then(response => {
        if (response) {
          this.$message.success('成功删除供应商')
          this.$emit('updateData', 0)
        } else {
          this.$message.error('供应商删除失败')
        }
      }).catch(error => {
        console.log(error)
      })
    },
    updateVendor() {
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
