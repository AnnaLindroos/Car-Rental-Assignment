namespace Car_Rental.Common.Interfaces;

public interface IPerson
{
    int ID { get; set; }
    int SSN { get; set; }
    string LastName { get; set; }
    string FirstName { get; set; }
}
