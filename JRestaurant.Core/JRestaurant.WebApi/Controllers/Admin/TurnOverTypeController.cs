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
    [RoutePrefix("admin/turnovertype")]
    [ApiAuthorize]
    public class TurnOverTypeController : ApiController
    {
        public string OptionsTurnOverType()
        {
            return null;
        }


        [HttpPost]
        [Route("AddTurnOverType")]
        public bool AddTurnOverType([FromBody] TurnOverTypes type)
        {
            return TurnOverTypesHanlder.AddTurnOverTypes(type);
        }

        [HttpPost]
        [Route("UpdateTurnOverType")]
        public bool UpdateTurnOverType([FromBody] TurnOverTypes type)
        {
            return TurnOverTypesHanlder.UpdateTurnOverTypes(type);
        }

        [HttpGet]
        [Route("InActiveTurnOverTypes")]
        public bool InActiveTurnOverTypes(int id, string uid)
        {
            return TurnOverTypesHanlder.InActiveTurnOverTypes(id, Guid.Parse(uid));
        }

        [HttpGet]
        [Route("ListAllTurnOverTypes")]
        public JArray ListAllTurnOverTypes()
        {
            DataSet ds = TurnOverTypesHanlder.ListAllTurnOverTypes();
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("ListTurnOverTypeByAim")]
        public JArray ListTurnOverTypeByAim(int aim)
        {
            DataSet ds = TurnOverTypesHanlder.ListTurnOverTypeByAim(aim);
            return CommonHandler.TransferArrayFromDS(ds);
        }

    }
}
