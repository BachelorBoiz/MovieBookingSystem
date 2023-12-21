using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Domain.IRepositories;

public interface IMovieBookingsRepository
{
    public List<MovieBookings> GetMovieBookings();
    public MovieBookings GetMovieBookingById(int id);
    public MovieBookings CreateMovieBooking(MovieBookings movie);
    public MovieBookings UpdateMovieBooking(MovieBookings movie);
    public void DeleteMovieBooking(int id);
}