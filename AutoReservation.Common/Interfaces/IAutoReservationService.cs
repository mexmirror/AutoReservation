using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        [OperationContract]
        Task<List<AutoDto>> GetCars();
        [OperationContract]
        Task<AutoDto> GetCar(int id);
        [OperationContract]
        void InsertCar(AutoDto car);
        [OperationContract]
        void UpdateCar(AutoDto modified, AutoDto original);
        [OperationContract]
        void DeleteCar(AutoDto car);

        [OperationContract]
        Task<List<ReservationDto>> GetReservations();
        [OperationContract]
        Task<ReservationDto> GetReservation(int id);
        [OperationContract]
        void InsertReservation(ReservationDto reservation);
        [OperationContract]
        void UpdateReservation(ReservationDto modified, ReservationDto original);
        [OperationContract]
        void DeleteReservation(ReservationDto reservation);

        [OperationContract]
        Task<List<KundeDto>> GetCustomers();
        [OperationContract]
        Task<KundeDto> GetCustomer(int id);
        [OperationContract]
        void InsertCustomer(KundeDto customer);
        [OperationContract]
        void UpdateCustomer(KundeDto modified, KundeDto original);
        [OperationContract]
        void DeleteCustomer(KundeDto customer);
    }
}
