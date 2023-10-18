using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using Car_Rental.Common.Classes;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;
using Car_Rental.Common.Extensions;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    private readonly List<IPerson> _persons = new List<IPerson>();
    private readonly List<Vehicle> _vehicles = new List<Vehicle>();
    private readonly List<IBooking> _bookings = new List<IBooking>();
    public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.ID) + 1;
    public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(b => b.ID) + 1;
    public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.ID) + 1;

    public IBooking? RentVehicle(int vehicleId, int customerId)
    {
        if (vehicleId <= 0 || customerId <= 0 || customerId == null)
            throw new ArgumentNullException();
    
        var vehicle = Single<Vehicle>(v => v.ID.Equals(vehicleId));
        var customer = Single<IPerson>(v => v.ID.Equals(customerId));
        vehicle.Status = VehicleStatuses.Booked;
        IBooking booking = new Booking(NextBookingId, vehicle, (Customer)customer, null, DateTime.Now.AddDays(-1), null, null, BookingStatuses.Open);

        if (booking == null)
            throw new ArgumentNullException();

        return booking;
    } 
    public IBooking ReturnVehicle(int vehicleId, int newDistance)
    {
        if (newDistance < 0)
            throw new ArgumentOutOfRangeException("Distance must be bigger than 0");

        var vehicleToReturn = Single<Vehicle>(v => v.ID.Equals(vehicleId));
        var booking = GetBookings().Single(i => i.Vehicle.RegNo.Equals(vehicleToReturn.RegNo) && i.Status == BookingStatuses.Open);
        booking.Returned = DateTime.Now;
        booking.KmReturned = booking.KmReturned = booking.Vehicle.Odometer + newDistance;
        booking.Cost = Math.Round((VehicleExtensions.Duration(booking.Rented, DateTime.Now) * vehicleToReturn.CostPerDay) + (newDistance * vehicleToReturn.CostKm), 2);
        booking.Status = BookingStatuses.Closed;
        vehicleToReturn.Status = VehicleStatuses.Available;
        return booking;
    }

    public List<T>? GetTypeAndFieldsList<T>()
    {
        List<T>? list = null;
        FieldInfo[] allFields = GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach (var field in allFields)
        {
            if (field.FieldType == typeof(List<T>))
            {
                list = field.GetValue(this) as List<T>;
                return list;
            }
        }
        return null;
    }
    public List<T> Get<T>(Expression<Func<T, bool>>? expression)
    {
        var list = GetTypeAndFieldsList<T>();

        if (list == null)
        {
            throw new ArgumentNullException("List you're trying to get is empty");
        }
        if (expression == null)
        {
            return list;
        }
        else
        {
            Func<T, bool> func = expression.Compile();
            return list.Where(v => func(v)).ToList();
        }
    }
    public void Add<T>(T item)
    {
        var list = GetTypeAndFieldsList<T>();

        if (list == null)
            throw new ArgumentNullException("List is empty");
        list.Add(item);
    }
    public T? Single<T>(Expression<Func<T, bool>>? expression)
    {
        var list = GetTypeAndFieldsList<T>();

        if (list == null)
        {
            throw new ArgumentNullException("List is empty");
        }
        if (expression == null)
        {
            throw new ArgumentNullException("Must enter a value to search for");
        }
        else
        {
            Func<T, bool> func = expression.Compile();
            return list.Single(v => func(v));
        }
    }
    public IEnumerable<IPerson> GetCustomers()
    {
        return Get<IPerson>(null);
    }
    public IEnumerable<IBooking> GetBookings()
    {
        return Get<IBooking>(null);
    }
    public IEnumerable<Vehicle> GetVehicles()
    {
        return Get<Vehicle>(null);
    }
    public CollectionData() => SeedData();
    private void SeedData()
    {
        _persons.Add(new Customer(NextPersonId, 12345, "Dal", "Sven"));
        _persons.Add(new Customer(NextPersonId, 98765, "Ljung", "Hans"));
        _vehicles.Add(new Car(NextVehicleId, "ABC123", "Volvo", 10000, 1, VehicleTypes.Combi, 200, VehicleStatuses.Available));
        _vehicles.Add(new Car(NextVehicleId, "DEF456", "Saab", 20000, 1, VehicleTypes.Sedan, 100, VehicleStatuses.Available));
        _vehicles.Add(new Car(NextVehicleId, "GHI789", "Tesla", 1000, 3, VehicleTypes.Sedan, 100, VehicleStatuses.Booked));
        _vehicles.Add(new Car(NextVehicleId, "JKL012", "Jeep", 5000, 1.5, VehicleTypes.Van, 300, VehicleStatuses.Available));
        _vehicles.Add(new Motorcycle(NextVehicleId, "MNO234", "Yamaha", 30000, 0.5, VehicleTypes.Motorcycle, VehicleStatuses.Available));
        _bookings.Add(new Booking(NextBookingId, _vehicles.First(i => i.RegNo == "GHI789"), (Customer)_persons.First(e => e.FirstName == "Sven"), null, DateTime.Now.Date.AddDays(-1), null, null, BookingStatuses.Open));
        _bookings.Add(new Booking(NextBookingId, _vehicles.First(i => i.RegNo == "JKL012"), (Customer)_persons.First(e => e.FirstName== "Hans"), 5000,  DateTime.Now.Date.AddDays(-1), DateTime.Now.Date, 300, BookingStatuses.Closed));
    }
}