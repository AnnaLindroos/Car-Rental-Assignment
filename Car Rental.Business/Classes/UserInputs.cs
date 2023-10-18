using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;

namespace Car_Rental.Business.Classes;

public class UserInputs
{
    public int newDistance = 0;
    public string error = string.Empty;
    public bool isProcessing = false;

    //public Vehicle newVehicle = new(0, string.Empty, string.Empty, 0, 0, VehicleTypes.Sedan, 0, VehicleStatuses.Available);
    public Vehicle newVehicle { get; set; } = new();
    public Customer newCustomer { get; set; } = new();

}
