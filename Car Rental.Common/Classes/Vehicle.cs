using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Vehicle
{
    public int ID { get; init; }
    public string RegNo { get; set; }
    public string Make { get; set; }
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public VehicleTypes VehicleType { get; set; }
    public double CostPerDay { get; set; }
    public VehicleStatuses Status { get; set; }
}
