<template>
	<view>
		<view class="cu-bar bg-white solid-bottom margin-top">
			<view class="action">
				<text class="cuIcon-title text-orange"></text> 料理营业额
			</view>
		</view>
		<view class="qiun-columns">
			<view class="qiun-charts" >
				<canvas canvas-id="ftCanvasColumn" id="ftCanvasColumn" class="charts" @touchstart="touchColumn"></canvas>
			</view>
		</view>
		<view class="cu-bar bg-white solid-bottom margin-top">
			<view class="action">
				<text class="cuIcon-title text-orange"></text> 酒水营业额
			</view>
		</view>
		<view class="qiun-columns">
			<view class="qiun-charts" >
				<canvas canvas-id="dtCanvasColumn" id="dtCanvasColumn" class="charts" @touchstart="touchColumnD"></canvas>
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
	import uCharts from '@/js_sdk/u-charts/u-charts/u-charts.js'
	var _self
	var canvaColumn=null;
	var dcanvaColumn=null;
	export default {
		data() {
			return {
				modalName: null,
				cWidth:'',
				cHeight:'',
				pixelRatio:1,
				date: '',
				currentdate: '',
				categories: ["星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期天"],
				ftseries: [{
					name: "料理营业额",
					data: [0, 0, 0, 0, 0, 0, 0]
				 }],
				dtseries: [{
					name: "酒水营业额",
					data: [0, 0, 0, 0, 0, 0, 0]
				 }],
				week: 1,
				loadModal: false,
				ftList: [],
				dtList: []
			}
		},
		onLoad() {
			var _date = new Date()
			this.date = this.timeformat(_date.toLocaleString())
			this.currentdate = this.timeformat(_date.toLocaleString())
			_self = this
			this.cWidth=uni.upx2px(750)
			this.cHeight=uni.upx2px(500)
			this.refreshData(1)
		},
		methods: {
			refreshData(week) {
				this.loadModal = true
				this.ftTableData = []
				http.get('http://81.70.37.99/JRestaurant/weixin/ListFoodsTurnOverByWeek', {params: {week: week}}).then(res => {
					this.ftList = res.data
					this.loadModal = false
					
					this.ftList.forEach(ft => {
						this.categories.forEach((day, index) => {
							if (this.convertWeekDay(ft.WeekDay) ===  day) {
								this.ftseries[0].data[index] = this.ftseries[0].data[index] + parseFloat(ft.Amount)		
							}
						})
					})
					let ftColumn={categories:[],series:[]};
					ftColumn.categories=this.categories;
					if (this.ftList.length == 0) {
						this.initData()
					}
					ftColumn.series=this.ftseries;
					this.showColumn("ftCanvasColumn",ftColumn);
					
				}).catch(err => {
					console.log(err)
				})
				http.get('http://81.70.37.99/JRestaurant/weixin/ListDrinksTurnOverByWeek', {params: {week: week}}).then(res => {
					this.dtList = res.data
					this.loadModal = false
					
					this.dtList.forEach(dt => {
						this.categories.forEach((day, index) => {
							if (this.convertWeekDay(dt.WeekDay) ===  day) {
								this.dtseries[0].data[index] = this.dtseries[0].data[index] + parseFloat(dt.Amount)		
							}
						})
					})
					let dtColumn={categories:[],series:[]};
					dtColumn.categories=this.categories;
					if (this.dtList.length == 0) {
						this.initData()
					}
					dtColumn.series=this.dtseries;
					this.showColumnd("dtCanvasColumn",dtColumn);
				}).catch(err => {
					console.log(err)
				})
			},
			initData() {
				this.ftseries = [{
					name: "料理营业额",
					data: [0, 0, 0, 0, 0, 0, 0]
				 }],
				this.dtseries = [{
					name: "酒水营业额",
					data: [0, 0, 0, 0, 0, 0, 0]
				 }]
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
				  day = '星期一'
				  break
				case 2:
				  day = '星期二'
				  break
				case 3:
				  day = '星期三'
				  break
				case 4:
				  day = '星期四'
				  break
				case 5:
				  day = '星期五'
				  break
				case 6:
				  day = '星期六'
				  break
				case 0:
				  day = '星期天'
				  break
				default:
				  day = '星期一'
				  break
			  }
			  return day
			},
			showColumn(canvasId,chartData){
				canvaColumn=new uCharts({
					$this:_self,
					canvasId: canvasId,
					type: 'column',
					legend:{show:true},
					fontSize:11,
					background:'#FFFFFF',
					pixelRatio:_self.pixelRatio,
					animation: true,
					categories: chartData.categories,
					series: chartData.series,
					xAxis: {
						disableGrid:true,
					},
					yAxis: {
						//disabled:true
					},
					dataLabel: true,
					width: _self.cWidth*_self.pixelRatio,
					height: _self.cHeight*_self.pixelRatio,
					extra: {
						column: {
							type:'group',
							width: _self.cWidth*_self.pixelRatio*0.45/chartData.categories.length
						}
					  }
				});
				
			},
			showColumnd(canvasId,chartData){
				dcanvaColumn=new uCharts({
					$this:_self,
					canvasId: canvasId,
					type: 'column',
					legend:{show:true},
					fontSize:11,
					background:'#FFFFFF',
					pixelRatio:_self.pixelRatio,
					animation: true,
					categories: chartData.categories,
					series: chartData.series,
					xAxis: {
						disableGrid:true,
					},
					yAxis: {
						//disabled:true
					},
					dataLabel: true,
					width: _self.cWidth*_self.pixelRatio,
					height: _self.cHeight*_self.pixelRatio,
					extra: {
						column: {
							type:'group',
							width: _self.cWidth*_self.pixelRatio*0.45/chartData.categories.length
						}
					  }
				});
				
			},
			touchColumn(e){
				canvaColumn.showToolTip(e, {
					format: function (item, category) {
						if(typeof item.data === 'object'){
							return category + ' ' + item.name + ':' + item.data.value 
						}else{
							return category + ' ' + item.name + ':' + item.data 
						}
					}
				});
			},
			touchColumnD(e){
				dcanvaColumn.showToolTip(e, {
					format: function (item, category) {
						if(typeof item.data === 'object'){
							return category + ' ' + item.name + ':' + item.data.value 
						}else{
							return category + ' ' + item.name + ':' + item.data 
						}
					}
				});
			}
		}
	}
