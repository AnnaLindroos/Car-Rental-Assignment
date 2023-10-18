using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Car : Vehicle
{
    public Car(int id, string regNo, string make, int odometer, double costKm, VehicleTypes vehicletype, double costPerDay, VehicleStatuses status)
    {
        ID = id;
        RegNo = regNo;
        Make = make;
        Odometer = odometer;
        CostKm = costKm;
        VehicleType = vehicletype;
        CostPerDay = costPerDay;
        Status = status;
    }
}
