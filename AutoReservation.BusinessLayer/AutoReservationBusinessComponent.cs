using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        public async Task<IEnumerable<Auto>> GetCars()
        {
            using ( var context = new AutoReservationEntities())
            {
                return await context.Autos.Include(a => a.Reservations).ToListAsync();

            }
        }

        public async Task<Auto> GetCar(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return await context.Autos.Where(a => a.Id == id).Include(e => e.Reservations).FirstOrDefaultAsync();
            }
        }

        public async Task<Auto> InsertCar(Auto car)
        {
            using (var context = new AutoReservationEntities())
            { 
                context.Autos.Add(car);
                await context.SaveChangesAsync();
                return car;
            }
        }

        public async Task<Auto> UpdateCar(Auto modified, Auto original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    await context.SaveChangesAsync();
                    return original;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    HandleDbConcurrencyException(context, original);
                    return null;
                }
            }

        }

        public async Task<Auto> DeleteCar(Auto car)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Attach(car);
                context.Autos.Remove(car);
                await context.SaveChangesAsync();
                return car;
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            using (var context = new AutoReservationEntities())
            {
                return await context.Reservations.Include(r => r.Kunde).Include(r => r.Auto).ToListAsync();
            }
        }

        public async Task<Reservation> GetReservation(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return await context.Reservations.Include(r => r.Auto).Include(r => r.Kunde).Where(r => r.ReservationNr == id).FirstOrDefaultAsync();
            }
        }

        public async Task<Reservation> InsertReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservations.Add(reservation);
                await context.SaveChangesAsync();
                return reservation;
            }
        }

        public async Task<Reservation> UpdateReservation(Reservation modified, Reservation original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservations.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    await context.SaveChangesAsync();
                    return original;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    HandleDbConcurrencyException(context, original);
                    return null;
                }
            }
            
        }

        public async Task<Reservation> DeleteReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservations.Attach(reservation);
                context.Reservations.Remove(reservation);
                await context.SaveChangesAsync();
                return reservation;

            }
        }

        public async Task<IEnumerable<Kunde>> GetCustomers()
        {
            using (var context = new AutoReservationEntities())
            {
                return await context.Kundes.Include(k => k.Reservations).ToListAsync();
            }
        }

        public async Task<Kunde> GetCustomer(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return await context.Kundes.Where(k => k.Id == id).Include(k => k.Reservations).FirstOrDefaultAsync();
            }
        }

        public async Task<Kunde> InsertCustomer(Kunde customer)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kundes.Add(customer);
                await context.SaveChangesAsync();
                return customer;
            }
        }

        public async Task<Kunde> UpdateCustomer(Kunde modified, Kunde original)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kundes.Attach(original);
                context.Entry(original).CurrentValues.SetValues(modified);
                try
                {
                    await context.SaveChangesAsync();
                    return original;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    HandleDbConcurrencyException(context, original);
                    return null;
                }
            }
        }

        public async Task<Kunde> DeleteCustomer(Kunde customer)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kundes.Attach(customer);
                context.Kundes.Remove(customer);
                await context.SaveChangesAsync();
                return customer;
            }
        }

        private static void HandleDbConcurrencyException<T>(DbContext context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>($"Update {typeof (T).Name}: Concurrency-Fehler", original);
        }
    }
}