</script>

<style scoped>
	page {
		background: #F2F2F2;
		width: 750rpx;
		overflow-x: hidden;
	}
	
	.qiun-padding {
		padding: 2%;
		width: 96%;
	}
	
	.qiun-wrap {
		display: flex;
		flex-wrap: wrap;
	}
	
	.qiun-rows {
		display: flex;
		flex-direction: row !important;
	}
	
	.qiun-columns {
		display: flex;
		flex-direction: column !important;
	}
	
	.qiun-common-mt {
		margin-top: 10rpx;
	}
	
	.qiun-bg-white {
		background: #FFFFFF;
	}
	
	.qiun-title-bar {
		width: 96%;
		padding: 10rpx 2%;
		flex-wrap: nowrap;
	}
	
	.qiun-title-dot-light {
		border-left: 10rpx solid #0ea391;
		padding-left: 10rpx;
		font-size: 32rpx;
		color: #000000
	}
	
	/* 通用样式 */
	.qiun-charts {
		width: 750rpx;
		height: 500rpx;
		background-color: #FFFFFF;
	}
	
	.charts {
		width: 750rpx;
		height: 500rpx;
		background-color: #FFFFFF;
	}
	
	/* 横屏样式 */
	.qiun-charts-rotate {
		width: 700rpx;
		height: 1100rpx;
		background-color: #FFFFFF;
		padding: 25rpx;
	}
	
	.charts-rotate {
		width: 700rpx;
		height: 1100rpx;
		background-color: #FFFFFF;
	}
	
	/* 圆弧进度样式 */
	.qiun-charts3 {
		width: 750rpx;
		height: 250rpx;
		background-color: #FFFFFF;
		position: relative;
	}
	
	.charts3 {
		position: absolute;
		width: 250rpx;
		height: 250rpx;
		background-color: #FFFFFF;
	}
	
	.qiun-tip {
		display: block;
		width: auto;
		overflow: hidden;
		padding: 15rpx;
		height: 30rpx;
		line-height: 30rpx;
		margin: 10rpx;
		background: #ff9933;
		font-size: 30rpx;
		border-radius: 8rpx;
		justify-content: center;
		text-align: center;
		border: 1px solid #dc7004;
		color: #FFFFFF;
	}
</style>
