namespace MovieBookingSystem.Core.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public int Rating { get; set; }
    public int QuantityAvailable { get; set; }
    public string Description { get; set; }
    public List<MovieBookings> Bookings { get; set; }
}