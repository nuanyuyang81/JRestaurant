using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace JRestaurant.Test
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void AddAdmin()
        {
            Admin admin = new Admin();
            admin.UserName = "店长-张三";
            admin.PhoneNumber = "13810308263";
            string password = "Esoteric$666";
            admin.PasswordHash = CommonHelper.CreateMD5(password);
            admin.RoleId = 1;

            Assert.IsTrue(AdminHandler.AddAdmin(admin));
        }

        [TestMethod]
        public void LoginAttemp()
        {
            string phone = "13810308263";
            string password = "Esoteric$666";
            string passwordhash = CommonHelper.CreateMD5(password);

            DataSet ds = AdminHandler.LogAttempt(phone, passwordhash);
            Assert.IsTrue(ds.Tables.Count > 0);
            if (ds.Tables.Count > 0)
            {
                JObject obj = CommonHelper.ParseFromDataSet(ds);
                string id = obj.GetValue("Id").ToString();
                AdminHandler.UpdateLastLoginTime(Guid.Parse(id));
            }
        }


        [TestMethod]
        public void UpdatePassword()
        {
            Guid userid = Guid.Parse("86ecdaa6-9c84-41a1-aef8-f41e2cedf079");
            string password = "Esoteric$666";
            string passwordhash = CommonHelper.CreateMD5(password);

            string newpassword = "Esoteric$1";
            string newpasswordhash = CommonHelper.CreateMD5(newpassword);

            Assert.IsTrue(AdminHandler.UpdatePassword(userid, newpasswordhash));
        }
    }
}
