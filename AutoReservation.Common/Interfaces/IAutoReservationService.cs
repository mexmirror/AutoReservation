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
        Task<AutoDto> InsertCar(AutoDto car);
        [OperationContract]
        [FaultContract(typeof(AutoDto))]
        Task<AutoDto> UpdateCar(AutoDto modified, AutoDto original);
        [OperationContract]
        Task<AutoDto> DeleteCar(AutoDto car);

        [OperationContract]
        Task<List<ReservationDto>> GetReservations();
        [OperationContract]
        Task<ReservationDto> GetReservation(int id);
        [OperationContract]
        Task<ReservationDto> InsertReservation(ReservationDto reservation);
        [OperationContract]
        [FaultContract(typeof(ReservationDto))]
        Task<ReservationDto> UpdateReservation(ReservationDto modified, ReservationDto original);
        [OperationContract]
        Task<ReservationDto> DeleteReservation(ReservationDto reservation);

        [OperationContract]
        Task<List<KundeDto>> GetCustomers();
        [OperationContract]
        Task<KundeDto> GetCustomer(int id);
        [OperationContract]
        Task<KundeDto> InsertCustomer(KundeDto customer);
        [OperationContract]
        [FaultContract(typeof(KundeDto))]
        Task<KundeDto> UpdateCustomer(KundeDto modified, KundeDto original);
        [OperationContract]
        Task<KundeDto> DeleteCustomer(KundeDto customer);
    }
}
