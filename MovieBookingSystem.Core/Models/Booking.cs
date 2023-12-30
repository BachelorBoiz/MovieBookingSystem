namespace MovieBookingSystem.Core.Models;

public class Booking
{
    public int Id { get; set; }
    public Customer Customer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Movie> Movies { get; set; }

}