using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Extensions;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;
    public readonly UserInputs input;

    public BookingProcessor(IData db, UserInputs inputs) => (_db, input) = (db, inputs);

    public async Task RentVehicle(int vehicleId)
    {
        try
        {
            input.isProcessing = true;
            await Task.Delay(5000);
            var booking = _db.RentVehicle(vehicleId, input.newCustomer.ID);
            _db.Add(booking);
        }
        catch (ArgumentNullException)
        {
            input.error = "Could not add booking. Please make sure to select a customer";
        }

        input.isProcessing = false;
    }

    public IBooking? ReturnVehicle(int vehicleId)
    {
        try
        {
            var booking = _db.ReturnVehicle(vehicleId, input.newDistance);
            return booking;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            input.error = ex.Message;
        }
        return null;
    }

    public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatuses));
    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
    public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);

    public IEnumerable<Vehicle> GetVehicles(VehicleStatuses status = default)
    {
        input.error = input.error.ClearString();
        return _db.Get<Vehicle>(null);
    }

    public IEnumerable<IPerson> GetCustomers()
    {
        input.error = input.error.ClearString();
        return _db.Get<IPerson>(null);
    }

    public IEnumerable<IBooking> GetBookings()
    {
        input.error = input.error.ClearString();
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
            input.error = "Couldn't find a person with that ID";
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
            input.error = "Couldn't find a person with that ID";
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
            input.error = "Couldn't find a vehicle with that ID";
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
            input.error = "Couldn't find a vehicle with that RegNo";
            return null;
        }            
    }
    public void AddCustomer()
    {
        if (input.newCustomer.SSN.ToString().Length < 5 || input.newCustomer.SSN.ToString().Length > 5 || input.newCustomer.LastName.Length <= 0 || input.newCustomer.FirstName.Length <= 0 )
        {
            input.error = "Please enter an SSN with five numbers, and a first name and last name";
            return;
        }

        try
        {
            IPerson newCustomer = new Customer(_db.NextPersonId, input.newCustomer.SSN, input.newCustomer.LastName, input.newCustomer.FirstName);
            _db.Add(newCustomer);
            ClearCustomerInput();
        }
        catch (ArgumentNullException ex)
        {
            input.error = ex.Message;
        }
    }

    public void AddVehicle()
    {
        if (input.newVehicle.RegNo.Length < 6 || input.newVehicle.RegNo.Length > 6 || input.newVehicle.Make.Length <= 0 )
        {
            input.error = "Please enter an RegNo with six symbols, and a make with at least one symbol";
            return;
        }

        try
        {
            if (input.newVehicle.VehicleType == VehicleTypes.Motorcycle)
            {
                Vehicle newMotorcycle = new Motorcycle(_db.NextVehicleId, input.newVehicle.RegNo.ToUpper(), input.newVehicle.Make, input.newVehicle.Odometer, input.newVehicle.CostKm, VehicleTypes.Motorcycle, VehicleStatuses.Available);
                _db.Add(newMotorcycle);
                return;
            }
            double newCarCostPerDay;

            if (input.newVehicle.VehicleType == VehicleTypes.Sedan)
                newCarCostPerDay = 100;
            else if (input.newVehicle.VehicleType == VehicleTypes.Combi)
                newCarCostPerDay = 200;
            else
                newCarCostPerDay = 300;

            Vehicle newCar = new Car(_db.NextVehicleId, input.newVehicle.RegNo.ToUpper(), input.newVehicle.Make, input.newVehicle.Odometer, input.newVehicle.CostKm, input.newVehicle.VehicleType, newCarCostPerDay, VehicleStatuses.Available);
            _db.Add(newCar);

            ClearVehicleInput();
        }
        catch (ArgumentNullException ex)
        {
            input.error = ex.Message;
        }

    }

    public void ClearVehicleInput ()
    {
        input.newVehicle.RegNo = string.Empty;
        input.newVehicle.Make = string.Empty;
        input.newVehicle.Odometer = 0;
        input.newVehicle.CostKm = 0;
    }

    public void ClearCustomerInput()
    {
        input.newCustomer.SSN = 0;
        input.newCustomer.FirstName = string.Empty;
        input.newCustomer.LastName = string.Empty;
    }
}
