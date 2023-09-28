using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    string RegNo { get; init; }
    Customer Customer { get; set; }
    int KmRented { get; set; }
    int ? KmReturned { get; set; }
    DateTime Rented { get; set; }
    DateTime ? Returned { get; set; }
    double ? Cost { get; set; }
    BookingStatuses Status { get; set; }
}
