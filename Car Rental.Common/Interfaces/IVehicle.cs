using Car_Rental.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Interfaces;

public interface IVehicle
{
    string RegNo { get; init; }
    string Make { get; init; }
    int Odometer { get; set; }
    double Cost { get; init; }
    VehicleTypes VehicleType { get; init; }
    double CostPerDay { get; init; }
    VehicleStatuses Status { get; set; }
}
