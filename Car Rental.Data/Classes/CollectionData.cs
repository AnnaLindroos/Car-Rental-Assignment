using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using Car_Rental.Common;
using Car_Rental.Common.Classes;
using System.Linq;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> _persons = new List<IPerson>();
    readonly List<IVehicle> _vehicles = new List<IVehicle>();
    readonly List<IBooking> _bookings = new List<IBooking>();

    public CollectionData() => SeedData();

    void SeedData()
    {
        _persons.Add(new Customer(12345, "Doe", "John"));
        _persons.Add(new Customer(98765, "Doe", "Jane"));
        _vehicles.Add(new Car("ABC123", "Volvo", 10000, 1, VehicleTypes.Combi, 200, VehicleStatuses.Available));
        _vehicles.Add(new Car("DEF456", "Saab", 20000, 1, VehicleTypes.Sedan, 100, VehicleStatuses.Available));
        _vehicles.Add(new Car("GHI789", "Tesla", 1000, 3, VehicleTypes.Sedan, 100, VehicleStatuses.Booked));
        _vehicles.Add(new Car("JKL012", "Jeep", 5000, 1.5, VehicleTypes.Van, 300, VehicleStatuses.Available));
        _vehicles.Add(new Motorcycle("MNO234", "Yamaha", 30000, 0.5, 50, VehicleStatuses.Available));
        _bookings.Add(new Booking("GHI789", (Customer)_persons.First(e => e.FirstName == "John"), 1000, null, DateTime.Now.Date, null, null, BookingStatuses.Open));
        _bookings.Add(new Booking("JKL012", (Customer)_persons.First(e => e.FirstName == "Jane"), 5000, 5000, DateTime.Now.Date.AddDays(-1), DateTime.Now.Date, 300, BookingStatuses.Closed));
    }

    public IEnumerable<IPerson> GetPersons() => _persons;
    public IEnumerable<IBooking> GetBookings() => _bookings;
    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default) => _vehicles;
}