using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public int SSN { get; init; }
    public string LastName { get; init; }
    public string FirstName { get; init; }

    public Customer(int ssn, string lastName, string firstName)
        => (SSN, LastName, FirstName) = (ssn, lastName, firstName);
}
