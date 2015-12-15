using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService: IAutoReservationService
    {

        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }

        public async Task<List<AutoDto>> GetCars()
        {
            var cars = await AutoReservationBusinessComponent.GetCars();
            return cars.ConvertToDtos();
        }

        public async Task<AutoDto> GetCar(int id)
        {
            var car = await AutoReservationBusinessComponent.GetCar(id);
            return car.ConvertToDto();
        }

        public void InsertCar(AutoDto car)
        {
            AutoReservationBusinessComponent.InsertCar(car.ConvertToEntity());
        }

        public void UpdateCar(AutoDto modified, AutoDto original)
        {
            AutoReservationBusinessComponent.UpdateCar(modified.ConvertToEntity(), original.ConvertToEntity());
        }

        public void DeleteCar(AutoDto car)
        {
            AutoReservationBusinessComponent.DeleteCar(car.ConvertToEntity());
        }

        public async Task<List<ReservationDto>> GetReservations()
        {
            var reservations = await AutoReservationBusinessComponent.GetReservations();
            return reservations.ConvertToDtos();
        }

        public async Task<ReservationDto> GetReservation(int id)
        {
            var reservation = await AutoReservationBusinessComponent.GetReservation(id);
            return reservation.ConvertToDto();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            AutoReservationBusinessComponent.InsertReservation(reservation.ConvertToEntity());
        }

        public void UpdateReservation(ReservationDto modified, ReservationDto original)
        {
            AutoReservationBusinessComponent.UpdateReservation(modified.ConvertToEntity(), original.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            AutoReservationBusinessComponent.DeleteReservation(reservation.ConvertToEntity());
        }

        public async Task<List<KundeDto>> GetCustomers()
        {
            var customers = await AutoReservationBusinessComponent.GetCustomers();
            return customers.ConvertToDtos();
        }

        public async Task<KundeDto> GetCustomer(int id)
        {
            var customer = await AutoReservationBusinessComponent.GetCustomer(id);
            return customer.ConvertToDto();
        }

        public void InsertCustomer(KundeDto customer)
        {
            AutoReservationBusinessComponent.InsertCustomer(customer.ConvertToEntity());
        }

        public void UpdateCustomer(KundeDto modified, KundeDto original)
        {
            AutoReservationBusinessComponent.UpdateCustomer(modified.ConvertToEntity(), original.ConvertToEntity());
        }

        public void DeleteCustomer(KundeDto customer)
        {
            AutoReservationBusinessComponent.DeleteCustomer(customer.ConvertToEntity());
        }
    }
}