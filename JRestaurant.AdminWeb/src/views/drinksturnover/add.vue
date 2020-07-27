<template>
  <div class="app-container">
    <el-row style="text-align: center;margin-top:10px;max-width:800px;margin-left: auto;margin-right: auto">
      <el-form ref="form" label-width="80px" size="mini">
        <el-form-item>
          <el-tag slot="label" size="medium">进货日期</el-tag>
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
      <el-card style="margin-top:10px;">
        <div slot="header" class="clearfix">
          <span>酒水营业额</span>
        </div>
        <el-button type="primary" style="margin-left:auto;margin-right:auto" @click="addTurnOver">添加一条营业额记录</el-button>
        <el-table :data="addList" :max-height="maxHeight">
          <el-table-column
            prop="TypeId"
            label="营业额类型"
            width="150"
          >
            <template slot-scope="{row}">
              <el-select v-model="row.TypeId" clearable placeholder="营业额类型">
                <el-option
                  v-for="type in typeList"
                  :key="type.Id"
                  :label="type.Name"
                  :value="type.Id"
                />
              </el-select>
            </template>
          </el-table-column>
          <el-table-column
            prop="Amount"
            label="金额"
            width="150"
          >
            <template slot-scope="{row}">
              <el-input-number v-model="row.Amount" controls-position="right" :min="0" :precision="2" :step="1" />
            </template>
          </el-table-column>
          <el-table-column
            prop="Discount"
            label="折扣"
            width="150"
          >
            <template slot-scope="{row}">
              <el-input-number v-model="row.Discount" controls-position="right" :min="0.01" :max="1" :precision="2" :step="0.01" />
            </template>
          </el-table-column>
          <el-table-column
            prop="Comments"
            label="备注"
            min-width="260"
          >
            <template slot-scope="{row}">
              <el-input v-model="row.Comments" autosize type="textarea" placeholder="备注" clearable />
            </template>
          </el-table-column>
          <el-table-column width="40">
            <template slot-scope="index">
              <el-popconfirm
                title="确认要删除该条营业额记录吗？"
                @onConfirm="delAddItem(index)"
              >
                <el-button slot="reference" type="danger" icon="el-icon-delete" circle style="float:right" />
              </el-popconfirm>
            </template>
          </el-table-column>
        </el-table>
      </el-card>
      <el-form label-width="100px" label-position="left" size="mini" style="margin-top:20px;">
        <el-form-item>
          <el-tag slot="label" size="medium">营业总额</el-tag>
          <el-input v-model="totalAmount" :disabled="true" style="width:200px;float:left" />
        </el-form-item>
        <el-form-item>
          <el-tag slot="label" size="medium">折后营业总额</el-tag>
          <el-input v-model="discountAmount" :disabled="true" style="width:200px;float:left" />
        </el-form-item>
      </el-form>

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
      addList: [
        {
          TypeId: null,
          Amount: 0,
          Discount: 1,
          Comments: ''
        }
      ],
      discountTypeList: [],
      AddDate: '',
      maxHeight: document.body.offsetHeight - 400,
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
    totalAmount() {
      var result = 0
      this.addList.forEach(item => {
        result = result + parseFloat(item.Amount)
      })
      return result
    },
    discountAmount() {
      var result = 0
      this.addList.forEach(item => {
        result = result + parseFloat(item.Amount) * parseFloat(item.Discount)
      })
      return result
    }
  },
  mounted() {
    this.maxHeight = document.body.offsetHeight - 400
    window.onresize = () => {
      return (() => {
        this.maxHeight = document.body.offsetHeight - 400
      })()
    }
    var date = new Date()
    this.AddDate = this.dateFormat('YYYY-mm-dd HH:MM:SS', date)
    listTypesByAim(1).then(response => {
      this.typeList = response
    }).catch(error => {
      console.log(error)
    })
  },
  methods: {
    clear() {
      this.AddDate = ''
    },
    delAddItem(index) {
      console.log(index)
      this.addList.splice(index, 1)
    },
    addTurnOver() {
      this.addList.push({
        TypeId: null,
        Amount: 0,
        Discount: 1,
        Comments: ''
      })
    },
    submit() {
      this.addList.forEach((item, index) => {
        console.log(item.TypeId)
        if (typeof (item.TypeId) !== undefined && item.TypeId != null && item.TypeId > 0) {
          console.log(item.TypeId)
          if (item.Amount > 0 && item.Discount > 0) {
            addDT({ Amount: item.Amount, Discount: item.Discount, TypeId: item.TypeId, AddDate: this.AddDate, Comments: item.Comments, OwnerId: Cookies.get('uid') }).then(response => {
              if (response) {
                this.$message.success('酒水营业额记录记录添加成功')
              } else {
                this.$message.error('酒水营业额记录添加失败')
              }
              if (index === this.addList.length - 1) {
                this.$emit('updateData')
              }
            }).catch(error => {
              console.log(error)
            })
          } else {
            this.$message.error('不允许添加金额为 0 或者 折扣为 0 的营业额记录')
          }
        } else {
          this.$message.error('请选择营业额类型')
        }
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

