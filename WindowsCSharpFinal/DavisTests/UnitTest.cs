using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using WindowsCSharpFinal.ViewModels;
using WindowsCSharpFinal.Models;
using WindowsCSharpFinal.Views;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;

namespace DavisTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateAPartner()
        {
            Partner partner = new Partner();
            Assert.AreNotEqual(null, partner);
        }

        [TestMethod]
        public void CreateAPartnerAndSetHappinessToOneHundred()
        {
            int expected = 100;
            Partner partner = new Partner();
            partner.Happiness = 100;
            Assert.AreEqual(expected, partner.Happiness);
        }

        [TestMethod]
        public void CreateAUser()
        {
            User user = new User();
            Assert.AreNotEqual(null, user);
        }

        [TestMethod]
        public void CreateAUserAndSetMoneyToOneThousand()
        {
            int expected = 1000;
            User user = new User();
            user.Money = 1000;
            Assert.AreEqual(expected, user.Money);
        }
    }
}
