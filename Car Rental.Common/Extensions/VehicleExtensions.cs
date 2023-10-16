namespace Car_Rental.Common.Extensions;

public static class VehicleExtensions
{
    public static int Duration(this DateTime startDate, DateTime endDate) => (int)(endDate - startDate).TotalDays; 
}
