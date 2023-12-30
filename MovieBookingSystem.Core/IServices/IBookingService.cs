using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Core.IServices;

public interface IBookingService
{
    public List<Booking> GetBookings();
    public Booking GetBookingById(int id);
    public Booking CreateBooking(Booking booking, IEnumerable<Movie> movies);
    public Booking UpdateBooking(Booking booking);
    public void DeleteBooking(int id);
}