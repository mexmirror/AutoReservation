using System.Threading.Tasks;
using AutoReservation.TestEnvironment;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Ui.Factory;
using Ninject;

namespace AutoReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        private IKernel _kernel;

        [TestInitialize]
        public void InitializeTestData()
        {
            _kernel = new StandardKernel();
            _kernel.Load("Dependencies.Ninject.xml");

            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public async Task Test_AutosLoad()
        {
            var vm = new AutoViewModel(_kernel.Get<IServiceFactory>());
            await vm.Init();
            Assert.AreEqual(3, vm.Autos.Count);
        }

        [TestMethod]
        public async Task Test_KundenLoad()
        {
            var vm = new KundeViewModel(_kernel.Get<IServiceFactory>());
            await vm.Init();
            Assert.AreEqual(4, vm.Kunden.Count);
        }

        [TestMethod]
        public async Task Test_ReservationenLoad()
        {
            var vm = new ReservationViewModel(_kernel.Get<IServiceFactory>());
            await vm.Init();
            Assert.AreEqual(3, vm.Reservationen.Count);
        }
    }
}