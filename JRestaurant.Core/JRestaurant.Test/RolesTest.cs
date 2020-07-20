using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JRestaurant.Test
{
    /// <summary>
    /// RolesTest 的摘要说明
    /// </summary>
    [TestClass]
    public class RolesTest
    {

        [TestMethod]
        public void AddRoles()
        {
            Roles role = new Roles();
            role.Key = "admin";
            role.Name = "系统管理员";
            role.OwnerId = Guid.NewGuid();
            role.Comments = "系统超级管理员";

            Assert.IsTrue(RoleHandler.AddRole(role));
        }
    }
}
