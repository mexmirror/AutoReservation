using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public async Task Test_GetAutos()
        {
            var cars = await Target.GetCars();
            Assert.AreEqual(3,  cars.Count());
        }

        [TestMethod]
        public async Task Test_GetKunden()
        {
            var kunden = await Target.GetCustomers();
            Assert.AreEqual(4, kunden.Count());
        }

        [TestMethod]
        public async Task Test_GetReservationen()
        {
            var reservations = await Target.GetReservations();
            Assert.AreEqual(3, reservations.Count());
        }

        [TestMethod]
        public async Task Test_GetAutoById()
        {
            var auto = await Target.GetCar(2);
            Assert.AreEqual("VW Golf", auto.Marke);
            Assert.AreEqual(AutoKlasse.Mittelklasse, auto.AutoKlasse);
            Assert.AreEqual(120, auto.Tagestarif);
        }

        [TestMethod]
        public async Task Test_GetKundeById()
        {
            var kunde = await Target.GetCustomer(3);
            Assert.AreEqual("Pfahl", kunde.Nachname);
            Assert.AreEqual("Martha", kunde.Vorname);
            Assert.AreEqual("03.07.1950", kunde.Geburtsdatum.ToShortDateString());
        }

        [TestMethod]
        public async Task Test_GetReservationByNr()
        {
            var reservation = await Target.GetReservation(1);

            Assert.AreEqual("10.01.2020", reservation.Von.ToShortDateString());
            Assert.AreEqual("20.01.2020", reservation.Bis.ToShortDateString());

            var auto = await Target.GetCar(reservation.Auto.Id);
            Assert.AreEqual(auto.Marke, reservation.Auto.Marke);
            Assert.AreEqual(auto.AutoKlasse, reservation.Auto.AutoKlasse);
            Assert.AreEqual(auto.Tagestarif, reservation.Auto.Tagestarif);

            var kunde = await Target.GetCustomer(reservation.Kunde.Id);
            Assert.AreEqual(kunde.Nachname, reservation.Kunde.Nachname);
            Assert.AreEqual(kunde.Vorname, reservation.Kunde.Vorname);
            Assert.AreEqual(kunde.Geburtsdatum.ToShortDateString(), reservation.Kunde.Geburtsdatum.ToShortDateString());
        }

        [TestMethod]
        public async Task Test_GetReservationByIllegalNr()
        {
            var reservation = await Target.GetReservation(-1);
            Assert.AreEqual(null, reservation);
        }

        [TestMethod]
        public async Task Test_InsertAuto()
        {
            var newAuto = new AutoDto
            {
                Basistarif = 200,
                Marke = "Ferrari",
                Tagestarif = 2000,
                AutoKlasse = AutoKlasse.Luxusklasse
            };
            var temp = await Target.InsertCar(newAuto);

            var auto = await Target.GetCar(temp.Id);
            Assert.AreEqual("Ferrari", auto.Marke);
            Assert.AreEqual(AutoKlasse.Luxusklasse, auto.AutoKlasse);
            Assert.AreEqual(2000, auto.Tagestarif);
            Assert.AreEqual(200, auto.Basistarif);
        }

        [TestMethod]
        public async Task Test_InsertKunde()
        {
            var newCustomer = new KundeDto
            {
                Nachname = "Otto",
                Vorname = "Hans",
                Geburtsdatum = DateTime.Now

            };
            var temp = await Target.InsertCustomer(newCustomer);
            var customer = await Target.GetCustomer(temp.Id);
            Assert.AreEqual(newCustomer.Nachname, customer.Nachname);
            Assert.AreEqual(newCustomer.Vorname, customer.Vorname);
        }

        [TestMethod]
        public async Task Test_InsertReservation()
        {
            var tempAuto = await Target.GetCar(2);
            var tempKunde = await Target.GetCustomer(3);

            ReservationDto newReservation = new ReservationDto()
            {
                Auto = tempAuto,
                Kunde = tempKunde,
                Bis = DateTime.Now.AddMonths(2),
                Von = DateTime.Now.AddMonths(1)
            };

            var temp = await Target.InsertReservation(newReservation);
            var reservation = await Target.GetReservation(temp.ReservationNr);

            Assert.AreEqual(newReservation.Von.ToShortDateString(), reservation.Von.ToShortDateString());
            Assert.AreEqual(newReservation.Bis.ToShortDateString(), reservation.Bis.ToShortDateString());

            Assert.AreEqual(tempAuto.Marke, reservation.Auto.Marke);
            Assert.AreEqual(tempAuto.AutoKlasse, reservation.Auto.AutoKlasse);
            Assert.AreEqual(tempAuto.Tagestarif, reservation.Auto.Tagestarif);

            Assert.AreEqual(tempKunde.Nachname, reservation.Kunde.Nachname);
            Assert.AreEqual(tempKunde.Vorname, reservation.Kunde.Vorname);
            Assert.AreEqual(tempKunde.Geburtsdatum.ToShortDateString(), reservation.Kunde.Geburtsdatum.ToShortDateString());
        }

        [TestMethod]
        public async Task Test_UpdateAuto()
        {
            var modified = await Target.GetCar(1);
            var original = await Target.GetCar(1);
            modified.Basistarif = 4000;
            await Target.UpdateCar(modified, original);
            var updatedCar = await Target.GetCar(1);
            Assert.AreEqual(modified.Tagestarif, updatedCar.Tagestarif);
        }

        [TestMethod]
        public async Task Test_UpdateKunde()
        {
            var modified = await Target.GetCustomer(1);
            var original = await Target.GetCustomer(1);
            modified.Nachname = "HansGuckInDieLuft";
            await Target.UpdateCustomer(modified, original);
            var updatedCustomer = await Target.GetCustomer(1);
            Assert.AreEqual(modified.Nachname, updatedCustomer.Nachname);
        }

        [TestMethod]
        public async Task Test_UpdateReservation()
        {
            var modified = await Target.GetReservation(1);
            var original = await Target.GetReservation(1);
            modified.Bis = DateTime.MaxValue;
            await Target.UpdateReservation(modified, original);
            var updatedReservation = await Target.GetReservation(1);
            Assert.AreEqual(modified.Bis.Date, updatedReservation.Bis.Date);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoDto>))]
        public async Task Test_UpdateAutoWithOptimisticConcurrency()
        {
            var orgAuto1 = await Target.GetCar(1);
            var auto1 = await Target.GetCar(1);
            var orgAuto2 = await Target.GetCar(1);
            var auto2 = await Target.GetCar(1);

            auto1.Marke = "Fiat";
            await Target.UpdateCar(auto1, orgAuto1);

            auto2.Marke = "Lamborghini";
            await Target.UpdateCar(auto2, orgAuto2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<KundeDto>))]
        public async Task Test_UpdateKundeWithOptimisticConcurrency()
        {
            var orgKunde1 = await Target.GetCustomer(1);
            var kunde1 = await Target.GetCustomer(1);
            var orgKunde2 = await Target.GetCustomer(1);
            var kunde2 = await Target.GetCustomer(1);

            kunde1.Vorname = "Max";

            kunde2.Vorname = "Moritz";
            await Target.UpdateCustomer(kunde1, orgKunde1);

            await Target.UpdateCustomer(kunde2, orgKunde2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ReservationDto>))]
        public async Task Test_UpdateReservationWithOptimisticConcurrency()
        {
            var orgReservation1 = await Target.GetReservation(1);
            var reservation1 = await Target.GetReservation(1);
            var orgReservation2 = await Target.GetReservation(1);
            var reservation2 = await Target.GetReservation(1);

            reservation1.Auto = await Target.GetCar(2);
            await Target.UpdateReservation(reservation1, orgReservation1);

            reservation2.Auto = await Target.GetCar(3);
            await Target.UpdateReservation(reservation2, orgReservation2);

        }

        [TestMethod]
        public async Task Test_DeleteKunde()
        {
            await Target.DeleteCustomer(await Target.GetCustomer(1));
            Assert.AreEqual(3, (await Target.GetCustomers()).Count());
        }

        [TestMethod]
        public async Task Test_DeleteAuto()
        {
            await Target.DeleteCar(await Target.GetCar(1));
            Assert.AreEqual(2, (await Target.GetCars()).Count());
        }

        [TestMethod]
        public async Task Test_DeleteReservation()
        {
            await Target.DeleteReservation(await Target.GetReservation(1));
            Assert.AreEqual(2, (await Target.GetReservations()).Count());
        }
    }
}
