using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using JRestaurant.WebApi.AuthAttributes;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Web.Http;

namespace JRestaurant.WebApi.Controllers.Admin
{
    [RoutePrefix("admin/foodspo")]
    [ApiAuthorize]
    public class FoodsPOController : ApiController
    {
        public string OptionsFoodsPO() { return null; }

        [HttpPost]
        [Route("AddFoodsPO")]
        public bool AddFoodsPO([FromBody] FoodsPO po)
        {
            return FoodsPOHandler.AddFoodsPO(po);
        }

        [HttpPost]
        [Route("UpdateFoodsPO")]
        public bool UpdateFoodsPO([FromBody] FoodsPO po)
        {
            return FoodsPOHandler.UpdateFoodsPO(po);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return FoodsPOHandler.DeleteFoodsPO(id);
        }

        [HttpGet]
        [Route("ListFoodsPOByPage")]
        public JArray ListFoodsPOByPage(int index, int pageSize)
        {
            DataSet ds = FoodsPOHandler.ListFoodsPOByPage(index, pageSize);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("ListFoodsPOByWeek")]
        public JArray ListFoodsPOByWeek(int week)
        {
            DataSet ds = FoodsPOHandler.ListFoodsPOByWeek(week);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("GetTotalCount")]
        public int GetTotalCount()
        {
            return CommonHandler.GetTotalCount("FoodsPO");
        }

        [HttpGet]
        [Route("GetFoodsPOByToDay")]
        public JArray GetFoodsPOByToDay()
        {
            DataSet ds = FoodsPOHandler.GetFoodsPOByToDay();
            return CommonHandler.TransferArrayFromDS(ds);
        }
    }
}
