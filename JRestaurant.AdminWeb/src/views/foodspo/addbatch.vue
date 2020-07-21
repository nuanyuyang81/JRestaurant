<template>
  <el-card style="margin-top:10px;max-width:800px;margin-right: auto; margin-left: auto;">
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
    <el-table :data="vendorList" border fit style="width: 100%">
      <el-table-column
        label="序列"
        type="index"
        width="50"
      />
      <el-table-column align="center" label="供应商" width="120">
        <template slot-scope="{row}">
          <el-tag size="medium" effect="plain">{{ row.vendorName }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="进货金额" width="185">
        <template slot-scope="{row}">
          <template>
            <el-input-number v-model="row.Amount" :precision="2" :step="0.1" style="width:160px" />
          </template>
        </template>
      </el-table-column>
      <el-table-column label="备注">
        <template slot-scope="{row}">
          <template>
            <el-input v-model="row.Comments" class="edit-input" size="small" />
          </template>
        </template>
      </el-table-column>
    </el-table>
  </el-card>
</template>
<script>
import { ListVendorByAim } from '@/api/vendor'
import { addFoodsPO } from '@/api/foodspo'
import Cookies from 'js-cookie'

export default {
  name: 'AddBatch',
  data() {
    return {
      vendorList: [],
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
  mounted() {
    var date = new Date()
    this.AddDate = this.dateFormat('YYYY-mm-dd HH:MM:SS', date)
    ListVendorByAim(0).then(response => {
      var list = response
      for (var i = 0; i < list.length; i++) {
        this.vendorList.push({ vendorId: list[i].Id, vendorName: list[i].Name, Amount: 0, Comments: '' })
      }
    }).catch(error => {
      console.log(error)
    })
  },
  methods: {
    clear() {
      this.AddDate = ''
    },
    submit() {
      this.vendorList.forEach((item, index) => {
        addFoodsPO({ VendorId: item.vendorId, Amount: item.Amount, AddDate: this.AddDate, Comments: item.Comments, OwnerId: Cookies.get('uid') }).then(response => {
          if (response) {
            this.$message.success('料理进货记录添加成功')
          } else {
            this.$message.error('料理进货记录添加失败')
          }
          if (index === this.vendorList.length - 1) {
            this.$emit('updateData')
          }
        }).catch(error => {
          console.log(error)
        })
      })
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

