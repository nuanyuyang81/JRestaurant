using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using JRestaurant.WebApi.AuthAttributes;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web.Http;

namespace JRestaurant.WebApi.Controllers.Admin
{
    [RoutePrefix("admin/manage")]
    [ApiAuthorize]
    public class AdminController : ApiController
    {
        [HttpPost]
        [Route("AddAdmin")]
        public bool AddAdmin([FromBody] Entities.Admin usr)
        {
            return AdminHandler.AddAdmin(usr);
        }

        [HttpPost]
        [Route("UpdateAdminProfile")]
        public bool UpdateAdminProfile([FromBody] Entities.Admin usr)
        {
            return AdminHandler.UpdateAdminProfile(usr);
        }
        [HttpGet]
        [Route("InActiveAdmin")]
        public bool InActiveAdmin(string id)
        {
            return AdminHandler.InActiveAdmin(Guid.Parse(id));
        }

        [HttpGet]
        [Route("CheckAdminExist")]
        public bool CheckAdminExist(string phone)
        {
            return AdminHandler.CheckAdminExist(phone);
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public bool UpdatePassword(dynamic obj)
        {
            string id = obj.id;
            string newpwd = obj.newpwd;
            return AdminHandler.UpdatePassword(Guid.Parse(id), newpwd);
        }

        [HttpGet]
        [Route("GetUsrInfo")]
        public JObject GetUsrInfo(string phone)
        {
            DataSet ds = AdminHandler.GetUsrInfo(phone);
            return CommonHandler.TransferObjFromDS(ds);
        }

        [HttpGet]
        [Route("ListAllAdmins")]
        public JArray ListAllAdmins()
        {
            DataSet ds = AdminHandler.ListAllAdmins();
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("ListAdminsByRole")]
        public JArray ListAdminsByRole(int roleId)
        {
            DataSet ds = AdminHandler.ListAdminsByRole(roleId);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("GetTotalCount")]
        public int GetTotalCount()
        {
            return CommonHandler.GetTotalCount("Admin");
        }
    }
}
