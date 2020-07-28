<style lang="less">
  @import '@/pages/style/table.less';
</style>
<template name="component">
	<view>
		<scroll-view scroll-y class="page">
			<cu-custom bgColor="bg-gradual-blue" :isBack="true">
				<block slot="backText">返回</block>
				<block slot="content">酒水进货</block>
			</cu-custom>
			<view class="unicorn-body">
				<view class="unicorn-margin-top-common">			
					<view class="unicorn-card">
						<view class="unicorn-card-title">
							<text class="cuIcon-circlefill text-blue"></text>
							<text class="text-grey" style="margin-left:20rpx">{{ title }}</text>
						</view>
						<view class="unicorn-card-context">
							<plugin-table :tableData='dpoTableData' :columns='dpoColumns' :showBottomSum='true' :stickSide='true'>
							</plugin-table>
						</view>
					</view>
				</view>
			</view>
			<view class="padding">
				<button class="cu-btn block bg-blue margin-tb-sm lg" @click="goToNWeek">查看上一周</button>
				<button class="cu-btn block line-blue margin-tb-sm lg" :disabled="week===1" @click="goToPWeek">查看下一周</button>
			</view>
			<form>
				<view class="cu-form-group">
					<view class="title">日期选择</view>
					<picker mode="date" :value="date" start="2015-09-01" :end="currentdate" @change="DateChange">
						<view class="picker">
							{{date}}
						</view>
					</picker>
				</view>
			</form>	
		</scroll-view>
		<view class="cu-load load-modal" v-if="loadModal">
			<!-- <view class="cuIcon-emojifill text-orange"></view> -->
			<image src="/static/logo.png" mode="aspectFit"></image>
			<view class="gray-text">数据加载中...</view>
		</view>
	</view>
</template>

<script>
	import Request from '@/js_sdk/luch-request/luch-request/index.js'
	const http = new Request()
	export default {
		name: "TurnOverIndex",
		data() {
			return {
				tabCur: 2,
				title: '本周酒水进货记录',
				week: 1,
				modalName: null,
				dpoList: [],
				date: '',
				currentdate: '',
				loadModal: false,
				dpoColumns: [{
						"title": "星期",
						"key": "week",
						"width": 100
					}],
				dpoTableData: [],
				weekList: [1,2,3,4,5,6,0]
			};
		},
		onLoad() {
			var _date = new Date()
			this.date = this.timeformat(_date.toLocaleString())
			this.currentdate = this.timeformat(_date.toLocaleString())
			this.loadModal = true
			http.get('http://81.70.37.99/JRestaurant/weixin/ListVendorByAim', {params: {aim: 1}}).then(res => {
				var list = res.data
				this.dpoColumns.push({
					"title": "总额",
					"key": "total",
					"width": 120
				})
				list.forEach(item => {
					this.dpoColumns.push({
						"title": item.Name,
						"key": "Id" + item.Id,
						"width": 180
					})
				})
				this.dpoColumns.push({
						"title": "进货日期",
						"key": "date",
						"width": 180
					})
				this.refreshData(1)
			}).catch(err => {
				console.log(err)
			})
		},
		methods: {
			toChild(e) {
				uni.navigateTo({
					url: e.currentTarget.dataset.url
				})
			},
			refreshData(week) {
				this.dpoTableData = []
				this.weekList.forEach(week => {
					var target = {}
					this.dpoColumns.forEach(dpo => {
						var key = dpo['key']
						target[key] = 0
					})
					target.week = this.convertWeekDay(week)
					target.date = 'N/A'
					this.dpoTableData.push(target)
				})
				this.loadModal = true
				http.get('http://81.70.37.99/JRestaurant/weixin/ListDrinksPOByWeek', {params: {week: week}}).then(res => {
					this.dpoList = res.data
					this.loadModal = false
					this.dpoList.forEach(dpo => {
						this.dpoTableData.forEach(row => {
							if (this.convertWeekDay(dpo.WeekDay) === row.week) {
								var key = "Id" + dpo.VendorId
								row[key] = row[key] + parseFloat(dpo.Amount)
								row.date = this.timeformat(dpo.AddDate)
							}
							var total = 0
							for (var key in row) {
								if (key.indexOf("Id")>-1) {
									if (!isNaN(row[key])) {
										total = total + parseFloat(row[key])
									}
								}
							}
							row.total = total
						})
					})
					
				}).catch(err => {
					console.log(err)
				})
			},
			goToPWeek() {
			  this.week--
			  if (this.week > 0) {
				this.refreshData(this.week)
			  }
			},
			goToNWeek() {
			  this.week++
			  if (this.week > 1) {
				this.refreshData(this.week)
			  }
			},
			DateChange(e) {
				this.date = e.detail.value.replace(/-/g,"/")
				var td = this.TodayInfo(this.date)
				console.log(td.week)
				this.refreshData(td.week)
			},
			TodayInfo(start) {
				var WEEKLEN = 7, // 一周7天为常量
					WEEKDAYS = ["日", "一", "二", "三", "四", "五", "六"],
					weekInfo = {"week": null, "day": null}, // 初始化返回信息，默认第null周，星期null
					oneDay = 24 * 60 * 60 * 1000, // 一天的毫秒时长
					weekLeave,
					weekStart,
					today, // 今天
					dateDiff,
					sDate;
				var rDateStr = /\d{4}[\/-]\d{1,2}[\/-]\d{1,2}/g; // 简单的日期格式校验：2013/12/19
				if (!rDateStr.test(start)) {
					return weekInfo;
				}
				sDate = new Date(start.replace("-", "/"));
				weekStart = sDate.getDay();
				weekStart = weekStart === 0 ? 7 : weekStart; // JS中周日的索引为0，这里转换为7，方便计算
					
				weekLeave = WEEKLEN - weekStart;
				today = new Date();
				weekInfo.day = WEEKDAYS[today.getDay()];
				today = new Date(today.getFullYear() + "/" + (today.getMonth() + 1) + "/" + today.getDate());
				dateDiff = today - sDate;
				if (dateDiff < 0) {
					return weekInfo;
				}
				dateDiff = parseInt(dateDiff / oneDay);
				weekInfo.week = Math.ceil((dateDiff - weekLeave) / WEEKLEN) + 1;
				return weekInfo;
			},
			timeformat(time) {
			  var dtstr = time
			  if (dtstr != null) {
				dtstr = dtstr.replace(/\D/g, ' ')
				var dtcomps = dtstr.split(' ')
				dtcomps[1]--
				var convdt = new Date(Date.UTC(dtcomps[0], dtcomps[1], dtcomps[2], dtcomps[3], dtcomps[4], dtcomps[5]))
				var date = convdt.toLocaleString()
				return date.substring(0, date.indexOf(' '))
			  }
			  return ''
			},
			convertWeekDay(weekday) {
			      var day = ''
			      switch (weekday) {
			        case 1:
			          day = '周一'
			          break
			        case 2:
			          day = '周二'
			          break
			        case 3:
			          day = '周三'
			          break
			        case 4:
			          day = '周四'
			          break
			        case 5:
			          day = '周五'
			          break
			        case 6:
			          day = '周六'
			          break
			        case 0:
			          day = '周日'
			          break
			        default:
			          day = '周一'
			          break
			      }
			      return day
			    }
		},
	}
</script>
