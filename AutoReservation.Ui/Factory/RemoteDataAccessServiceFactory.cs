using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Ui.Factory
{
    class RemoteDataAccessServiceFactory: IServiceFactory
    {
        public IAutoReservationService GetService()
        {
            var bind = new NetTcpBinding();
            var addr = new EndpointAddress("net.tcp://localhost:7876/AutoReservationService");
            var factory = new ChannelFactory<IAutoReservationService>(bind, addr);

            return factory.CreateChannel();
        }
    }
}
