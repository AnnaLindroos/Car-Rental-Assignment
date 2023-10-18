using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public int ID { get; set; }
    public int SSN { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }

    public Customer(int id, int ssn, string lastName, string firstName)
        => (ID, SSN, LastName, FirstName) = (id, ssn, lastName, firstName);
    public Customer() { }
}
