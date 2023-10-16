namespace Car_Rental.Common.Interfaces;

public interface IPerson
{
    int ID { get; init; }
    int SSN { get; init; }
    string LastName { get; init; }
    string FirstName { get; init; }
}
