using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System.Reflection.Metadata;

namespace Car_Rental.Common.Classes;

public class Vehicle : IVehicle
{
    public int ID { get; set; }
    public string RegNo { get; set; }
    public string Make { get; set; }
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public VehicleTypes VehicleType { get; set; }
    public double CostPerDay { get; set; }
    public VehicleStatuses Status { get; set; }
}
