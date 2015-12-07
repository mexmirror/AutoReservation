﻿using System;
using System.Threading.Tasks;
using AutoReservation.Dal;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public async Task Test_UpdateAuto()
        {
            const int newTarif = 7546;
            var modifiedCar = await Target.GetCar(1);
            var originalCar = await Target.GetCar(1);
            modifiedCar.Tagestarif = newTarif;
            Target.UpdateCar(modifiedCar, originalCar);
            var updatedCar = await Target.GetCar(1);
            Assert.AreEqual(newTarif, updatedCar.Tagestarif);

        }

        [TestMethod]
        public async Task Test_UpdateKunde()
        {
            const string newSurname = "Pascal";
            var modifiedCustomer = await Target.GetCustomer(1);
            var originalCustomer = await Target.GetCustomer(1);
            modifiedCustomer.Nachname = newSurname;
            Target.UpdateCustomer(modifiedCustomer, originalCustomer);
            var updatetCustomer = await Target.GetCustomer(1);
            Assert.AreEqual(newSurname, updatetCustomer.Nachname);
        }

        [TestMethod]
        public async Task Test_UpdateReservation()
        {
            var bis = DateTime.Now.AddDays(22);
            var modifiedReservation = await Target.GetReservation(1);
            var originalReservation = await Target.GetReservation(1);
            modifiedReservation.Bis = bis;
            Target.UpdateReservation(modifiedReservation, originalReservation);
            var updatetReservation = await Target.GetReservation(1);
            Assert.AreEqual(bis, updatetReservation.Bis);
        }
    }
}
