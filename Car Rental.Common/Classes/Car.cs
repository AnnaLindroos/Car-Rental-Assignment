using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Car : Vehicle
{
    public Car(int id, string regNo, string make, int odometer, double costKm, 
        VehicleTypes vehicletype, double costPerDay, VehicleStatuses status) 
        => (ID, RegNo, Make, Odometer, CostKm, VehicleType, CostPerDay, Status)
         = (id, regNo, make, odometer, costKm, vehicletype, costPerDay, status);
}
