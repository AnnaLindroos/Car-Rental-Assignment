using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Extensions;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;

    public BookingProcessor(IData db) => _db = db;

    public UserInputs Inputs = new();

    public async Task RentVehicle(int vehicleId)
    {
        try
        {
            Inputs.isProcessing = true;
            await Task.Delay(5000);
            var booking = _db.RentVehicle(vehicleId, Inputs.newRentingCustomer);
            _db.Add(booking);
        }
        catch (ArgumentNullException)
        {
            Inputs.error = "Could not add booking. Please make sure to select a customer";
        }

        Inputs.isProcessing = false;
    }

    public IBooking? ReturnVehicle(int vehicleId)
    {
        try
        {
            var booking = _db.ReturnVehicle(vehicleId, Inputs.newDistance);
            return booking;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Inputs.error = ex.Message;
        }
        return null;
    }

    public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatuses));
    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
    public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);

    public IEnumerable<Vehicle> GetVehicles(VehicleStatuses status = default)
    {
        Inputs.error = Inputs.error.ClearString();
        return _db.Get<Vehicle>(null);
    }

    public IEnumerable<IPerson> GetCustomers()
    {
        Inputs.error = Inputs.error.ClearString();
        return _db.Get<IPerson>(null);
    }

    public IEnumerable<IBooking> GetBookings()
    {
        Inputs.error = Inputs.error.ClearString();
        return _db.Get<IBooking>(null);
    }

    public IPerson? GetPerson(string ssn)
    {
        try
        {
            var person = _db.Single<IPerson>(i => i.SSN.Equals(ssn));
            return person;
        }
        catch (ArgumentNullException)
        {
            Inputs.error = "Couldn't find a person with that ID";
            return null;
        }
    }
    
    public IPerson? GetPerson(int id)
    {
        try
        {
            var person = _db.Single<IPerson>(i => i.ID.Equals(id));
            return person;
        }
        catch (ArgumentNullException)
        {
            Inputs.error = "Couldn't find a person with that ID";
            return null;
        }
    }
    public Vehicle? GetVehicle(int vehicleId)
    {
        try
        {
            var vehicle = _db.Single<Vehicle>(i => i.ID.Equals(vehicleId));
            return vehicle;
        }
        catch (ArgumentNullException)
        {
            Inputs.error = "Couldn't find a vehicle with that ID";
            return null;
        }
    }
    public Vehicle? GetVehicle(string regNo)
    {
        try
        {
            var vehicle = _db.Single<Vehicle>(i => i.RegNo.Equals(regNo));
            return vehicle;
        }
        catch (ArgumentNullException)
        {
            Inputs.error = "Couldn't find a vehicle with that RegNo";
            return null;
        }            
    }
    public void AddCustomer()
    {
        if (Inputs.newSsn.ToString().Length < 5 || Inputs.newSsn.ToString().Length > 5 || Inputs.newLastName.Length <= 0 || Inputs.newFirstName.Length <= 0 )
        {
            Inputs.error = "Please enter an SSN with five numbers, and a first name and last name";
            return;
        }

        try
        {
            IPerson newCustomer = new Customer(_db.NextPersonId, Inputs.newSsn, Inputs.newLastName, Inputs.newFirstName);
            _db.Add(newCustomer);
            ClearCustomerInput();
        }
        catch (ArgumentNullException ex)
        { 
            Inputs.error = ex.Message;
        }
    }

    public void AddVehicle()
    {
        if (Inputs.newRegNo.Length < 6 || Inputs.newRegNo.Length > 6 || Inputs.newMake.Length <= 0 )
        {
            Inputs.error = "Please enter an RegNo with six symbols, and a make with at least one symbol";
            return;
        }

        try
        {
            if (Inputs.newVehicleType == VehicleTypes.Motorcycle)
            {
                Vehicle newMotorcycle = new Motorcycle(_db.NextVehicleId, Inputs.newRegNo.ToUpper(), Inputs.newMake, Inputs.newOdometer, Inputs.newCostKm, VehicleTypes.Motorcycle, VehicleStatuses.Available);
                _db.Add(newMotorcycle);
                return;
            }
            double newCarCostPerDay;

            if (Inputs.newVehicleType == VehicleTypes.Sedan)
                newCarCostPerDay = 100;
            else if (Inputs.newVehicleType == VehicleTypes.Combi)
                newCarCostPerDay = 200;
            else
                newCarCostPerDay = 300;

            Vehicle newCar = new Car(_db.NextVehicleId, Inputs.newRegNo.ToUpper(), Inputs.newMake, Inputs.newOdometer, Inputs.newCostKm, Inputs.newVehicleType, newCarCostPerDay, VehicleStatuses.Available);
            _db.Add(newCar);

            ClearVehicleInput();
        }
        catch (ArgumentNullException ex)
        {
            Inputs.error = ex.Message;
        }

    }

    public void ClearVehicleInput ()
    {
        Inputs.newRegNo = string.Empty;
        Inputs.newMake = string.Empty;
        Inputs.newOdometer = 0;
        Inputs.newCostKm = 0;
    }

    public void ClearCustomerInput()
    {
        Inputs.newSsn = 0;
        Inputs.newFirstName = string.Empty;
        Inputs.newLastName = string.Empty;
    }
}
