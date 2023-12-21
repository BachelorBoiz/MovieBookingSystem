using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Domain.IRepositories;

public interface IBookingRepository
{
    public List<Booking> GetBookings();
    public Booking GetBookingById(int id);
    public Booking CreateBooking(Booking booking);
    public Booking UpdateBooking(Booking booking);
    public void DeleteBooking(int id);
}