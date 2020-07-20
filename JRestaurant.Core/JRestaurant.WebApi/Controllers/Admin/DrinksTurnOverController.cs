using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using JRestaurant.WebApi.AuthAttributes;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Web.Http;

namespace JRestaurant.WebApi.Controllers.Admin
{
    [RoutePrefix("admin/drinksturnover")]
    [ApiAuthorize]
    public class DrinksTurnOverController : ApiController
    {
        [HttpPost]
        [Route("AddDrinksTurnOver")]
        public bool AddDrinksTurnOver([FromBody] DrinksTurnOver dt)
        {
            return DrinksTurnOverHandler.AddDrinksTurnOver(dt);
        }

        [HttpPost]
        [Route("UpdateDrinksTurnOver")]
        public bool UpdateDrinksTurnOver([FromBody] DrinksTurnOver dt)
        {
            return DrinksTurnOverHandler.UpdateDrinksTurnOver(dt);
        }

        [HttpDelete]
        public bool Delete(int id)

        {
            return DrinksTurnOverHandler.DeleteDrinksTurnOver(id);
        }

        [HttpGet]
        [Route("ListDrinksTurnOverByPage")]
        public JArray ListDrinksTurnOverByPage(int index, int pageSize)
        {
            DataSet ds = DrinksTurnOverHandler.ListDrinksTurnOverByPage(index, pageSize);
            return CommonHandler.TransferArrayFromDS(ds);
        }


        [HttpGet]
        [Route("GetTotalCount")]
        public int GetTotalCount()
        {
            return CommonHandler.GetTotalCount("DrinksTurnOVer");
        }

        [HttpGet]
        [Route("ListDrinksTurnOverByWeek")]
        public JArray ListDrinksTurnOverByWeek(int week)
        {
            DataSet ds = DrinksTurnOverHandler.ListDrinksTurnOverByWeek(week);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("ListDrinksTurnOverToday")]
        public JArray ListDrinksTurnOverToday()
        {
            DataSet ds = DrinksTurnOverHandler.ListDrinksTurnOverToday();
            return CommonHandler.TransferArrayFromDS(ds);
        }

    }
}
