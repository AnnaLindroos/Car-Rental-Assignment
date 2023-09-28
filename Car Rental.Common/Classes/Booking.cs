using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public string RegNo { get; init; }
    public Customer Customer { get; set; }
    public int KmRented { get; set; } 
    public int? KmReturned { get; set; } 
    public DateTime Rented { get; set; } 
    public DateTime? Returned { get; set; }
    public double? Cost { get; set; } = default;
    public BookingStatuses Status { get; set; }

    public Booking(string regNo, Customer customer, int kmRented, int? kmReturned,
    DateTime rented, DateTime? returned, double? cost, BookingStatuses status)
    => (RegNo, Customer, KmRented, KmReturned, Rented, Returned, Cost, Status)
    = (regNo, customer, kmRented, kmReturned, rented, returned, cost, status);

    public void ReturnVehicle(IVehicle vehicle, int km)
    {
        var dateRented = DateTime.Now.AddDays(-1);
        var dateReturned = DateTime.Now;
        var daysRented = (dateReturned - dateRented).TotalDays;

        KmRented = vehicle.Odometer;
        KmReturned = KmRented + km;
        Returned = dateReturned;
        Rented = dateRented;
        Cost = GetCost(daysRented, vehicle, km);
        Status = BookingStatuses.Closed;
        if (KmReturned != null) vehicle.Odometer = (int)KmReturned; 
        vehicle.Status = VehicleStatuses.Available;
    }

    public void RentVehicle(IVehicle vehicle)
    {
        Rented = DateTime.Now;
        KmRented = vehicle.Odometer;

    }

    public double GetCost(double daysRented, IVehicle vehicle, int km)
        => Math.Round(daysRented * vehicle.CostPerDay + (km * vehicle.CostKm), 2);
}
