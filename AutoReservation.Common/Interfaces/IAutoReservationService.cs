using System.Collections.Generic;
using System.ComponentModel.Design;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        List<AutoDto> GetCars();
        AutoDto GetCar(int id);
        void InsertCar(AutoDto car);
        void UpdateCar(AutoDto modified, AutoDto original);
        void DeleteCar(AutoDto car);

        List<ReservationDto> GetReservations();
        ReservationDto GetReservation(int id);
        void InsertReservation(ReservationDto reservation);
        void UpdateReservation(ReservationDto modified, ReservationDto original);
        void DeleteReservation(ReservationDto reservation);

        List<KundeDto> GetCustomers();
        KundeDto GetCustomer(int id);
        void InsertCustomer(KundeDto customer);
        void UpdateCustomer(KundeDto modified, KundeDto original);
        void DeleteCustomer(KundeDto customer);
    }
}
