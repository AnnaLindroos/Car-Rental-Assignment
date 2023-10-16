using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    int ID { get; init; }
    string RegNo { get; init; }
    Customer Customer { get; set; }
    int KmRented { get; set; }
    int? KmReturned { get; set; }
    DateTime Rented { get; set; }
    DateTime? Returned { get; set; }
    double? Cost { get; set; }
    BookingStatuses Status { get; set; }
}