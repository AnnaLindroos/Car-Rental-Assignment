using Car_Rental.Common.Enums;

namespace Car_Rental.Business.Classes;

public class UserInputs
{
    public int newSsn = 0;
    public string newFirstName = string.Empty;
    public string newLastName = string.Empty;

    public string newRegNo = string.Empty;
    public string newMake = string.Empty;
    public int newOdometer;
    public double newCostKm;

    public int newDistance = 0;
    public VehicleTypes newVehicleType = VehicleTypes.Sedan;

    public int newRentingCustomer;
    public string error = string.Empty;
    public bool isProcessing = false;
}
