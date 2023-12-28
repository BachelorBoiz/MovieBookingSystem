using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly MovieBookingContext _context;

    public BookingRepository(MovieBookingContext context)
    {
        _context = context;
    }

    public List<Booking> GetBookings()
    {
        return _context.Bookings.ToList();
    }

    public Booking GetBookingById(int id)
    {
        return _context.Bookings.Find(id) ?? throw new InvalidOperationException();
    }

    public Booking CreateBooking(Booking booking)
    {
        _context.Bookings.Add(booking);
        return booking;
    }

    public Booking UpdateBooking(Booking booking)
    {
        _context.Bookings.Update(booking);
        return booking;
    }

    public void DeleteBooking(int id)
    {
        var booking = _context.Bookings.Find(id);
        if (booking != null) _context.Bookings.Remove(booking);
    }
}