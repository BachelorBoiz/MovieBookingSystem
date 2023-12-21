using MovieBookingSystem.Core.IServices;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Domain.Services;

public class MovieBookingsService : IMovieBookingsService
{
    private readonly IMovieBookingsRepository _repo;

    public MovieBookingsService(IMovieBookingsRepository repo)
    {
        _repo = repo;
    }

    public List<MovieBookings> GetMovieBookings()
    {
        return _repo.GetMovieBookings();
    }

    public MovieBookings GetMovieBookingById(int id)
    {
        if (id > 0)
        {
            return _repo.GetMovieBookingById(id);
        }
        return null;
    }

    public MovieBookings CreateMovieBooking(MovieBookings movie)
    {
        return _repo.CreateMovieBooking(movie);
    }

    public MovieBookings UpdateMovieBooking(MovieBookings movie)
    {
        return _repo.UpdateMovieBooking(movie);
    }

    public void DeleteMovieBooking(int id)
    {
        if (id > 0)
        {
            _repo.DeleteMovieBooking(id);
        }
    }
}