using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Extensions;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;
    public UserInputs _inputs = new();

    public BookingProcessor(IData db) => _db = db;

    public async Task RentVehicle(int vehicleId)
    {
        try
        {
            _inputs.isProcessing = true;
            await Task.Delay(5000);
            var booking = _db.RentVehicle(vehicleId, _inputs.newCustomer.ID);
            _db.Add(booking);
        }
        catch (ArgumentNullException)
        {
            _inputs.error = "Could not add booking. Please make sure to select a customer";
        }

        _inputs.isProcessing = false;
    }

    public IBooking? ReturnVehicle(int vehicleId)
    {
        try
        {
            var booking = _db.ReturnVehicle(vehicleId, _inputs.newDistance);
            return booking;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _inputs.error = ex.Message;
        }
        return null;
    }

    public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatuses));
    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
    public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);

    public IEnumerable<Vehicle> GetVehicles(VehicleStatuses status = default)
    {
        _inputs.error = _inputs.error.ClearString();
        return _db.Get<Vehicle>(null);
    }

    public IEnumerable<IPerson> GetCustomers()
    {
        _inputs.error = _inputs.error.ClearString();
        return _db.Get<IPerson>(null);
    }

    public IEnumerable<IBooking> GetBookings()
    {
        _inputs.error = _inputs.error.ClearString();
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
            _inputs.error = "Couldn't find a person with that ID";
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
            _inputs.error = "Couldn't find a person with that ID";
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
            _inputs.error = "Couldn't find a vehicle with that ID";
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
            _inputs.error = "Couldn't find a vehicle with that RegNo";
            return null;
        }            
    }
    public void AddCustomer()
    {
        if (_inputs.newCustomer.SSN.ToString().Length < 5 || _inputs.newCustomer.SSN.ToString().Length > 5 || _inputs.newCustomer.LastName.Length <= 0 || _inputs.newCustomer.FirstName.Length <= 0 )
        {
            _inputs.error = "Please enter an SSN with five numbers, and a first name and last name";
            return;
        }

        try
        {
            IPerson newCustomer = new Customer(_db.NextPersonId, _inputs.newCustomer.SSN, _inputs.newCustomer.LastName, _inputs.newCustomer.FirstName);
            _db.Add(newCustomer);
            ClearCustomerInput();
        }
        catch (ArgumentNullException ex)
        {
            _inputs.error = ex.Message;
        }
    }

    public void AddVehicle()
    {
        if (_inputs.newVehicle.RegNo.Length < 6 || _inputs.newVehicle.RegNo.Length > 6 || _inputs.newVehicle.Make.Length <= 0 )
        {
            _inputs.error = "Please enter an RegNo with six symbols, and a make with at least one symbol";
            return;
        }

        try
        {
            if (_inputs.newVehicle.VehicleType == VehicleTypes.Motorcycle)
            {
                Vehicle newMotorcycle = new Motorcycle(_db.NextVehicleId, _inputs.newVehicle.RegNo.ToUpper(), _inputs.newVehicle.Make, _inputs.newVehicle.Odometer, _inputs.newVehicle.CostKm, VehicleTypes.Motorcycle, VehicleStatuses.Available);
                _db.Add(newMotorcycle);
                return;
            }
            double newCarCostPerDay;

            if (_inputs.newVehicle.VehicleType == VehicleTypes.Sedan)
                newCarCostPerDay = 100;
            else if (_inputs.newVehicle.VehicleType == VehicleTypes.Combi)
                newCarCostPerDay = 200;
            else
                newCarCostPerDay = 300;

            Vehicle newCar = new Car(_db.NextVehicleId, _inputs.newVehicle.RegNo.ToUpper(), _inputs.newVehicle.Make, _inputs.newVehicle.Odometer, _inputs.newVehicle.CostKm, _inputs.newVehicle.VehicleType, newCarCostPerDay, VehicleStatuses.Available);
            _db.Add(newCar);

            ClearVehicleInput();
        }
        catch (ArgumentNullException ex)
        {
            _inputs.error = ex.Message;
        }

    }

    public void ClearVehicleInput ()
    {
        _inputs.newVehicle.RegNo = string.Empty;
        _inputs.newVehicle.Make = string.Empty;
        _inputs.newVehicle.Odometer = 0;
        _inputs.newVehicle.CostKm = 0;
    }

    public void ClearCustomerInput()
    {
        _inputs.newCustomer.SSN = 0;
        _inputs.newCustomer.FirstName = string.Empty;
        _inputs.newCustomer.LastName = string.Empty;
    }
}
