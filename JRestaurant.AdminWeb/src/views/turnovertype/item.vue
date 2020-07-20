<template>
  <el-card shadow="hover" style="height:150px;text-align:left;">
    <div slot="header" class="clearfix">
      <el-tag>{{ name }}</el-tag>
      <el-button style="float: right;" type="danger" size="mini" @click="deleteType">删除营业额分类</el-button>
    </div>
    <el-row>
      <el-col :span="6">
        <svg-icon icon-class="turnovertype" class="modelImage" />
        <el-button type="text" @click="updateType">编辑</el-button>
      </el-col>
      <el-col :span="18" style="font-size:12px;padding-left:5px;color:#606266">
        <el-row style="margin:5px;2px">适用类型: {{ convertAim(aim) }}</el-row>
        <el-row style="margin:5px;2px">备  注: {{ comments }}</el-row>
        <el-row style="margin:5px;2px">添加时间: {{ createtime != null? dateformat(createtime) : '' }}</el-row>
      </el-col>
    </el-row>
    <el-dialog
      title="营业额分类管理"
      center
      top="5vh"
      :visible.sync="dialogVisible"
      width="30%"
    >
      <add
        :typeid="id"
        :aimtype="aim"
        :name="name"
        :comments="comments"
      />
    </el-dialog>
  </el-card>
</template>
<script>
import add from './add'
import { deleteType } from '@/api/turnovertype'
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
    name: {
      type: String,
      default: null
    },
    aim: {
      type: Number,
      default: 0
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
      dialogVisible: false,
      aimList: [
        { Id: 0, Name: '料理' },
        { Id: 1, Name: '酒水' },
        { Id: 3, Name: '通用' }
      ]
    }
  },
  methods: {
    deleteType() {
      deleteType(this.id, Cookies.get('uid')).then(response => {
        if (response) {
          this.$message.success('成功删除营业额分类')
          this.$emit('updateData')
        } else {
          this.$message.error('营业额分类删除失败')
        }
      }).catch(error => {
        console.log(error)
      })
    },
    updateType() {
      this.dialogVisible = true
    },
    convertAim() {
      if (this.aim === 0) {
        return '料理'
      } else if (this.aim === 2) {
        return '酒水'
      } else {
        return '通用'
      }
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
