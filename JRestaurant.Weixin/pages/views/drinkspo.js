(global["webpackJsonp"]=global["webpackJsonp"]||[]).push([["pages/views/drinkspo"],{"0111":function(e,t,a){"use strict";(function(e){Object.defineProperty(t,"__esModule",{value:!0}),t.default=void 0;var n=o(a("e8fe"));function o(e){return e&&e.__esModule?e:{default:e}}var r=new n.default,i={name:"TurnOverIndex",data:function(){return{tabCur:2,title:"本周酒水进货记录",week:1,modalName:null,dpoList:[],date:"",currentdate:"",loadModal:!1,dpoColumns:[{title:"星期",key:"week",width:100}],dpoTableData:[],weekList:[1,2,3,4,5,6,0]}},onLoad:function(){var e=this,t=new Date;this.date=this.timeformat(t.toLocaleString()),this.currentdate=this.timeformat(t.toLocaleString()),this.loadModal=!0,r.get("http://81.70.37.99/JRestaurant/weixin/ListVendorByAim",{params:{aim:1}}).then((function(t){var a=t.data;e.dpoColumns.push({title:"总额",key:"total",width:120}),a.forEach((function(t){e.dpoColumns.push({title:t.Name,key:"Id"+t.Id,width:180})})),e.dpoColumns.push({title:"进货日期",key:"date",width:180}),e.refreshData(1)})).catch((function(e){console.log(e)}))},methods:{toChild:function(t){e.navigateTo({url:t.currentTarget.dataset.url})},refreshData:function(e){var t=this;this.dpoTableData=[],this.weekList.forEach((function(e){var a={};t.dpoColumns.forEach((function(e){var t=e["key"];a[t]=0})),a.week=t.convertWeekDay(e),a.date="N/A",t.dpoTableData.push(a)})),this.loadModal=!0,r.get("http://81.70.37.99/JRestaurant/weixin/ListDrinksPOByWeek",{params:{week:e}}).then((function(e){t.dpoList=e.data,t.loadModal=!1,t.dpoList.forEach((function(e){t.dpoTableData.forEach((function(a){if(t.convertWeekDay(e.WeekDay)===a.week){var n="Id"+e.VendorId;a[n]=a[n]+parseFloat(e.Amount),a.date=t.timeformat(e.AddDate)}var o=0;for(var n in a)n.indexOf("Id")>-1&&(isNaN(a[n])||(o+=parseFloat(a[n])));a.total=o}))}))})).catch((function(e){console.log(e)}))},goToPWeek:function(){this.week--,this.week>0&&this.refreshData(this.week)},goToNWeek:function(){this.week++,this.week>1&&this.refreshData(this.week)},DateChange:function(e){this.date=e.detail.value;var t=this.TodayInfo(this.date);console.log(t.week),this.refreshData(t.week)},TodayInfo:function(e){var t,a,n,o,r,i=7,u=["日","一","二","三","四","五","六"],s={week:null,day:null},c=864e5,d=/\d{4}[\/-]\d{1,2}[\/-]\d{1,2}/g;return d.test(e)?(r=new Date(e.replace("-","/")),a=r.getDay(),a=0===a?7:a,t=i-a,n=new Date,s.day=u[n.getDay()],n=new Date(n.getFullYear()+"/"+(n.getMonth()+1)+"/"+n.getDate()),o=n-r,o<0?s:(o=parseInt(o/c),s.week=Math.ceil((o-t)/i)+1,s)):(alert("请使用合法的开学日期！！！"),s)},timeformat:function(e){var t=e;if(null!=t){t=t.replace(/\D/g," ");var a=t.split(" ");a[1]--;var n=new Date(Date.UTC(a[0],a[1],a[2],a[3],a[4],a[5])),o=n.toLocaleString();return o.substring(0,o.indexOf(" "))}return""},convertWeekDay:function(e){var t="";switch(e){case 1:t="周一";break;case 2:t="周二";break;case 3:t="周三";break;case 4:t="周四";break;case 5:t="周五";break;case 6:t="周六";break;case 0:t="周日";break;default:t="周一";break}return t}}};t.default=i}).call(this,a("543d")["default"])},"0c21":function(e,t,a){},"4aaf":function(e,t,a){"use strict";var n,o=function(){var e=this,t=e.$createElement;e._self._c},r=[];a.d(t,"b",(function(){return o})),a.d(t,"c",(function(){return r})),a.d(t,"a",(function(){return n}))},"5b37":function(e,t,a){"use strict";a.r(t);var n=a("4aaf"),o=a("fed6");for(var r in o)"default"!==r&&function(e){a.d(t,e,(function(){return o[e]}))}(r);a("8882");var i,u=a("f0c5"),s=Object(u["a"])(o["default"],n["b"],n["c"],!1,null,null,null,!1,n["a"],i);t["default"]=s.exports},8882:function(e,t,a){"use strict";var n=a("0c21"),o=a.n(n);o.a},ebfb:function(e,t,a){"use strict";(function(e){a("0955");n(a("66fd"));var t=n(a("5b37"));function n(e){return e&&e.__esModule?e:{default:e}}e(t.default)}).call(this,a("543d")["createPage"])},fed6:function(e,t,a){"use strict";a.r(t);var n=a("0111"),o=a.n(n);for(var r in n)"default"!==r&&function(e){a.d(t,e,(function(){return n[e]}))}(r);t["default"]=o.a}},[["ebfb","common/runtime","common/vendor"]]]);