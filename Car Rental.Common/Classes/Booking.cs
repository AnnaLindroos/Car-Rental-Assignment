using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public int ID { get; init; }
    public Vehicle Vehicle { get; init; }
    public Customer Customer { get; set; }
    public int? KmReturned { get; set; } 
    public DateTime Rented { get; set; } 
    public DateTime? Returned { get; set; }
    public double? Cost { get; set; } = default;
    public BookingStatuses Status { get; set; }

    public Booking(int id, Vehicle vehicle, Customer customer, int? kmReturned,
    DateTime rented, DateTime? returned, double? cost, BookingStatuses status)
    => (ID, Vehicle, Customer, KmReturned, Rented, Returned, Cost, Status)
    = (id, vehicle, customer, kmReturned, rented, returned, cost, status);
}
