using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    public int ID { get; init; }
    public Vehicle Vehicle { get; init; }
    public Customer Customer { get; set; }
    public int? KmReturned { get; set; }
    public DateTime Rented { get; set; }
    public DateTime? Returned { get; set; }
    public double? Cost { get; set; }
    public BookingStatuses Status { get; set; }

}