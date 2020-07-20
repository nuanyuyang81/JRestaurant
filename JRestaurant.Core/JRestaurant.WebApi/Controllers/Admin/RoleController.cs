using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using JRestaurant.WebApi.AuthAttributes;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web.Http;

namespace JRestaurant.WebApi.Controllers.Admin
{
    [RoutePrefix("admin/role")]
    [ApiAuthorize]
    public class RoleController : ApiController
    {
        public string OptionsRole()
        {
            return null;
        }

        [HttpPost]
        [Route("AddRole")]
        public bool AddRole([FromBody] Roles role)
        {
            return RoleHandler.AddRole(role);
        }

        [HttpPost]
        [Route("UpdateRole")]
        public bool UpdateRole([FromBody] Roles role)
        {
            return RoleHandler.UpdateRoleInfo(role);
        }

        [HttpGet]
        [Route("InActiveRole")]
        public bool InActiveRole(int roleId, string ownerId)
        {
            return RoleHandler.InActiveRole(roleId, Guid.Parse(ownerId));
        }

        [HttpGet]
        [Route("ListAllRoles")]
        public JArray ListAllRoles()
        {
            DataSet ds = RoleHandler.ListAllRoles();
            return CommonHandler.TransferArrayFromDS(ds);
        }
    }
}
