<template>
  <el-card>
    <el-form label-width="120px" :rules="rules" label-position="left" :model="taragetType" size="mini">
      <el-form-item label="营业额分类" prop="Name">
        <el-input v-model="taragetType.Name" />
      </el-form-item>
      <el-form-item label="适用于" prop="AimType">
        <el-select v-model="taragetType.AimType" clearable>
          <el-option v-for="item in aimList" :key="item.Id" :label="item.Name" :value="item.Id" />
        </el-select>
      </el-form-item>
      <el-form-item label="备注" prop="Comments">
        <el-input
          v-model="taragetType.Comments"
          type="textarea"
          :rows="5"
        />
      </el-form-item>
    </el-form>
    <el-row style="text-align: center">
      <el-button type="warning" @click="clear">清除</el-button>
      <el-button type="primary" @click="submit">提交</el-button>
    </el-row>
  </el-card>
</template>
<script>
import { addType, updateType } from '@/api/turnovertype'
import Cookies from 'js-cookie'
export default {
  name: 'Add',
  props: {
    name: {
      type: String,
      default: null
    },
    comments: {
      type: String,
      default: null
    },
    typeid: {
      type: Number,
      default: null
    },
    aimtype: {
      type: Number,
      default: null
    }
  },
  data() {
    return {
      aimList: [
        { Id: 0, Name: '料理' },
        { Id: 1, Name: '酒水' },
        { Id: 2, Name: '通用' }
      ],
      taragetType: {
        Name: '',
        Comments: '',
        Id: 0,
        AimType: 0
      },
      rules: {
        Name: [
          { required: true, trigger: 'blur' }
        ],
        AimType: [
          { required: true, trigger: 'blur' }
        ],
        Comments: [
          { required: true, trigger: 'blur' }
        ]
      }
    }
  },
  mounted() {
    console.log(this.typeid)
    if (this.typeid !== null) {
      this.taragetType.Name = this.name
      this.taragetType.Comments = this.comments
      this.taragetType.Id = this.typeid
      this.taragetType.AimType = this.aimtype
    }
  },
  methods: {
    clear() {
      this.taragetType = {
        Name: '',
        Comments: ''
      }
    },
    submit() {
      if (this.typeid !== null) {
        updateType({ Name: this.taragetType.Name.trim(), AimType: this.taragetType.AimType, Comments: this.taragetType.Comments.trim(), OwnerId: Cookies.get('uid'), Id: this.taragetType.Id }).then(response => {
          if (response) {
            this.$message.success('营业额分类更新成功')
          } else {
            this.$message.error('营业额分类更新失败')
          }
        }).catch(error => {
          console.log(error)
        })
      } else {
        addType({ Name: this.taragetType.Name.trim(), AimType: this.taragetType.AimType, Comments: this.taragetType.Comments.trim(), OwnerId: Cookies.get('uid') }).then(response => {
          if (response) {
            this.$message.success('营业额分类添加成功')
            this.$emit('updateData')
          } else {
            this.$message.error('营业额分类添加失败')
          }
        }).catch(error => {
          console.log(error)
        })
      }
    }
  }
}
</script>
