<template>
  <el-card>
    <el-form label-width="120px" :rules="rules" label-position="left" :model="targetUser" size="mini">
      <el-form-item label="用户名" prop="UserName">
        <el-input v-model="targetUser.UserName" />
      </el-form-item>
      <el-form-item label="电话" prop="PhoneNumber">
        <el-input v-model="targetUser.PhoneNumber" />
      </el-form-item>
      <el-form-item v-if="adminid==null" label="密码" prop="Password">
        <el-input v-model="targetUser.Password" type="password" />
      </el-form-item>
      <el-form-item label="权限" prop="RoleId">
        <el-select v-model="targetUser.RoleId" clearable>
          <el-option v-for="item in roleList" :key="item.Id" :label="item.Name" :value="item.Id" />
        </el-select>
      </el-form-item>
    </el-form>
    <el-row style="text-align: center">
      <el-button type="warning" @click="clear">清除</el-button>
      <el-button type="primary" @click="submit">提交</el-button>
    </el-row>
  </el-card>
</template>
<script>
import { listAllRoles } from '@/api/role'
import { addAdmin, updateAdmin } from '@/api/manage'
import md5 from 'js-md5'
export default {
  name: 'Add',
  props: {
    adminid: {
      type: String,
      default: null
    },
    username: {
      type: String,
      default: null
    },
    phone: {
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
      targetUser: {
        UserName: '',
        PhoneNumber: '',
        Password: '',
        RoleId: 1
      },
      roleList: [],
      rules: {
        UserName: [
          { required: true, trigger: 'blur' }
        ],
        PhoneNumber: [
          { required: true, trigger: 'blur' }
        ],
        RoleName: [
          { required: true, trigger: 'blur' }
        ]
      }
    }
  },
  mounted() {
    listAllRoles().then(response => {
      this.roleList = response
    }).catch(error => {
      console.log(error)
    })
    console.log(this.adminid)
    if (this.adminid !== null) {
      this.targetUser.UserName = this.username
      this.targetUser.PhoneNumber = this.phone
      this.targetUser.RoleId = this.roleid
    }
  },
  methods: {
    clear() {
      this.targetUser = {
        Name: '',
        PhoneNumber: '',
        RoleName: '',
        RoleId: ''
      }
    },
    submit() {
      if (this.adminid !== null) {
        updateAdmin({ UserName: this.targetUser.UserName.trim(), PhoneNumber: this.targetUser.PhoneNumber.trim(), RoleId: this.targetUser.RoleId, Id: this.adminid }).then(response => {
          if (response) {
            this.$message.success('用户信息更新成功')
            this.$emit('updateData')
          } else {
            this.$message.error('用户信息更新失败')
          }
        }).catch(error => {
          console.log(error)
        })
      } else {
        addAdmin({ UserName: this.targetUser.UserName.trim(), PhoneNumber: this.targetUser.PhoneNumber.trim(), PasswordHash: md5(this.targetUser.Password.trim()), RoleId: this.targetUser.RoleId }).then(response => {
          if (response) {
            this.$message.success('用户添加成功')
            this.$emit('updateData')
          } else {
            this.$message.error('用户添加失败')
          }
        }).catch(error => {
          console.log(error)
        })
      }
    }
  }
}
</script>
