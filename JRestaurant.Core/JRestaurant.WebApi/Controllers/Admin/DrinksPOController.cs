using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using JRestaurant.WebApi.AuthAttributes;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace JRestaurant.WebApi.Controllers.Admin
{
    [RoutePrefix("admin/drinkspo")]
    [ApiAuthorize]
    public class DrinksPOController : ApiController
    {
        public string OptionsDrinksPO() { return null; }

        [HttpPost]
        [Route("AddDrinksPO")]
        public bool AddDrinksPO([FromBody] DrinksPO po)
        {
            return DrinksPOHandler.AddDrinksPO(po);
        }

        [HttpPost]
        [Route("UpdateDrinksPO")]
        public bool UpdateDrinksPO([FromBody] DrinksPO po)
        {
            return DrinksPOHandler.UpdateDrinksPO(po);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return DrinksPOHandler.DeleteDrinksPO(id);
        }

        [HttpGet]
        [Route("ListDrinksPOByPage")]
        public JArray ListDrinksPOByPage(int index, int pageSize)
        {
            DataSet ds = DrinksPOHandler.ListDrinksPOByPage(index, pageSize);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("ListDrinksPOByWeek")]
        public JArray ListDrinksPOByWeek(int week)
        {
            DataSet ds = DrinksPOHandler.ListDrinksPOByWeek(week);
            return CommonHandler.TransferArrayFromDS(ds);
        }

        [HttpGet]
        [Route("GetTotalCount")]
        public int GetTotalCount()
        {
            return CommonHandler.GetTotalCount("DrinksPO");
        }

        [HttpGet]
        [Route("GetDrinksPOByToDay")]
        public JArray GetDrinksPOByToDay()
        {
            DataSet ds = DrinksPOHandler.GetDrinksPOByToDay();
            return CommonHandler.TransferArrayFromDS(ds);
        }
    }
}
