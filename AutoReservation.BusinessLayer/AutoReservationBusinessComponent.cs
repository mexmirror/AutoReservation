using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        public static Task<List<Auto>> GetCars(AutoReservationEntities context)
        {
            return context.Autoes.ToListAsync();
        }

        public static Task<Auto> GetCar(AutoReservationEntities context, int id)
        {
            return context.Autoes.FindAsync(id);
        }

        public static void InsertCar(AutoReservationEntities context, Auto car)
        {
            context.Autoes.Add(car);
            context.SaveChangesAsync();
        }

        public static void UpdateCar(AutoReservationEntities context, Auto modified, Auto original)
        {
            context.Autoes.Attach(modified);
            try
            {
                context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                HandleDbConcurrencyException(context, original);
            }
        }

        public static void DeleteCar(AutoReservationEntities context, Auto car)
        {
            context.Autoes.Attach(car);
            context.Autoes.Remove(car);
            context.SaveChangesAsync();
        }

        public static Task<List<Reservation>> GetReservations(AutoReservationEntities context)
        {
            return context.Reservations.ToListAsync();
        }

        public static Task<Reservation> GetReservation(AutoReservationEntities context, int id)
        {
            return context.Reservations.FindAsync(id);
        }

        public void InsertReservation(AutoReservationEntities context, Reservation reservation)
        {
            context.Reservations.Add(reservation);
            context.SaveChangesAsync();
        }

        public void UpdateReservation(AutoReservationEntities context, Reservation modified, Reservation original)
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

        public void DeleteReservation(AutoReservationEntities context, Reservation reservation)
        {
            context.Reservations.Attach(reservation);
            context.Reservations.Remove(reservation);
            context.SaveChangesAsync();
        }

        public static Task<List<Kunde>> GetCustomers(AutoReservationEntities context)
        {
            return context.Kundes.ToListAsync();
        }

        public static Task<Kunde> GetCustomer(AutoReservationEntities context, int id)
        {
            return context.Kundes.FindAsync(id);
        }

        public static void InsertCustomer(AutoReservationEntities context, Kunde customer)
        {
            context.Kundes.Add(customer);
            context.SaveChangesAsync();
        }

        public static void UpdateCustomer(AutoReservationEntities context, Kunde modified, Kunde original)
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

        public static void DeleteCustomer(AutoReservationEntities context, Kunde customer)
        {
            context.Kundes.Attach(customer);
            context.Kundes.Remove(customer);
            context.SaveChangesAsync();
        }

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>($"Update {typeof (T).Name}: Concurrency-Fehler", original);
        }
    }
}