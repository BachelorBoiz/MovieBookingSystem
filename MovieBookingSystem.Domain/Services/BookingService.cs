using MovieBookingSystem.Core.Exceptions;
using MovieBookingSystem.Core.IServices;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repo;
    private readonly IMovieService _movieService;
    public BookingService(IBookingRepository repo, IMovieService movieService)
    {
        _repo = repo;
        _movieService = movieService;
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

    public Booking CreateBooking(Booking booking, IEnumerable<Movie> movies)
    {
        if (booking.Customer.HasOverdueBooking)
        {
            throw new CustomerHasOverdueBookingException("Customer has overdue bookings");
        }

        if ((booking.EndDate - booking.StartDate).TotalDays > 7)
        {
            return null;
        }

        if (!movies.Any())
        {
            return null;
        }

        booking.Movies = AddMoviesToBooking(movies);

        if (booking.Movies.Count != movies.Count())
        {
            return null;
        }

        var newBooking = _repo.CreateBooking(booking);

        return newBooking;
    }

    public List<Movie> AddMoviesToBooking(IEnumerable<Movie> movies)
    {
        var availableMovies = _movieService.GetMovies();

        var selectedMovies = movies.Where(movie => availableMovies.Any(availableMovie => availableMovie.Id == movie.Id && availableMovie.QuantityAvailable > 0)).ToList();

        if (!selectedMovies.Any())
        {
            return new List<Movie>();
        }

        return selectedMovies;
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