using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JRestaurant.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace JRestaurant.Test
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void GetTotalCount()
        {
            int result = CommonHandler.GetTotalCount("Admin");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetWeekStartTime()
        {
            DateTime weektime = CommonHandler.GetWeekStartTime();
            Console.WriteLine(weektime);

            Assert.IsNotNull(weektime);
        }
    }
}
