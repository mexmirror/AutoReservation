using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        Task<List<AutoDto>> GetCars();
        Task<AutoDto> GetCar(int id);
        void InsertCar(AutoDto car);
        void UpdateCar(AutoDto modified, AutoDto original);
        void DeleteCar(AutoDto car);

        Task<List<ReservationDto>> GetReservations();
        Task<ReservationDto> GetReservation(int id);
        void InsertReservation(ReservationDto reservation);
        void UpdateReservation(ReservationDto modified, ReservationDto original);
        void DeleteReservation(ReservationDto reservation);

        Task<List<KundeDto>> GetCustomers();
        Task<KundeDto> GetCustomer(int id);
        void InsertCustomer(KundeDto customer);
        void UpdateCustomer(KundeDto modified, KundeDto original);
        void DeleteCustomer(KundeDto customer);
    }
}
