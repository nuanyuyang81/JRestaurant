using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using JRestaurant.WebApi.AuthAttributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JRestaurant.WebApi.Controllers.Admin
{
    [RoutePrefix("admin/log")]
    [ApiAuthorize]
    public class LogController : ApiController
    {
        public string OptionsLog()
        {
            return null;
        }

        [HttpPost]
        [Route("AddLog")]
        public bool AddLog([FromBody] Logs log)
        {
            return LogHandler.AddLog(log);
        }

        [HttpDelete]
        [Route("DeleteLog")]
        public bool DeleteLog(int id)
        {
            return LogHandler.DeleteLog(id);
        }

        [HttpGet]
        [Route("ListFullLogsByPage")]
        public JArray ListFullLogsByPage(int index, int pageSize)
        {
            DataSet ds = LogHandler.ListFullLogsByPage(index, pageSize);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("ListTableLogsByPage")]
        public JArray ListTableLogsByPage(int index, int pageSize, string table)
        {
            DataSet ds = LogHandler.ListTableLogsByPage(index, pageSize, table);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("GetTotalFullCount")]
        public int GetTotalFullCount()
        {
            return LogHandler.GetTotalFullCount();
        }

        [HttpGet]
        [Route("GetTotalCountByTable")]
        public int GetTotalCountByTable(string table)
        {
            return LogHandler.GetTotalCountByTable(table);
        }
    }
}
