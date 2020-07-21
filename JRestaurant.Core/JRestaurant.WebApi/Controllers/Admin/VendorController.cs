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
    [RoutePrefix("admin/vendor")]
    [ApiAuthorize]
    public class VendorController : ApiController
    {
        public string OptionsVendor()
        {
            return null;
        }


        [HttpPost]
        [Route("AddVendor")]
        public bool AddVendor([FromBody] Vendor vendor)
        {
            return VendorHandler.AddVendor(vendor);
        }

        [HttpPost]
        [Route("UpdateVendor")]
        public bool UpdateVendor([FromBody] Vendor vendor)
        {
            return VendorHandler.UpdateVendor(vendor);
        }

        [HttpGet]
        [Route("InActiveVendor")]
        public bool InActiveVendor(int id, string uid)
        {
            return VendorHandler.InActiveVendor(id, Guid.Parse(uid));
        }

        [HttpGet]
        [Route("ListAllVendors")]
        public JArray ListAllVendros()
        {
            DataSet ds = VendorHandler.ListAllVendors();
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("ListVendorById")]
        public JObject ListVendorById(int id)
        {
            DataSet ds = VendorHandler.ListVendorById(id);
            return CommonHandler.TransferObjFromDS(ds);
        }

        [HttpGet]
        [Route("ListVendorByAim")]
        public JArray ListVendorByAim(int aim)
        {
            DataSet ds = VendorHandler.ListVendorByAim(aim);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("GetTotalCount")]
        public int GetTotalCount()
        {
            return VendorHandler.GetTotalCount();
        }
    }
}
