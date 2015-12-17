using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService: IAutoReservationService
    {
        private readonly AutoReservationBusinessComponent _repo = new AutoReservationBusinessComponent();
        public async Task<IEnumerable<AutoDto>> GetCars()
        {
            var cars = await _repo.GetCars();
            return cars.ConvertToDtos();
        }

        public async Task<AutoDto> GetCar(int id)
        {
            var car = await _repo.GetCar(id);
            return car.ConvertToDto();
        }

        public async Task<AutoDto> InsertCar(AutoDto car)
        {
            return (await _repo.InsertCar(car.ConvertToEntity())).ConvertToDto();
        }

        public async Task<AutoDto> UpdateCar(AutoDto modified, AutoDto original)
        {
            try
            {
                return (await _repo.UpdateCar(modified.ConvertToEntity(), original.ConvertToEntity())).ConvertToDto();

            }
            catch (LocalOptimisticConcurrencyException<Auto> e)
            {
                throw new FaultException<AutoDto>(modified);
            }
        }

        public async Task<AutoDto> DeleteCar(AutoDto car)
        {
            return (await _repo.DeleteCar(car.ConvertToEntity())).ConvertToDto();
        }

        public async Task<IEnumerable<ReservationDto>> GetReservations()
        {
            var reservations = await _repo.GetReservations();
            return reservations.ConvertToDtos();
        }

        public async Task<ReservationDto> GetReservation(int id)
        {
            var reservation = await _repo.GetReservation(id);
            return reservation.ConvertToDto();
        }

        public async Task<ReservationDto> InsertReservation(ReservationDto reservation)
        {
            return (await _repo.InsertReservation(reservation.ConvertToEntity())).ConvertToDto();
        }

        public async Task<ReservationDto> UpdateReservation(ReservationDto modified, ReservationDto original)
        {
            try
            {
                return
                    (await _repo.UpdateReservation(modified.ConvertToEntity(), original.ConvertToEntity())).ConvertToDto();

            }
            catch (LocalOptimisticConcurrencyException<Reservation> e)
            {
                throw new FaultException<ReservationDto>(modified);
            }
        }

        public async Task<ReservationDto> DeleteReservation(ReservationDto reservation)
        {
            return (await _repo.DeleteReservation(reservation.ConvertToEntity())).ConvertToDto();
        }

        public async Task<IEnumerable<KundeDto>> GetCustomers()
        {
            var customers = await _repo.GetCustomers();
            return customers.ConvertToDtos();
        }

        public async Task<KundeDto> GetCustomer(int id)
        {
            var customer = await _repo.GetCustomer(id);
            return customer.ConvertToDto();
        }

        public async Task<KundeDto> InsertCustomer(KundeDto customer)
        {
            return (await _repo.InsertCustomer(customer.ConvertToEntity())).ConvertToDto();
        }

        public async Task<KundeDto> UpdateCustomer(KundeDto modified, KundeDto original)
        {
            try
            {
                return
                    (await _repo.UpdateCustomer(modified.ConvertToEntity(), original.ConvertToEntity())).ConvertToDto();
            }
            catch (LocalOptimisticConcurrencyException<Kunde> e)
            {
                throw new FaultException<KundeDto>(modified);
            }
        }

        public async Task<KundeDto> DeleteCustomer(KundeDto customer)
        {
            return (await _repo.DeleteCustomer(customer.ConvertToEntity())).ConvertToDto();
        }
    }
}