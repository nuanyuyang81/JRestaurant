using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JRestaurant.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string dt = DateTime.Now.ToLongDateString();
            Console.WriteLine(dt);
            
        }
    }
}
