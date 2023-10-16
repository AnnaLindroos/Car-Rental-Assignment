using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Motorcycle : Vehicle
{
    public Motorcycle(int id, string regNo, string make, int odometer, double costKm, VehicleTypes vehicleType, VehicleStatuses status, double costPerDay = 50)
    {
        (ID, RegNo, Make, Odometer, CostKm, VehicleType, CostPerDay, Status)
         = (id, regNo, make, odometer, costKm, vehicleType, costPerDay, status);
    }
}
