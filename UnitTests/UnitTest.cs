using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPagesContacts.Pages;
using SailingWeb;
using SailingWeb.Data;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void EnterRace()
        {
            CreateModel cm = new CreateModel();

            var boat = new BoatsTidy("Luke Stanislaus", "Laser", "162872", "", 0, "");
            cm.Boats = boat;
            cm.Boatandnumber = "Laser, 162872";

            var str = "2nd evening Series Race 9abc1232018-08-08 19:00:00";
            cm.Race = str;
            Assert.IsTrue(cm.OnPost().IsCompletedSuccessfully);
            var str1 = str.Split("abc123");
            Calendar cal = new Calendar(str1[0], "", Convert.ToDateTime(str1[1]));



            Action act = new Action(Act);
            Assert.IsFalse(Assert.ThrowsException<Exception>(act) != new Exception());
            
        }
        static void Act()
        {
            var boat = new BoatsTidy("Luke Stanislaus", "Laser", "162872", "", 0, "");
            var str1 = "2nd evening Series Race 9abc1232018-08-08 19:00:00".Split("abc123");
            Calendar cal = new Calendar(str1[0], "", Convert.ToDateTime(str1[1]));

            Sql.RemoveBoats(boat, cal);

        }
    }
}
