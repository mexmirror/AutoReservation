using AutoReservation.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public class ServiceTestRemote : ServiceTestBase
    {
        private static ServiceHost _serviceHost;
        
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _serviceHost = new ServiceHost(typeof(AutoReservationService));
            _serviceHost.Open();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            if (_serviceHost.State != CommunicationState.Closed)
                _serviceHost.Close();
        }

        private IAutoReservationService _target;
        protected override IAutoReservationService Target
        {
            get
            {
                if (_target == null)
                {
                    ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                    _target = channelFactory.CreateChannel();
                }
                return _target;
            }
        }

    }
}