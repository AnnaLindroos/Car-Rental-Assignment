using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public int ID { get; init; }
    public string RegNo { get; init; }
    public Customer Customer { get; set; }
    public int KmRented { get; set; } 
    public int? KmReturned { get; set; } 
    public DateTime Rented { get; set; } 
    public DateTime? Returned { get; set; }
    public double? Cost { get; set; } = default;
    public BookingStatuses Status { get; set; }

    public Booking(int id, string regNo, Customer customer, int kmRented, int? kmReturned,
    DateTime rented, DateTime? returned, double? cost, BookingStatuses status)
    => (ID, RegNo, Customer, KmRented, KmReturned, Rented, Returned, Cost, Status)
    = (id, regNo, customer, kmRented, kmReturned, rented, returned, cost, status);
}
