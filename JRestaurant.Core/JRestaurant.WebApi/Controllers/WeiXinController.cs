using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Web.Http;

namespace JRestaurant.WebApi.Controllers
{
    [RoutePrefix("weixin")]
    public class WeiXinController : ApiController
    {
        #region 料理食材营业额
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
        #endregion

        #region 酒水营业额
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
        #endregion

        #region 料理食材进货
        [HttpGet]
        [Route("ListFoodsPOByWeek")]
        public JArray ListFoodsPOByWeek(int week)
        {
            DataSet ds = FoodsPOHandler.ListFoodsPOByWeek(week);
            return CommonHandler.TransferArrayFromDS(ds);
        }
        [HttpGet]
        [Route("GetFoodsPOByToDay")]
        public JArray GetFoodsPOByToDay()
        {
            DataSet ds = FoodsPOHandler.GetFoodsPOByToDay();
            return CommonHandler.TransferArrayFromDS(ds);
        }
        #endregion

        #region 酒水进货
        [HttpGet]
        [Route("ListDrinksPOByWeek")]
        public JArray ListDrinksPOByWeek(int week)
        {
            DataSet ds = DrinksPOHandler.ListDrinksPOByWeek(week);
            return CommonHandler.TransferArrayFromDS(ds);
        }
        [HttpGet]
        [Route("GetDrinksPOByToDay")]
        public JArray GetDrinksPOByToDay()
        {
            DataSet ds = DrinksPOHandler.GetDrinksPOByToDay();
            return CommonHandler.TransferArrayFromDS(ds);
        }
        #endregion

        #region 供应商
        [HttpGet]
        [Route("ListVendorByAim")]
        public JArray ListVendorByAim(int aim)
        {
            DataSet ds = VendorHandler.ListVendorByAim(aim);
            return CommonHandler.TransferArrayFromDS(ds);
        }
        #endregion
    }
}
