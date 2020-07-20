<template>
  <div class="app-container">
    <el-row style="text-align: center;margin-top:10px">
      <el-form ref="form" label-width="80px" size="mini">
        <el-form-item label="进货日期">
          <el-date-picker
            v-model="AddDate"
            align="right"
            type="date"
            placeholder="选择进货日期"
            style="width:150px"
          />
          <el-button type="warning" style="margin-left:10px" @click="clear">清除</el-button>
          <el-button type="primary" @click="submit">提交</el-button>
        </el-form-item>
      </el-form>
    </el-row>

    <el-row>
      <el-col :span="12">
        <el-card style="margin-top:10px;max-width:400px;margin-left: auto;margin-right: 10px">
          <div slot="header" class="clearfix">
            <span>酒水营业额</span>
          </div>
          <el-form label-width="120px" label-position="left" size="mini">
            <el-form-item v-for="item in typeList" :key="item.typeId" :label="item.typeName">
              <el-input v-model="item.Amount" />
            </el-form-item>
            <el-form-item label="营业总额" prop="Total">
              <el-input v-model="Total" :disabled="true" />
            </el-form-item>
          </el-form>
        </el-card>
      </el-col>
      <el-col :span="12">
        <el-card style="margin-top:10px;max-width:400px;margin-right:auto;margin-left:10px">
          <div slot="header" class="clearfix">
            <span>折扣后酒水营业额</span>
          </div>
          <el-form label-width="120px" label-position="left" size="mini">
            <el-form-item v-for="item in discountTypeList" :key="item.typeId" :label="item.typeName">
              <el-input v-model="item.Amount" />
            </el-form-item>
            <el-form-item label="营业总额" prop="Total">
              <el-input v-model="discountTotal" :disabled="true" />
            </el-form-item>
          </el-form>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>
<script>
import { listTypesByAim } from '@/api/turnovertype'
import { addDT } from '@/api/drinksturnover'
import Cookies from 'js-cookie'

export default {
  name: 'AddBatch',
  data() {
    return {
      typeList: [],
      discountTypeList: [],
      AddDate: '',
      rules: {
        VendorId: [
          { required: true, trigger: 'blur' }
        ],
        Amount: [
          { required: true, trigger: 'blur' }
        ],
        AddDate: [
          { required: true, trigger: 'blur' }
        ],
        Comments: [
          { required: true, trigger: 'blur' }
        ]
      }
    }
  },
  computed: {
    Total() {
      var result = 0
      this.typeList.forEach(item => {
        result = result + parseFloat(item.Amount)
      })
      return result
    },
    discountTotal() {
      var result = 0
      this.discountTypeList.forEach(item => {
        result = result + parseFloat(item.Amount)
      })
      return result
    }
  },
  mounted() {
    var date = new Date()
    this.AddDate = this.dateFormat('YYYY-mm-dd HH:MM:SS', date)
    listTypesByAim(1).then(response => {
      var list = response
      for (var i = 0; i < list.length; i++) {
        this.typeList.push({ typeId: list[i].Id, typeName: list[i].Name, Amount: 0, Discount: false })
        this.discountTypeList.push({ typeId: list[i].Id, typeName: list[i].Name, Amount: 0, Discount: true })
      }
    }).catch(error => {
      console.log(error)
    })
  },
  methods: {
    clear() {
      this.typeList.forEach(item => {
        item.Amount = 0
      })
      this.discount.forEach(item => {
        item.Amount = 0
      })
      this.AddDate = ''
    },
    submit() {
      if (this.Total > 0) {
        this.typeList.forEach(item => {
          if (item.Amount > 0) {
            addDT({ Amount: item.Amount, Discount: item.Discount, TypeId: item.typeId, AddDate: this.AddDate, OwnerId: Cookies.get('uid') }).then(response => {
              if (response) {
                this.$message.success('酒水营业额记录记录添加成功')
              } else {
                this.$message.error('酒水营业额记录添加失败')
              }
            }).catch(error => {
              console.log(error)
            })
          }
        })
      } else {
        this.$message.error('请先填写正常营业额')
      }
      if (this.discountTotal > 0) {
        this.discountTypeList.forEach(item => {
          if (item.Amount > 0) {
            addDT({ Amount: item.Amount, Discount: item.Discount, TypeId: item.typeId, AddDate: this.AddDate, OwnerId: Cookies.get('uid') }).then(response => {
              if (response) {
                this.$message.success('折后酒水营业额记录记录添加成功')
              } else {
                this.$message.error('折后酒水营业额记录添加失败')
              }
            }).catch(error => {
              console.log(error)
            })
          }
        })
      } else {
        this.$message.error('请先填写折后营业额')
      }
    },
    dateFormat(fmt, date) {
      let ret
      const opt = {
        'Y+': date.getFullYear().toString(), // 年
        'm+': (date.getMonth() + 1).toString(), // 月
        'd+': date.getDate().toString(), // 日
        'H+': date.getHours().toString(), // 时
        'M+': date.getMinutes().toString(), // 分
        'S+': date.getSeconds().toString() // 秒
        // 有其他格式化字符需求可以继续添加，必须转化成字符串
      }
      for (const k in opt) {
        ret = new RegExp('(' + k + ')').exec(fmt)
        if (ret) {
          fmt = fmt.replace(ret[1], (ret[1].length === 1) ? (opt[k]) : (opt[k].padStart(ret[1].length, '0')))
        }
      }
      return fmt
    }
  }
}
</script>

