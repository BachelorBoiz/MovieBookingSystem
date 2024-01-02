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
        ValidateBooking(booking, movies);

        var selectedMovies = AddMoviesToBooking(movies);
        if (selectedMovies.Count != movies.Count())
        {
            return null;
        }

        var newBooking = _repo.CreateBooking(booking);

        return newBooking;
    }
    
    private void ValidateBooking(Booking booking, IEnumerable<Movie> movies)
    {
        if (booking.Customer.HasOverdueBooking)
        {
            throw new CustomerHasOverdueBookingException("Customer has overdue bookings");
        }
        
        if (movies.Any(movie => movie.QuantityAvailable == 0))
        {
            throw new MovieUnavailableException("Movie is unavailable");
        }

        if ((booking.EndDate - booking.StartDate).TotalDays > 7)
        {
            throw new InvalidDateSpanException("Invalid date span. Max date span is 7 days");
        }

        if (!movies.Any())
        {
            throw new ArgumentException("Movie list is empty");
        }
    }

    public List<Movie> AddMoviesToBooking(IEnumerable<Movie> movies)
    {
        var availableMovies = _movieService.GetMovies();
        var selectedMovies = new List<Movie>();

        foreach (var movie in movies)
        {
            var dbMovie = availableMovies.FirstOrDefault(dbMovie => dbMovie.Id == movie.Id && dbMovie.QuantityAvailable > 0);

            if (dbMovie != null)
            {
                selectedMovies.Add(dbMovie);
                _movieService.UpdateMovieQuantity(dbMovie.Id, -1);
            }
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