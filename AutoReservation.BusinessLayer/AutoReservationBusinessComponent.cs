using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {

        public Task<List<Auto>> GetCars()
        {
            using ( var context = new AutoReservationEntities())
            {
                return context.Autos.ToListAsync();

            }
        }

        public Task<Auto> GetCar(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Autos.FindAsync(id);
            }
        }

        public void InsertCar(Auto car)
        {
            using (var context = new AutoReservationEntities())
            { 
                context.Autos.Add(car);
                context.SaveChangesAsync();
            }
        }

        public void UpdateCar(Auto modified, Auto original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Attach(modified);
                try
                {
                    context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }

        }

        public void DeleteCar(Auto car)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Attach(car);
                context.Autos.Remove(car);
                context.SaveChangesAsync();
            }
        }

        public Task<List<Reservation>> GetReservations()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Reservations.ToListAsync();
            }
        }

        public Task<Reservation> GetReservation(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Reservations.FindAsync(id);
            }
        }

        public void InsertReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservations.Add(reservation);
                context.SaveChangesAsync();
            }
        }

        public void UpdateReservation(Reservation modified, Reservation original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservations.Attach(modified);
                try
                {
                    context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
            
        }

        public void DeleteReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservations.Attach(reservation);
                context.Reservations.Remove(reservation);
                context.SaveChangesAsync();
            }
        }

        public Task<List<Kunde>> GetCustomers()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Kundes.ToListAsync();
            }
        }

        public Task<Kunde> GetCustomer(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Kundes.FindAsync(id);
            }
        }

        public void InsertCustomer(Kunde customer)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kundes.Add(customer);
                context.SaveChangesAsync();
            }
        }

        public void UpdateCustomer(Kunde modified, Kunde original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kundes.Attach(modified);
                try
                {
                    context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public void DeleteCustomer(Kunde customer)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kundes.Attach(customer);
                context.Kundes.Remove(customer);
                context.SaveChangesAsync();
            }
        }

        private void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>($"Update {typeof (T).Name}: Concurrency-Fehler", original);
        }
    }
}
