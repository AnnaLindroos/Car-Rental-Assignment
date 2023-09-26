using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

internal class Car : IVehicle
{
    public string RegNo { get; init; }
    public string Make { get; init; }
    public int Odometer { get; set; }
    public double Cost { get; init; }
    public VehicleTypes VehicleType { get; init; }
    public double CostPerDay { get; init; }
    public VehicleStatuses Status { get; set; }
}
