namespace MovieBookingSystem.Core.Models;

public class Booking
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<MovieBookings> Movies { get; set; }

}