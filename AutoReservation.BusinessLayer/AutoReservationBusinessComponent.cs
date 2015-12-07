using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {

        public static Task<List<Auto>> GetCars()
        {
            using ( var context = new AutoReservationEntities())
            {
                return context.Autos.ToListAsync();

            }
        }

        public static Task<Auto> GetCar(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Autos.FindAsync(id);
            }
        }

        public static void InsertCar(Auto car)
        {
            using (var context = new AutoReservationEntities())
            { 
                context.Autos.Add(car);
                context.SaveChangesAsync();
            }
        }

        public static void UpdateCar(Auto modified, Auto original)
        {
            using (var context = new AutoReservationEntities())
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

        }

        public static void DeleteCar(Auto car)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Attach(car);
                context.Autos.Remove(car);
                context.SaveChangesAsync();
            }
        }

        public static Task<List<Reservation>> GetReservations()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Reservations.ToListAsync();
            }
        }

        public static Task<Reservation> GetReservation(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Reservations.FindAsync(id);
            }
        }

        public static void InsertReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservations.Add(reservation);
                context.SaveChangesAsync();
            }
        }

        public static void UpdateReservation(Reservation modified, Reservation original)
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

        public static void DeleteReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservations.Attach(reservation);
                context.Reservations.Remove(reservation);
                context.SaveChangesAsync();
            }
        }

        public static Task<List<Kunde>> GetCustomers()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Kundes.ToListAsync();
            }
        }

        public static Task<Kunde> GetCustomer(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Kundes.FindAsync(id);
            }
        }

        public static void InsertCustomer(Kunde customer)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kundes.Add(customer);
                context.SaveChangesAsync();
            }
        }

        public static void UpdateCustomer(Kunde modified, Kunde original)
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

        public static void DeleteCustomer(Kunde customer)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kundes.Attach(customer);
                context.Kundes.Remove(customer);
                context.SaveChangesAsync();
            }
        }

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>($"Update {typeof (T).Name}: Concurrency-Fehler", original);
        }
    }
}
