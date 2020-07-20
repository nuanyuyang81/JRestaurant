using System;
using JRestaurant.DAL.Admin;
using JRestaurant.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JRestaurant.Test
{
    [TestClass]
    public class TurnOverTypeTest
    {
        [TestMethod]
        public void AddTurnOverType()
        {
            TurnOverTypes type = new TurnOverTypes();
            type.Name = "KOL&招待";
            type.OwnerId = Guid.Parse("86ecdaa6-9c84-41a1-aef8-f41e2cedf079");
            type.AimType = Aim.Both;
            type.Comments = "KOL&招待";

            Assert.IsTrue(TurnOverTypesHanlder.AddTurnOverTypes(type));
        }

        [TestMethod]
        public void UpdateTurnOverType()
        {
            TurnOverTypes type = new TurnOverTypes();
            type.Id = 1;
            type.Name = "KOL&招待";
            type.OwnerId = Guid.Parse("86ecdaa6-9c84-41a1-aef8-f41e2cedf079");
            type.AimType = Aim.Both;
            type.Comments = "KOL&招待 Update";

            Assert.IsTrue(TurnOverTypesHanlder.UpdateTurnOverTypes(type));
        }
    }
}
