using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Core.IServices;

public interface IMovieBookingsService
{
    public List<MovieBookings> GetMovieBookings();
    public MovieBookings GetMovieBookingById(int id);
    public MovieBookings CreateMovieBooking(MovieBookings movie);
    public MovieBookings UpdateMovieBooking(MovieBookings movie);
    public void DeleteMovieBooking(int id);
}