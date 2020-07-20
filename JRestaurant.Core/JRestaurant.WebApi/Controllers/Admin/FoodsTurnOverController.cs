using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using JRestaurant.WebApi.AuthAttributes;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Web.Http;

namespace JRestaurant.WebApi.Controllers.Admin
{
    [RoutePrefix("admin/foodsturnover")]
    [ApiAuthorize]
    public class FoodsTurnOverController : ApiController
    {
        public string OptionsFoodsTurnOver() { return null; }

        [HttpPost]
        [Route("AddFoodsTurnOver")]
        public bool AddFoodsTurnOver([FromBody] FoodsTurnOver ft)
        {
            return FoodsTurnOverHandler.AddFoodsTurnOver(ft);
        }

        [HttpPost]
        [Route("UpdateFoodsTurnOver")]
        public bool UpdateFoodsTurnOver([FromBody] FoodsTurnOver ft)
        {
            return FoodsTurnOverHandler.UpdateFoodsTurnOver(ft);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return FoodsTurnOverHandler.DeleteFoodsTurnOver(id);
        }

        [HttpGet]
        [Route("ListFoodsTurnOverByPage")]
        public JArray ListFoodsTurnOverByPage(int index, int pageSize)
        {
            DataSet ds = FoodsTurnOverHandler.ListFoodsTurnOverByPage(index, pageSize);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("GetTotalCount")]
        public int GetTotalCount()
        {
            return CommonHandler.GetTotalCount("FoodsTurnOver");
        }

        [HttpGet]
        [Route("ListFoodsTurnOverByWeek")]
        public JArray ListFoodsTurnOverByWeek(int week)
        {
            DataSet ds = FoodsTurnOverHandler.ListFoodsTurnOverByWeek(week);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("ListFoodsTurnOverToday")]
        public JArray ListFoodsTurnOverToday()
        {
            DataSet ds = FoodsTurnOverHandler.ListFoodsTurnOverToday();
            return CommonHandler.TransferArrayFromDS(ds);
        }
    }
}
