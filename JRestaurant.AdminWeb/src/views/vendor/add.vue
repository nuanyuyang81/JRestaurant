<template>
  <el-card>
    <el-form label-width="120px" :rules="rules" label-position="left" :model="targetVendor" size="mini">
      <el-form-item label="供应商名" prop="Name">
        <el-input v-model="targetVendor.Name" />
      </el-form-item>
      <el-form-item label="供货分类" prop="Name">
        <el-radio-group v-model="targetVendor.AimType">
          <el-radio-button v-for="(item, index) in aimList" :key="index" :label="item" />
        </el-radio-group>
      </el-form-item>
      <el-form-item label="备注" prop="Comments">
        <el-input
          v-model="targetVendor.Comments"
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
import { addVendor, updateVendor } from '@/api/vendor'
import Cookies from 'js-cookie'
export default {
  name: 'Add',
  props: {
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
    vendorid: {
      type: Number,
      default: null
    }
  },
  data() {
    return {
      targetVendor: {
        Name: '',
        AimType: '',
        Comments: '',
        Id: 0
      },
      aimList: ['料理', '酒水', '通用'],
      rules: {
        AimType: [
          { required: true, trigger: 'blur' }
        ],
        Name: [
          { required: true, trigger: 'blur' }
        ],
        Comments: [
          { required: true, trigger: 'blur' }
        ]
      }
    }
  },
  mounted() {
    if (this.vendorid !== null) {
      this.targetVendor.AimType = this.aimtype
      this.targetVendor.Name = this.name
      this.targetVendor.Comments = this.comments
      this.targetVendor.Id = this.vendorid
    }
  },
  methods: {
    clear() {
      this.targetVendor = {
        AimType: '',
        Name: '',
        Comments: ''
      }
    },
    submit() {
      if (this.vendorid !== null) {
        updateVendor({ Name: this.targetVendor.Name.trim(), AimType: this.formatAimType(this.targetVendor.AimType), Comments: this.targetVendor.Comments.trim(), OwnerId: Cookies.get('uid'), Id: this.targetVendor.Id }).then(response => {
          if (response) {
            this.$message.success('供应商信息更新成功')
            this.clear()
          } else {
            this.$message.error('供应商信息更新失败')
          }
        }).catch(error => {
          console.log(error)
        })
      } else {
        addVendor({ Name: this.targetVendor.Name.trim(), AimType: this.formatAimType(this.targetVendor.AimType), Comments: this.targetVendor.Comments.trim(), OwnerId: Cookies.get('uid') }).then(response => {
          if (response) {
            this.$message.success('供应商添加成功')
            this.$emit('updateData')
            this.clear()
          } else {
            this.$message.error('供应商添加失败')
          }
        }).catch(error => {
          console.log(error)
        })
      }
    },
    formatAimType(aimtype) {
      var aim = 0
      switch (aimtype) {
        case '料理':
          aim = 0
          break
        case '酒水':
          aim = 1
          break
        case '通用':
          aim = 2
          break
        default:
          aim = 2
          break
      }
      return aim
    }
  }
}
</script>
