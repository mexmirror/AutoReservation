using System.Collections.Generic;
using System.Threading.Tasks;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService: IAutoReservationService
    {
        private AutoReservationBusinessComponent repo = new AutoReservationBusinessComponent();
        public async Task<List<AutoDto>> GetCars()
        {
            var cars = await repo.GetCars();
            return cars.ConvertToDtos();
        }

        public async Task<AutoDto> GetCar(int id)
        {
            var car = await repo.GetCar(id);
            return car.ConvertToDto();
        }

        public void InsertCar(AutoDto car)
        {
            repo.InsertCar(car.ConvertToEntity());
        }

        public void UpdateCar(AutoDto modified, AutoDto original)
        {
            repo.UpdateCar(modified.ConvertToEntity(), original.ConvertToEntity());
        }

        public void DeleteCar(AutoDto car)
        {
            repo.DeleteCar(car.ConvertToEntity());
        }

        public async Task<List<ReservationDto>> GetReservations()
        {
            var reservations = await repo.GetReservations();
            return reservations.ConvertToDtos();
        }

        public async Task<ReservationDto> GetReservation(int id)
        {
            var reservation = await repo.GetReservation(id);
            return reservation.ConvertToDto();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            repo.InsertReservation(reservation.ConvertToEntity());
        }

        public void UpdateReservation(ReservationDto modified, ReservationDto original)
        {
            repo.UpdateReservation(modified.ConvertToEntity(), original.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            repo.DeleteReservation(reservation.ConvertToEntity());
        }

        public async Task<List<KundeDto>> GetCustomers()
        {
            var customers = await repo.GetCustomers();
            return customers.ConvertToDtos();
        }

        public async Task<KundeDto> GetCustomer(int id)
        {
            var customer = await repo.GetCustomer(id);
            return customer.ConvertToDto();
        }

        public void InsertCustomer(KundeDto customer)
        {
            repo.InsertCustomer(customer.ConvertToEntity());
        }

        public void UpdateCustomer(KundeDto modified, KundeDto original)
        {
            repo.UpdateCustomer(modified.ConvertToEntity(), original.ConvertToEntity());
        }

        public void DeleteCustomer(KundeDto customer)
        {
            repo.DeleteCustomer(customer.ConvertToEntity());
        }
    }
}