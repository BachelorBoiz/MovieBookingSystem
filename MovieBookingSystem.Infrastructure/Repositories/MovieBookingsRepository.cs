using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Infrastructure.Repositories;

public class MovieBookingsRepository : IMovieBookingsRepository
{
    private readonly MovieBookingContext _context;

    public MovieBookingsRepository(MovieBookingContext context)
    {
        _context = context;
    }
    public List<MovieBookings> GetMovieBookings()
    {
        return _context.MovieBookings.ToList();
    }

    public MovieBookings GetMovieBookingById(int id)
    {
        return _context.MovieBookings.Find(id) ?? throw new InvalidOperationException();
    }

    public MovieBookings CreateMovieBooking(MovieBookings movieBookings)
    {
        _context.MovieBookings.Add(movieBookings);
        return movieBookings;
    }

    public MovieBookings UpdateMovieBooking(MovieBookings movieBookings)
    {
        _context.MovieBookings.Update(movieBookings);
        return movieBookings;
    }

    public void DeleteMovieBooking(int id)
    {
        var movieBookings = _context.MovieBookings.Find(id);
        if (movieBookings != null) _context.MovieBookings.Remove(movieBookings);
    }
}