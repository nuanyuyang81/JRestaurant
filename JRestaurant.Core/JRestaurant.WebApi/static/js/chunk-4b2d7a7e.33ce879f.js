(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-4b2d7a7e"],{"0fea":function(e,t,a){"use strict";a.d(t,"c",(function(){return n})),a.d(t,"b",(function(){return i})),a.d(t,"a",(function(){return l})),a.d(t,"d",(function(){return o})),a.d(t,"e",(function(){return s}));var r=a("b775");function n(){return Object(r["a"])({url:"/admin/manage/ListAllAdmins",method:"get"})}function i(){return Object(r["a"])({url:"/admin/manage/GetTotalCount",method:"get"})}function l(e){return Object(r["a"])({url:"/admin/manage/AddAdmin",method:"post",data:e})}function o(e){return Object(r["a"])({url:"/admin/manage/UpdateAdminProfile",method:"post",data:e})}function s(e){return Object(r["a"])({url:"/admin/manage/UpdatePassword",method:"post",data:e})}},"7dbd":function(e,t,a){},a796:function(e,t,a){"use strict";var r=a("7dbd"),n=a.n(r);n.a},cc5e:function(e,t,a){"use strict";a.d(t,"c",(function(){return n})),a.d(t,"a",(function(){return i})),a.d(t,"d",(function(){return l})),a.d(t,"b",(function(){return o}));var r=a("b775");function n(){return Object(r["a"])({url:"/admin/role/ListAllRoles",method:"get"})}function i(e){return Object(r["a"])({url:"/admin/role/AddRole",method:"post",data:e})}function l(e){return Object(r["a"])({url:"/admin/role/UpdateRole",method:"post",data:e})}function o(e,t){return Object(r["a"])({url:"/admin/role/InActiveRole",params:{roleId:e,ownerId:t}})}},fa8c:function(e,t,a){"use strict";a.r(t);var r=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",{staticClass:"app-container"},[a("div",{staticClass:"filter-container"},[a("el-button",{staticClass:"filter-item",attrs:{type:"primary",size:"mini",icon:"el-icon-plus"},on:{click:function(t){e.UserDialogVisible=!0}}},[e._v(" 添加新用户 ")])],1),a("el-row",{directives:[{name:"loading",rawName:"v-loading",value:e.loadAdmin,expression:"loadAdmin"}],staticStyle:{width:"95%"}},e._l(e.adminList,(function(e){return a("el-col",{key:e.Id,staticStyle:{"margin-top":"10px","margin-left":"10px",width:"320px"}},[a("item",{attrs:{id:e.Id,name:e.UserName,roleid:e.RoleId,role:e.RoleName,phone:e.PhoneNumber,createtime:e.CreateTime,lastlogintime:e.LastLoginTime,rolekey:e.RoleKey}})],1)})),1),a("el-dialog",{attrs:{title:"用户信息管理",center:"",top:"5vh",visible:e.UserDialogVisible,width:"30%"},on:{"update:visible":function(t){e.UserDialogVisible=t}}},[a("add",{on:{updateData:e.refreshData}})],1)],1)},n=[],i=a("0fea"),l=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("el-card",{staticStyle:{height:"150px","text-align":"left"},attrs:{shadow:"hover"}},[a("div",{staticClass:"clearfix",attrs:{slot:"header"},slot:"header"},[a("el-tag",[e._v(e._s(e.name))]),e.delable?a("el-button",{staticStyle:{float:"right"},attrs:{type:"danger",size:"mini"},on:{click:e.deleteUser}},[e._v("删除用户")]):e._e()],1),a("el-row",[a("el-col",{attrs:{span:6}},[a("svg-icon",{staticClass:"modelImage",attrs:{"icon-class":"avatar"}}),a("el-button",{attrs:{type:"text"},on:{click:e.updateAdmin}},[e._v("编辑")])],1),a("el-col",{staticStyle:{"font-size":"12px","padding-left":"5px",color:"#606266"},attrs:{span:18}},[a("el-row",{staticStyle:{margin:"5px"}},[e._v("权 限: "+e._s(e.role))]),a("el-row",{staticStyle:{margin:"5px"}},[e._v("电 话: "+e._s(e.phone))]),a("el-row",{staticStyle:{margin:"5px"}},[e._v("注册时间: "+e._s(null!=e.createtime?e.dateformat(e.createtime):""))]),a("el-row",{staticStyle:{margin:"5px"}},[e._v("登录时间: "+e._s(null!=e.lastlogintime?e.dateformat(e.lastlogintime):""))])],1)],1),a("el-dialog",{attrs:{title:"用户信息管理",center:"",top:"5vh",visible:e.dialogVisible,width:"30%"},on:{"update:visible":function(t){e.dialogVisible=t}}},[a("add",{attrs:{adminid:e.id,username:e.name,phone:e.phone,roleid:e.roleid}})],1)],1)},o=[],s=(a("a9e3"),a("ac1f"),a("5319"),a("1276"),a("a78e")),d=a.n(s),u=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("el-card",[a("el-form",{attrs:{"label-width":"120px",rules:e.rules,"label-position":"left",model:e.targetUser,size:"mini"}},[a("el-form-item",{attrs:{label:"用户名",prop:"UserName"}},[a("el-input",{model:{value:e.targetUser.UserName,callback:function(t){e.$set(e.targetUser,"UserName",t)},expression:"targetUser.UserName"}})],1),a("el-form-item",{attrs:{label:"电话",prop:"PhoneNumber"}},[a("el-input",{model:{value:e.targetUser.PhoneNumber,callback:function(t){e.$set(e.targetUser,"PhoneNumber",t)},expression:"targetUser.PhoneNumber"}})],1),null==e.adminid?a("el-form-item",{attrs:{label:"密码",prop:"Password"}},[a("el-input",{attrs:{type:"password"},model:{value:e.targetUser.Password,callback:function(t){e.$set(e.targetUser,"Password",t)},expression:"targetUser.Password"}})],1):e._e(),a("el-form-item",{attrs:{label:"权限",prop:"RoleId"}},[a("el-select",{attrs:{clearable:""},model:{value:e.targetUser.RoleId,callback:function(t){e.$set(e.targetUser,"RoleId",t)},expression:"targetUser.RoleId"}},e._l(e.roleList,(function(e){return a("el-option",{key:e.Id,attrs:{label:e.Name,value:e.Id}})})),1)],1)],1),a("el-row",{staticStyle:{"text-align":"center"}},[a("el-button",{attrs:{type:"warning"},on:{click:e.clear}},[e._v("清除")]),a("el-button",{attrs:{type:"primary"},on:{click:e.submit}},[e._v("提交")])],1)],1)},c=[],m=(a("498a"),a("cc5e")),p=a("8237"),f=a.n(p),g={name:"Add",props:{adminid:{type:String,default:null},username:{type:String,default:null},phone:{type:String,default:null},roleid:{type:Number,default:null}},data:function(){return{targetUser:{UserName:"",PhoneNumber:"",Password:"",RoleId:1},roleList:[],rules:{UserName:[{required:!0,trigger:"blur"}],PhoneNumber:[{required:!0,trigger:"blur"}],RoleName:[{required:!0,trigger:"blur"}]}}},mounted:function(){var e=this;Object(m["c"])().then((function(t){e.roleList=t})).catch((function(e){console.log(e)})),console.log(this.adminid),null!==this.adminid&&(this.targetUser.UserName=this.username,this.targetUser.PhoneNumber=this.phone,this.targetUser.RoleId=this.roleid)},methods:{clear:function(){this.targetUser={Name:"",PhoneNumber:"",RoleName:"",RoleId:""}},submit:function(){var e=this;null!==this.adminid?Object(i["d"])({UserName:this.targetUser.UserName.trim(),PhoneNumber:this.targetUser.PhoneNumber.trim(),RoleId:this.targetUser.RoleId,Id:this.adminid}).then((function(t){t?(e.$message.success("用户信息更新成功"),e.$emit("updateData")):e.$message.error("用户信息更新失败")})).catch((function(e){console.log(e)})):Object(i["a"])({UserName:this.targetUser.UserName.trim(),PhoneNumber:this.targetUser.PhoneNumber.trim(),PasswordHash:f()(this.targetUser.Password.trim()),RoleId:this.targetUser.RoleId}).then((function(t){t?(e.$message.success("用户添加成功"),e.$emit("updateData")):e.$message.error("用户添加失败")})).catch((function(e){console.log(e)}))}}},h=g,b=a("2877"),U=Object(b["a"])(h,u,c,!1,null,null,null),v=U.exports,y={name:"Item",components:{add:v},props:{id:{type:String,default:null},name:{type:String,default:null},role:{type:String,default:null},roleid:{type:Number,default:null},phone:{type:String,default:null},createtime:{type:String,default:null},lastlogintime:{type:String,default:null},rolekey:{type:String,default:null}},data:function(){return{dialogVisible:!1}},computed:{delable:function(){return d.a.get("phone")!==this.phone&&"admin"!==this.rolekey}},methods:{deleteUser:function(){},updateAdmin:function(){this.dialogVisible=!0},dateformat:function(e){var t=e;if(null!=t){t=t.replace(/\D/g," ");var a=t.split(" ");a[1]--;var r=new Date(Date.UTC(a[0],a[1],a[2],a[3],a[4],a[5]));return r.toLocaleString()}return""}}},N=y,w=(a("a796"),Object(b["a"])(N,l,o,!1,null,"4d000d2a",null)),_=w.exports,x={name:"Manage",components:{item:_,add:v},data:function(){return{adminList:[],loadAdmin:!1,UserDialogVisible:!1}},mounted:function(){this.refreshData()},methods:{refreshData:function(){var e=this;this.loadAdmin=!0,Object(i["c"])().then((function(t){e.adminList=t,e.loadAdmin=!1})).catch((function(e){console.log(e)}))}}},I=x,P=Object(b["a"])(I,r,n,!1,null,null,null);t["default"]=P.exports}}]);