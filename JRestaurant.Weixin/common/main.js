(global["webpackJsonp"]=global["webpackJsonp"]||[]).push([["common/main"],{"0173":function(t,e,n){"use strict";(function(t){Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var r=o(n("66fd"));function o(t){return t&&t.__esModule?t:{default:t}}var a={onLaunch:function(){t.getSystemInfo({success:function(t){r.default.prototype.StatusBar=t.statusBarHeight;var e=wx.getMenuButtonBoundingClientRect();r.default.prototype.Custom=e,r.default.prototype.CustomBar=e.bottom+e.top-t.statusBarHeight}}),r.default.prototype.ColorList=[{title:"嫣红",name:"red",color:"#e54d42"},{title:"桔橙",name:"orange",color:"#f37b1d"},{title:"明黄",name:"yellow",color:"#fbbd08"},{title:"橄榄",name:"olive",color:"#8dc63f"},{title:"森绿",name:"green",color:"#39b54a"},{title:"天青",name:"cyan",color:"#1cbbb4"},{title:"海蓝",name:"blue",color:"#0081ff"},{title:"姹紫",name:"purple",color:"#6739b6"},{title:"木槿",name:"mauve",color:"#9c26b0"},{title:"桃粉",name:"pink",color:"#e03997"},{title:"棕褐",name:"brown",color:"#a5673f"},{title:"玄灰",name:"grey",color:"#8799a3"},{title:"草灰",name:"gray",color:"#aaaaaa"},{title:"墨黑",name:"black",color:"#333333"},{title:"雅白",name:"white",color:"#ffffff"}]},onShow:function(){console.log("App Show")},onHide:function(){console.log("App Hide")}};e.default=a}).call(this,n("543d")["default"])},"0656":function(t,e,n){"use strict";n.r(e);var r=n("f148"),o=n("d693");for(var a in o)"default"!==a&&function(t){n.d(e,t,(function(){return o[t]}))}(a);n("215a");var i,l=n("f0c5"),u=Object(l["a"])(o["default"],r["b"],r["c"],!1,null,null,null,!1,r["a"],i);e["default"]=u.exports},"10ed":function(t,e,n){"use strict";(function(t){Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var r=o(n("4795"));o(n("66fd"));function o(t){return t&&t.__esModule?t:{default:t}}function a(t,e,n,r,o,a,i){try{var l=t[a](i),u=l.value}catch(c){return void n(c)}l.done?e(u):Promise.resolve(u).then(r,o)}function i(t){return function(){var e=this,n=arguments;return new Promise((function(r,o){var i=t.apply(e,n);function l(t){a(i,r,o,l,u,"next",t)}function u(t){a(i,r,o,l,u,"throw",t)}l(void 0)}))}}var l={data:function(){return{version:"1.1.0",nowSortKey:"",sortType:"desc",longTable:!0,lineHeight:t.upx2px(64),tableLoaded:!1,tableShow:!0,selectAll:!1,selectArr:[]}},computed:{compluteHeight:function(){return this.tableHeight?"height: "+t.upx2px(this.tableHeight)+"px":""}},props:{tableData:{type:[Array,Boolean],default:function(){return!1}},columns:{type:[Array,Boolean],required:!0},stickSide:{type:Boolean,default:!1},showBottomSum:{type:Boolean,default:!1},showLoading:{type:Boolean,default:!0},emptyText:{type:String,default:"暂无数据"},tableHeight:{type:[Number,Boolean],default:0},showSelect:{type:Boolean,default:!1},singleSelect:{type:Boolean,default:!1},textAlign:{type:String,default:"left"},titleTextAlign:{type:String,default:"left"}},mounted:function(){this.init()},watch:{columns:function(){this.init()},tableData:function(){this.init()}},methods:{init:function(){var t=this;return i(r.default.mark((function e(){var n,o,a;return r.default.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return t.selectAll=!1,t.selectArr=[],t.tableLoaded=!1,t.tableShow=!0,n=t,e.next=7,n.getPageSize(".z-table-container");case 7:return o=e.sent,e.next=10,n.getPageSize(".z-table-pack");case 10:a=e.sent,n.timer&&clearTimeout(n.timer),o&&a?(n.$nextTick((function(){n.tableData&&n.tableData.length&&(n.tableShow=!1,n.timer=setTimeout((function(){n.tableLoaded=!0}),300))})),o.height!=a.height?n.longTable=!0:n.longTable=!1):(n.tableLoaded=!1,n.$nextTick((function(){n.tableShow=!0})));case 13:case"end":return e.stop()}}),e)})))()},getPageSize:function(e){var n=t.createSelectorQuery().in(this);return new Promise((function(t,r){n.select(e).boundingClientRect((function(e){t(e)})).exec()}))},dosum:function(t){var e="-";return this.tableData&&this.tableData.every((function(e){return!Number.isNaN(e[t]-0)}))&&(e=0,this.tableData.map((function(n,r){if(t||0==r){var o=n[t]-0;Number.isNaN(o)?e+=0:e+=o}else e="-"}))),this.numTransform(e)},getRowContent:function(t,e){var n="",r=t[e.key];if([null,""].includes(r)&&(r="-"),["nvl"].includes(r)&&(r=""),r||0===r)n=isNaN(r-0)?r:this.numTransform(r-0);else if(e.format){var o=e.format.template;e.format.names.map((function(e){var n=new RegExp("#".concat(e,"#"),"mg");o=o.replace(n,t[e])})),n=o}else if(!e.render){var a=new Error("数据的key或format值至少一个不为空");throw a}return n.toString()},sort:function(t,e){t&&this.columns[e].sort&&(t!=this.nowSortKey?(this.nowSortKey=t,this.sortType="desc"):this.toggleSort(),this.$emit("onSort",{key:this.nowSortKey,type:this.sortType}))},toggleSort:function(){this.sortType="asc"==this.sortType?"desc":"asc"},numTransform:function(t){return Number.isNaN(t-0)?t:t.toString()},resetSort:function(){this.nowSortKey="",this.sortType="desc"},setUrl:function(t,e){if(e.isLink){var n={},r=e.isLink,o=r.url,a=r.params,i=void 0===a?[]:a;return i.forEach((function(e){if(~e.indexOf("|")){var r=e.split("|");n[r[0]]=t[r[1]]}else n[e]=t[e]})),o=this.setUrlParams(o,n),o}},setUrlParams:function(t,e){var n=t,r=Object.keys(e);return r.forEach((function(t){n+="&".concat(t,"=").concat(e[t])})),n=n.replace(/\&/,"?"),n},itemClick:function(t,e){e.listenerClick&&this.$emit("onClick",t)},doSelect:function(){var t=arguments.length>0&&void 0!==arguments[0]&&arguments[0],e=arguments.length>1?arguments[1]:void 0,n=new Set;if(t){if(!this.selectAll)for(var r=0;r<this.tableData.length;r++)n.add(r)}else this.selectArr.forEach((function(t){n.add(t)})),n.has(e)?n.delete(e):(this.singleSelect&&n.clear(),n.add(e));this.selectArr=Array.from(n),this.selectArr.length==this.tableData.length?this.selectAll=!0:this.selectAll=!1,this.$emit("onSelect",this.selectArr)},getTitleText:function(t){var e=t;return e.toString()}}};e.default=l}).call(this,n("543d")["default"])},1788:function(t,e,n){"use strict";(function(t){n("0955");var e=l(n("66fd")),r=l(n("6bf2")),o=l(n("8251")),a=l(n("0656")),i=l(n("c227"));function l(t){return t&&t.__esModule?t:{default:t}}function u(t,e){var n=Object.keys(t);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(t);e&&(r=r.filter((function(e){return Object.getOwnPropertyDescriptor(t,e).enumerable}))),n.push.apply(n,r)}return n}function c(t){for(var e=1;e<arguments.length;e++){var n=null!=arguments[e]?arguments[e]:{};e%2?u(Object(n),!0).forEach((function(e){s(t,e,n[e])})):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(n)):u(Object(n)).forEach((function(e){Object.defineProperty(t,e,Object.getOwnPropertyDescriptor(n,e))}))}return t}function s(t,e,n){return e in t?Object.defineProperty(t,e,{value:n,enumerable:!0,configurable:!0,writable:!0}):t[e]=n,t}e.default.component("manage",o.default);var f=function(){n.e("colorui/components/cu-custom").then(function(){return resolve(n("0f7d"))}.bind(null,n)).catch(n.oe)};e.default.component("cu-custom",f),e.default.component("plugin-table",a.default),e.default.component("discount-table",i.default),e.default.config.productionTip=!1,r.default.mpType="app";var d=new e.default(c({},r.default));t(d).$mount()}).call(this,n("543d")["createApp"])},"210f":function(t,e,n){"use strict";n.r(e);var r=n("b1c4"),o=n.n(r);for(var a in r)"default"!==a&&function(t){n.d(e,t,(function(){return r[t]}))}(a);e["default"]=o.a},"215a":function(t,e,n){"use strict";var r=n("eeec"),o=n.n(r);o.a},"2ff3":function(t,e,n){"use strict";var r=n("f13a"),o=n.n(r);o.a},3187:function(t,e,n){"use strict";var r,o=function(){var t=this,e=t.$createElement,n=(t._self._c,t.__map(t.columns,(function(e,n){var r=t.getTitleText(e.title),o=e.hasOwnProperty("key"),a=e.hasOwnProperty("sort");return{$orig:t.__get_orig(e),m0:r,g0:o,g1:a}}))),r=t.__map(t.tableData,(function(e,n){var r=t.selectArr.includes(n),o=t.__map(t.columns,(function(n,r){var o=t.getRowContent(e,n),a=t.setUrl(e,n),i=t.getRowContent(e,n);return{$orig:t.__get_orig(n),m1:o,m2:a,m3:i}}));return{$orig:t.__get_orig(e),g2:r,l1:o}})),o=t.__map(t.columns,(function(e,n){var r=t.dosum(e.key);return{$orig:t.__get_orig(e),m4:r}}));t.$mp.data=Object.assign({},{$root:{l0:n,l2:r,l3:o}})},a=[];n.d(e,"b",(function(){return o})),n.d(e,"c",(function(){return a})),n.d(e,"a",(function(){return r}))},"3d00":function(t,e,n){"use strict";n.r(e);var r=n("fa16"),o=n.n(r);for(var a in r)"default"!==a&&function(t){n.d(e,t,(function(){return r[t]}))}(a);e["default"]=o.a},4352:function(t,e,n){},"6afe":function(t,e,n){},"6bf2":function(t,e,n){"use strict";n.r(e);var r=n("fa15");for(var o in r)"default"!==o&&function(t){n.d(e,t,(function(){return r[t]}))}(o);n("c07e");var a,i,l,u,c=n("f0c5"),s=Object(c["a"])(r["default"],a,i,!1,null,null,null,!1,l,u);e["default"]=s.exports},8251:function(t,e,n){"use strict";n.r(e);var r=n("8b09"),o=n("210f");for(var a in o)"default"!==a&&function(t){n.d(e,t,(function(){return o[t]}))}(a);n("2ff3");var i,l=n("f0c5"),u=Object(l["a"])(o["default"],r["b"],r["c"],!1,null,null,null,!1,r["a"],i);e["default"]=u.exports},"8b09":function(t,e,n){"use strict";var r,o=function(){var t=this,e=t.$createElement;t._self._c},a=[];n.d(e,"b",(function(){return o})),n.d(e,"c",(function(){return a})),n.d(e,"a",(function(){return r}))},b1c4:function(t,e,n){"use strict";(function(t){Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var n={name:"ManageIndex",data:function(){return{elements:[{title:"营业额",name:"turnover",color:"cyan",cuIcon:"newsfill"},{title:"进货",name:"po",color:"blue",cuIcon:"colorlens"},{title:"数据统计",name:"analysis",color:"purple",cuIcon:"font"},{title:"供应商",name:"vendor",color:"mauve",cuIcon:"colorlens"}],StatusBar:this.StatusBar,CustomBar:this.CustomBar}},methods:{toChild:function(e){t.navigateTo({url:e.currentTarget.dataset.url})}}};e.default=n}).call(this,n("543d")["default"])},c07e:function(t,e,n){"use strict";var r=n("4352"),o=n.n(r);o.a},c227:function(t,e,n){"use strict";n.r(e);var r=n("3187"),o=n("3d00");for(var a in o)"default"!==a&&function(t){n.d(e,t,(function(){return o[t]}))}(a);n("f6bb");var i,l=n("f0c5"),u=Object(l["a"])(o["default"],r["b"],r["c"],!1,null,null,null,!1,r["a"],i);e["default"]=u.exports},d693:function(t,e,n){"use strict";n.r(e);var r=n("10ed"),o=n.n(r);for(var a in r)"default"!==a&&function(t){n.d(e,t,(function(){return r[t]}))}(a);e["default"]=o.a},eeec:function(t,e,n){},f13a:function(t,e,n){},f148:function(t,e,n){"use strict";var r,o=function(){var t=this,e=t.$createElement,n=(t._self._c,t.__map(t.columns,(function(e,n){var r=t.getTitleText(e.title),o=e.hasOwnProperty("key"),a=e.hasOwnProperty("sort");return{$orig:t.__get_orig(e),m0:r,g0:o,g1:a}}))),r=t.__map(t.tableData,(function(e,n){var r=t.selectArr.includes(n),o=t.__map(t.columns,(function(n,r){var o=t.getRowContent(e,n),a=t.setUrl(e,n),i=t.getRowContent(e,n);return{$orig:t.__get_orig(n),m1:o,m2:a,m3:i}}));return{$orig:t.__get_orig(e),g2:r,l1:o}})),o=t.__map(t.columns,(function(e,n){var r=t.dosum(e.key);return{$orig:t.__get_orig(e),m4:r}}));t.$mp.data=Object.assign({},{$root:{l0:n,l2:r,l3:o}})},a=[];n.d(e,"b",(function(){return o})),n.d(e,"c",(function(){return a})),n.d(e,"a",(function(){return r}))},f6bb:function(t,e,n){"use strict";var r=n("6afe"),o=n.n(r);o.a},fa15:function(t,e,n){"use strict";n.r(e);var r=n("0173"),o=n.n(r);for(var a in r)"default"!==a&&function(t){n.d(e,t,(function(){return r[t]}))}(a);e["default"]=o.a},fa16:function(t,e,n){"use strict";(function(t){Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var r=o(n("4795"));o(n("66fd"));function o(t){return t&&t.__esModule?t:{default:t}}function a(t,e,n,r,o,a,i){try{var l=t[a](i),u=l.value}catch(c){return void n(c)}l.done?e(u):Promise.resolve(u).then(r,o)}function i(t){return function(){var e=this,n=arguments;return new Promise((function(r,o){var i=t.apply(e,n);function l(t){a(i,r,o,l,u,"next",t)}function u(t){a(i,r,o,l,u,"throw",t)}l(void 0)}))}}var l={data:function(){return{version:"1.1.0",nowSortKey:"",sortType:"desc",longTable:!0,lineHeight:t.upx2px(64),tableLoaded:!1,tableShow:!0,selectAll:!1,selectArr:[]}},computed:{compluteHeight:function(){return this.tableHeight?"height: "+t.upx2px(this.tableHeight)+"px":""}},props:{tableData:{type:[Array,Boolean],default:function(){return!1}},columns:{type:[Array,Boolean],required:!0},stickSide:{type:Boolean,default:!1},showBottomSum:{type:Boolean,default:!1},showLoading:{type:Boolean,default:!0},emptyText:{type:String,default:"暂无数据"},tableHeight:{type:[Number,Boolean],default:0},showSelect:{type:Boolean,default:!1},singleSelect:{type:Boolean,default:!1},textAlign:{type:String,default:"left"},titleTextAlign:{type:String,default:"left"}},mounted:function(){this.init()},watch:{columns:function(){this.init()},tableData:function(){this.init()}},methods:{init:function(){var t=this;return i(r.default.mark((function e(){var n,o,a;return r.default.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return t.selectAll=!1,t.selectArr=[],t.tableLoaded=!1,t.tableShow=!0,n=t,e.next=7,n.getPageSize(".z-table-container");case 7:return o=e.sent,e.next=10,n.getPageSize(".z-table-pack");case 10:a=e.sent,n.timer&&clearTimeout(n.timer),o&&a?(n.$nextTick((function(){n.tableData&&n.tableData.length&&(n.tableShow=!1,n.timer=setTimeout((function(){n.tableLoaded=!0}),300))})),o.height!=a.height?n.longTable=!0:n.longTable=!1):(n.tableLoaded=!1,n.$nextTick((function(){n.tableShow=!0})));case 13:case"end":return e.stop()}}),e)})))()},getPageSize:function(e){var n=t.createSelectorQuery().in(this);return new Promise((function(t,r){n.select(e).boundingClientRect((function(e){t(e)})).exec()}))},dosum:function(t){var e="-";return this.tableData&&this.tableData.every((function(e){return!Number.isNaN(e[t]-0)}))&&(e=0,this.tableData.map((function(n,r){if(t||0==r){var o=n[t]-0;Number.isNaN(o)?e+=0:e+=o}else e="-"}))),this.numTransform(e)},getRowContent:function(t,e){var n="",r=t[e.key];if([null,""].includes(r)&&(r="-"),["nvl"].includes(r)&&(r=""),r||0===r)n=isNaN(r-0)?r:this.numTransform(r-0);else if(e.format){var o=e.format.template;e.format.names.map((function(e){var n=new RegExp("#".concat(e,"#"),"mg");o=o.replace(n,t[e])})),n=o}else if(!e.render){var a=new Error("数据的key或format值至少一个不为空");throw a}return n.toString()},sort:function(t,e){t&&this.columns[e].sort&&(t!=this.nowSortKey?(this.nowSortKey=t,this.sortType="desc"):this.toggleSort(),this.$emit("onSort",{key:this.nowSortKey,type:this.sortType}))},toggleSort:function(){this.sortType="asc"==this.sortType?"desc":"asc"},numTransform:function(t){return Number.isNaN(t-0)?t:t.toString()},resetSort:function(){this.nowSortKey="",this.sortType="desc"},setUrl:function(t,e){if(e.isLink){var n={},r=e.isLink,o=r.url,a=r.params,i=void 0===a?[]:a;return i.forEach((function(e){if(~e.indexOf("|")){var r=e.split("|");n[r[0]]=t[r[1]]}else n[e]=t[e]})),o=this.setUrlParams(o,n),o}},setUrlParams:function(t,e){var n=t,r=Object.keys(e);return r.forEach((function(t){n+="&".concat(t,"=").concat(e[t])})),n=n.replace(/\&/,"?"),n},itemClick:function(t,e){e.listenerClick&&this.$emit("onClick",t)},doSelect:function(){var t=arguments.length>0&&void 0!==arguments[0]&&arguments[0],e=arguments.length>1?arguments[1]:void 0,n=new Set;if(t){if(!this.selectAll)for(var r=0;r<this.tableData.length;r++)n.add(r)}else this.selectArr.forEach((function(t){n.add(t)})),n.has(e)?n.delete(e):(this.singleSelect&&n.clear(),n.add(e));this.selectArr=Array.from(n),this.selectArr.length==this.tableData.length?this.selectAll=!0:this.selectAll=!1,this.$emit("onSelect",this.selectArr)},getTitleText:function(t){var e=t;return e.toString()}}};e.default=l}).call(this,n("543d")["default"])}},[["1788","common/runtime","common/vendor"]]]);