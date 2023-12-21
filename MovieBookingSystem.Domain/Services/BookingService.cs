using MovieBookingSystem.Core.IServices;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repo;
    public BookingService(IBookingRepository repo)
    {
        _repo = repo;
    }
    public List<Booking> GetBookings()
    {
        return _repo.GetBookings();
    }

    public Booking GetBookingById(int id)
    {
        if (id > 0)
        {
            return _repo.GetBookingById(id);
        }
        return null;
    }

    public Booking CreateBooking(Booking booking)
    {
        return _repo.CreateBooking(booking);
    }

    public Booking UpdateBooking(Booking booking)
    {
        return _repo.UpdateBooking(booking);
    }

    public void DeleteBooking(int id)
    {
        if (id > 0)
        {
            _repo.DeleteBooking(id);
        }
    }
}