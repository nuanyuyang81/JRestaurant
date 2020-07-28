import Vue from 'vue'
import App from './App'

import manage from './pages/manage/home.vue'
Vue.component('manage',manage)

import cuCustom from './colorui/components/cu-custom.vue'
Vue.component('cu-custom',cuCustom)
import plugintable from './pages/component/plugin-table/plugin-table.vue'
Vue.component('plugin-table', plugintable)
import discounttable from './pages/component/discount-table/discount-table.vue'
Vue.component('discount-table', discounttable)

Vue.config.productionTip = false

App.mpType = 'app'

const app = new Vue({
    ...App
})
app.$mount()

 



