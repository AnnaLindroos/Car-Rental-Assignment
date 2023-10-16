using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public int ID { get; init; }
    public int SSN { get; init; }
    public string LastName { get; init; }
    public string FirstName { get; init; }

    public Customer(int id, int ssn, string lastName, string firstName)
        => (ID, SSN, LastName, FirstName) = (id, ssn, lastName, firstName);
}
