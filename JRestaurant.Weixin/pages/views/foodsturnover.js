(global["webpackJsonp"]=global["webpackJsonp"]||[]).push([["pages/views/foodsturnover"],{"2e95":function(t,e,a){"use strict";a.r(e);var n=a("7d54"),r=a("8324");for(var o in r)"default"!==o&&function(t){a.d(e,t,(function(){return r[t]}))}(o);a("7a9c");var i,u=a("f0c5"),s=Object(u["a"])(r["default"],n["b"],n["c"],!1,null,null,null,!1,n["a"],i);e["default"]=s.exports},"59fb":function(t,e,a){},"7a9c":function(t,e,a){"use strict";var n=a("59fb"),r=a.n(n);r.a},"7d54":function(t,e,a){"use strict";var n,r=function(){var t=this,e=t.$createElement;t._self._c},o=[];a.d(e,"b",(function(){return r})),a.d(e,"c",(function(){return o})),a.d(e,"a",(function(){return n}))},8324:function(t,e,a){"use strict";a.r(e);var n=a("8738"),r=a.n(n);for(var o in n)"default"!==o&&function(t){a.d(e,t,(function(){return n[t]}))}(o);e["default"]=r.a},8738:function(t,e,a){"use strict";(function(t){Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var n=r(a("e8fe"));function r(t){return t&&t.__esModule?t:{default:t}}var o=new n.default,i={name:"TurnOverIndex",data:function(){return{tabCur:2,title:"本周料理营业额",week:1,loadModal:!1,modalName:null,dtList:[],date:"",currentdate:"",ftColumns:[{title:"星期",key:"week",width:100}],ftTableData:[],weekList:[1,2,3,4,5,6,0]}},onLoad:function(){var t=this,e=new Date;this.date=this.timeformat(e.toLocaleString()),this.currentdate=this.timeformat(e.toLocaleString()),this.loadModal=!0,o.get("http://81.70.37.99/JRestaurant/weixin/ListTurnOverTypeByAim",{params:{aim:0}}).then((function(e){var a=e.data;t.ftColumns.push({title:"总额",key:"total",width:130}),a.forEach((function(e){t.ftColumns.push({title:e.Name,key:"Id"+e.Id,width:180})})),t.ftColumns.push({title:"营收日期",key:"date",width:180}),t.refreshData(1)})).catch((function(t){console.log(t)}))},methods:{toChild:function(e){t.navigateTo({url:e.currentTarget.dataset.url})},refreshData:function(t){var e=this;this.loadModal=!0,this.ftTableData=[],this.weekList.forEach((function(t){var a={};e.ftColumns.forEach((function(t){var e=t["key"];a[e]=0})),a.week=e.convertWeekDay(t),a.date="N/A",e.ftTableData.push(a)})),o.get("http://81.70.37.99/JRestaurant/weixin/ListFoodsTurnOverByWeek",{params:{week:t}}).then((function(t){e.dtList=t.data,e.loadModal=!1,e.dtList.forEach((function(t){e.ftTableData.forEach((function(a){if(e.convertWeekDay(t.WeekDay)===a.week){var n="Id"+t.TypeId;a[n]=a[n]+parseFloat(t.Amount),a.date=e.timeformat(t.AddDate)}var r=0;for(var n in a)n.indexOf("Id")>-1&&(r+=parseFloat(a[n]));a.total=r}))}))})).catch((function(t){console.log(t)}))},goToPWeek:function(){this.week--,this.week>0&&this.refreshData(this.week)},goToNWeek:function(){this.week++,this.week>1&&this.refreshData(this.week)},DateChange:function(t){this.date=t.detail.value;var e=this.TodayInfo(this.date);console.log(e.week),this.refreshData(e.week)},TodayInfo:function(t){var e,a,n,r,o,i=7,u=["日","一","二","三","四","五","六"],s={week:null,day:null},c=864e5,f=/\d{4}[\/-]\d{1,2}[\/-]\d{1,2}/g;return f.test(t)?(o=new Date(t.replace("-","/")),a=o.getDay(),a=0===a?7:a,e=i-a,n=new Date,s.day=u[n.getDay()],n=new Date(n.getFullYear()+"/"+(n.getMonth()+1)+"/"+n.getDate()),r=n-o,r<0?s:(r=parseInt(r/c),s.week=Math.ceil((r-e)/i)+1,s)):(alert("请使用合法的开学日期！！！"),s)},timeformat:function(t){var e=t;if(null!=e){e=e.replace(/\D/g," ");var a=e.split(" ");a[1]--;var n=new Date(Date.UTC(a[0],a[1],a[2],a[3],a[4],a[5])),r=n.toLocaleString();return r.substring(0,r.indexOf(" "))}return""},convertWeekDay:function(t){var e="";switch(t){case 1:e="周一";break;case 2:e="周二";break;case 3:e="周三";break;case 4:e="周四";break;case 5:e="周五";break;case 6:e="周六";break;case 0:e="周日";break;default:e="周一";break}return e}}};e.default=i}).call(this,a("543d")["default"])},ba50:function(t,e,a){"use strict";(function(t){a("0955");n(a("66fd"));var e=n(a("2e95"));function n(t){return t&&t.__esModule?t:{default:t}}t(e.default)}).call(this,a("543d")["createPage"])}},[["ba50","common/runtime","common/vendor"]]]);