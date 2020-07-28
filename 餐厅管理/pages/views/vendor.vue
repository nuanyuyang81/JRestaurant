<template>
	<view>
		<scroll-view :scroll-y="modalName==null" class="page" :class="modalName!=null?'show':''">
			<cu-custom bgColor="bg-gradual-pink" :isBack="true">
				<block slot="backText">返回</block>
				<block slot="content">供应商</block>
			</cu-custom>
			<view class="cu-bar bg-white solid-bottom margin-top">
				<view class="action">
					<text class="cuIcon-title text-orange"></text> 料理供应商
				</view>
			</view>
			<view class="cu-list menu" :class="[menuBorder?'sm-border':'',menuCard?'card-menu margin-top':'']">
				<view class="cu-item" :class="menuArrow?'arrow':''" v-for="(item,index) in foodsVendorList" :key="index">
					<view class="content">
						<text class="cuIcon-circlefill text-grey"></text>
						<text class="text-grey">{{ item.Name }}</text>
					</view>
				</view>
			</view>
			<view class="cu-bar bg-white solid-bottom margin-top">
				<view class="action">
					<text class="cuIcon-title text-orange"></text> 酒水供应商
				</view>
			</view>
			<view class="cu-list menu" :class="[menuBorder?'sm-border':'',menuCard?'card-menu margin-top':'']">
				<view class="cu-item" :class="menuArrow?'arrow':''" v-for="(item,index) in drinksVendorList" :key="index">
					<view class="content">
						<text class="cuIcon-circlefill text-grey"></text>
						<text class="text-grey">{{ item.Name }}</text>
					</view>
				</view>
			</view>
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
		data() {
			return {
				modalName: null,
				gridCol: 3,
				gridBorder: false,
				menuBorder: true,
				menuArrow: false,
				menuCard: true,
				skin: false,
				loadModal: false,
				listTouchStart: 0,
				listTouchDirection: null,
				foodsVendorList: [],
				drinksVendorList: []
			};
		},
		onLoad() {
			this.loadModal = true
			http.get('http://81.70.37.99/JRestaurant/weixin/ListVendorByAim', {params: {aim: 0}}).then(res => {
				this.foodsVendorList = res.data
				this.loadModal = false
			}).catch(err => {
				console.log(err)
			})
			http.get('http://81.70.37.99/JRestaurant/weixin/ListVendorByAim', {params: {aim: 1}}).then(res => {
				this.drinksVendorList = res.data
			}).catch(err => {
				console.log(err)
			})
		},
		methods: {
			showModal(e) {
				this.modalName = e.currentTarget.dataset.target
			},
			hideModal(e) {
				this.modalName = null
			},
			Gridchange(e) {
				this.gridCol = e.detail.value
			},
			Gridswitch(e) {
				this.gridBorder = e.detail.value
			},
			MenuBorder(e) {
				this.menuBorder = e.detail.value
			},
			MenuArrow(e) {
				this.menuArrow = e.detail.value
			},
			MenuCard(e) {
				this.menuCard = e.detail.value
			},
			SwitchSex(e) {
				this.skin = e.detail.value
			},

			// ListTouch触摸开始
			ListTouchStart(e) {
				this.listTouchStart = e.touches[0].pageX
			},

			// ListTouch计算方向
			ListTouchMove(e) {
				this.listTouchDirection = e.touches[0].pageX - this.listTouchStart > 0 ? 'right' : 'left'
			},

			// ListTouch计算滚动
			ListTouchEnd(e) {
				if (this.listTouchDirection == 'left') {
					this.modalName = e.currentTarget.dataset.target
				} else {
					this.modalName = null
				}
				this.listTouchDirection = null
			}
		}
	}
</script>

<style>
	.page {
		height: 100Vh;
		width: 100vw;
	}

	.page.show {
		overflow: hidden;
	}

	.switch-sex::after {
		content: "\e716";
	}

	.switch-sex::before {
		content: "\e7a9";
	}

	.switch-music::after {
		content: "\e66a";
	}

	.switch-music::before {
		content: "\e6db";
	}
</style>
