<template>
	<view>
		<manage v-if="PageCur=='manage'"></manage>
	</view>
</template>

<script>
	export default {
		data() {
		return {
				PageCur: 'manage'
			}
		},
		methods: {
			NavChange: function(e) {
				this.PageCur = e.currentTarget.dataset.cur
			}
		}
	}
</script>

<style>

</style>
