using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        private static readonly AutoReservationEntities _context = new AutoReservationEntities();

        public static Task<List<Auto>> GetCars()
        {
            return _context.Autoes.ToListAsync();
        }

        public static Task<Auto> GetCar(int id)
        {
            return _context.Autoes.FindAsync(id);
        }

        public static void InsertCar(Auto car)
        {
            _context.Autoes.Add(car);
            _context.SaveChangesAsync();
        }

        public static void UpdateCar(Auto modified, Auto original)
        {
            _context.Autoes.Attach(modified);
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                HandleDbConcurrencyException(_context, original);
            }
        }

        public static void DeleteCar(Auto car)
        {
            _context.Autoes.Attach(car);
            _context.Autoes.Remove(car);
            _context.SaveChangesAsync();
        }

        public static Task<List<Reservation>> GetReservations()
        {
            return _context.Reservations.ToListAsync();
        }

        public static Task<Reservation> GetReservation(int id)
        {
            return _context.Reservations.FindAsync(id);
        }

        public static void InsertReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChangesAsync();
        }

        public static void UpdateReservation(Reservation modified, Reservation original)
        {
            _context.Reservations.Attach(modified);
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                HandleDbConcurrencyException(_context, original);
            }
        }

        public static void DeleteReservation(Reservation reservation)
        {
            _context.Reservations.Attach(reservation);
            _context.Reservations.Remove(reservation);
            _context.SaveChangesAsync();
        }

        public static Task<List<Kunde>> GetCustomers()
        {
            return _context.Kundes.ToListAsync();
        }

        public static Task<Kunde> GetCustomer(int id)
        {
            return _context.Kundes.FindAsync(id);
        }

        public static void InsertCustomer(Kunde customer)
        {
            _context.Kundes.Add(customer);
            _context.SaveChangesAsync();
        }

        public static void UpdateCustomer(Kunde modified, Kunde original)
        {
            _context.Kundes.Attach(modified);
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                HandleDbConcurrencyException(_context, original);
            }

        }

        public static void DeleteCustomer(Kunde customer)
        {
            _context.Kundes.Attach(customer);
            _context.Kundes.Remove(customer);
            _context.SaveChangesAsync();
        }

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>($"Update {typeof (T).Name}: Concurrency-Fehler", original);
        }
    }
}