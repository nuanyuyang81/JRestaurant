<template>
  <el-card>
    <el-form label-width="120px" :rules="rules" label-position="left" :model="targetRole" size="mini">
      <el-form-item label="角色Key" prop="Key">
        <el-select v-model="targetRole.Key" clearable>
          <el-option v-for="item in keyList" :key="item.Id" :label="item.Name" :value="item.Name" />
        </el-select>
      </el-form-item>
      <el-form-item label="角色名" prop="Name">
        <el-input v-model="targetRole.Name" />
      </el-form-item>
      <el-form-item label="备注" prop="Comments">
        <el-input
          v-model="targetRole.Comments"
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
import { addRole, updateRole } from '@/api/role'
import Cookies from 'js-cookie'
export default {
  name: 'Add',
  props: {
    keyname: {
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
    roleid: {
      type: Number,
      default: null
    }
  },
  data() {
    return {
      keyList: [
        { Id: 0, Name: 'admin' },
        { Id: 1, Name: 'manager' },
        { Id: 2, Name: 'staff' }
      ],
      targetRole: {
        Key: '',
        Name: '',
        Comments: '',
        Id: 0
      },
      rules: {
        Key: [
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
    if (this.roleid !== null) {
      this.targetRole.Key = this.keyname
      this.targetRole.Name = this.name
      this.targetRole.Comments = this.comments
      this.targetRole.Id = this.roleid
    }
  },
  methods: {
    clear() {
      this.targetRole = {
        Id: '',
        Key: '',
        Name: '',
        Comments: ''
      }
    },
    submit() {
      if (this.roleid !== null) {
        updateRole({ Key: this.targetRole.Key, Name: this.targetRole.Name.trim(), Comments: this.targetRole.Comments.trim(), OwnerId: Cookies.get('uid'), Id: this.targetRole.Id }).then(response => {
          if (response) {
            this.$message.success('权限角色信息更新成功')
            this.clear()
          } else {
            this.$message.error('权限角色信息更新失败')
          }
        }).catch(error => {
          console.log(error)
        })
      } else {
        addRole({ Key: this.targetRole.Key, Name: this.targetRole.Name.trim(), Comments: this.targetRole.Comments.trim(), OwnerId: Cookies.get('uid') }).then(response => {
          if (response) {
            this.$message.success('权限角色添加成功')
            this.clear()
            this.$emit('updateData')
          } else {
            this.$message.error('权限角色添加失败')
          }
        }).catch(error => {
          console.log(error)
        })
      }
    }
  }
}
</script>
