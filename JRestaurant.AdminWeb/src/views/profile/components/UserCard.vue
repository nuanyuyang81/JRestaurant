<template>
  <el-card style="margin-bottom:20px;">
    <div slot="header" class="clearfix">
      <span>用户个人信息</span>
    </div>

    <div class="user-profile">
      <div class="box-center">
        <pan-thumb :image="'https://d.lanrentuku.com/down/png/1610/16young-avatar-collection/man.png'" :height="'100px'" :width="'100px'" :hoverable="false">
          <div>您好</div>
          {{ roles }}
        </pan-thumb>
      </div>
      <div class="box-center">
        <div class="user-name text-center">{{ name }}</div>
      </div>
    </div>
    <div class="user-bio">
      <div class="user-education user-bio-section">
        <div class="user-bio-section-header"><svg-icon icon-class="password" /><span>修改密码</span></div>
        <div class="user-bio-section-body">
          <div class="text-muted">
            <el-form ref="passForm" :model="passForm" status-icon :rules="rules" label-width="100px" class="demo-ruleForm">
              <el-form-item label="新密码" prop="pass">
                <el-input v-model="passForm.pass" type="password" autocomplete="off" />
              </el-form-item>
              <el-form-item label="确认密码" prop="checkPass">
                <el-input v-model="passForm.checkPass" type="password" autocomplete="off" />
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="submit">确认修改</el-button>
              </el-form-item>
            </el-form>
          </div>
        </div>
      </div>
    </div>
  </el-card>
</template>

<script>
import PanThumb from '@/components/PanThumb'
import { mapGetters } from 'vuex'
import { updatePassword } from '@/api/manage'
import Cookies from 'js-cookie'
import md5 from 'js-md5'
export default {
  components: { PanThumb },
  data() {
    var validatePass = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('请输入密码'))
      } else {
        if (this.passForm.checkPass !== '') {
          this.$refs.passForm.validateField('checkPass')
        }
        callback()
      }
    }
    var validatePass2 = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('请再次输入密码'))
      } else if (value !== this.passForm.pass) {
        callback(new Error('两次输入密码不一致!'))
      } else {
        callback()
      }
    }
    return {
      passForm: {
        pass: '',
        checkPass: ''
      },
      rules: {
        pass: [
          { validator: validatePass, trigger: 'blur' }
        ],
        checkPass: [
          { validator: validatePass2, trigger: 'blur' }
        ]
      }
    }
  },
  computed: {
    ...mapGetters([
      'roles',
      'name'
    ])
  },
  methods: {
    submit() {
      updatePassword({ id: Cookies.get('uid'), newpwd: md5(this.passForm.pass) }).then(response => {
        if (response) {
          this.$message.success('密码修改成功')
        } else {
          this.$message.error('密码修改失败')
        }
      }).catch(error => {
        console.log(error)
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.box-center {
  margin: 0 auto;
  display: table;
}

.text-muted {
  color: #777;
}

.user-profile {
  .user-name {
    font-weight: bold;
  }

  .box-center {
    padding-top: 10px;
  }

  .user-role {
    padding-top: 10px;
    font-weight: 400;
    font-size: 14px;
  }

  .box-social {
    padding-top: 30px;

    .el-table {
      border-top: 1px solid #dfe6ec;
    }
  }

  .user-follow {
    padding-top: 20px;
  }
}

.user-bio {
  margin-top: 20px;
  color: #606266;

  span {
    padding-left: 4px;
  }

  .user-bio-section {
    font-size: 14px;
    padding: 15px 0;

    .user-bio-section-header {
      border-bottom: 1px solid #dfe6ec;
      padding-bottom: 10px;
      margin-bottom: 10px;
      font-weight: bold;
    }
  }
}
</style>
