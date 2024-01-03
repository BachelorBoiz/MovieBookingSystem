using Moq;
using MovieBookingSystem.Core.Exceptions;
using MovieBookingSystem.Core.IServices;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;
using MovieBookingSystem.Domain.Services;
using Xunit;

namespace MovieBookingSystem.SpecFlowTests.Steps;

[Binding]
public class CreateBookingSteps
{
    private Booking booking = new Booking();
    private Customer customer = new Customer();
    private List<Movie> movies = new List<Movie>();
    private IBookingService bookingService;
    private readonly Mock<IMovieService> movieServiceMock = new Mock<IMovieService>();
    private readonly Mock<IBookingRepository> bookingRepoMock = new Mock<IBookingRepository>();
    private CustomerHasOverdueBookingException _customerHasOverdueBookingException;
    private MovieUnavailableException _movieUnavailableException;
    private InvalidDateSpanException _invalidDateSpanException;
    private ArgumentException _argumentException;

    public CreateBookingSteps()
    {
        customer.Id = 1;
        customer.Name = "Bobby";
        bookingService = new BookingService(bookingRepoMock.Object, movieServiceMock.Object);
        bookingRepoMock.Setup(repository => repository.CreateBooking(It.IsAny<Booking>()))
            .Returns(booking);
    }

    [Given("a list of available movies")]
    public void GivenAListOfAvailableMovies()
    {
        var movie1 = new Movie
        {
            Id = 1,
            Title = "Lord of The Rings",
            Description = "Lord of the rings",
            Rating = 5,
            ReleaseYear = 2001,
            QuantityAvailable = 10
        };
        var movie2 = new Movie
        {
            Id = 2,
            Title = "Star Wars",
            Description = "War in space",
            Rating = 4,
            ReleaseYear = 1977,
            QuantityAvailable = 10
        };
        movies.Add(movie1);
        movies.Add(movie2);
        
        movieServiceMock.Setup(service => service.GetMovies()).Returns(movies);
    }
    
    [Given("a customer with no overdue bookings")]
    public void GivenACustomerWithNoOverdueBookings()
    {
        customer.HasOverdueBooking = false;
        booking.Customer = customer;
    }
    
    [Given("a customer with overdue bookings")]
    public void GivenACustomerWithOverdueBookings()
    {
        customer.HasOverdueBooking = true;
        booking.Customer = customer;
    }
    
    [Given("a list with an unavailable movie")]
    public void GivenAListWithAnUnavailableMovie()
    {
        var movie1 = new Movie
        {
            Id = 1,
            Title = "Lord of The Rings",
            Description = "Lord of the rings",
            Rating = 5,
            ReleaseYear = 2001,
            QuantityAvailable = 1
        };
        var movie2 = new Movie
        {
            Id = 2,
            Title = "Star Wars",
            Description = "War in space",
            Rating = 4,
            ReleaseYear = 1977,
            QuantityAvailable = 0
        };
        movies.Add(movie1);
        movies.Add(movie2);
        
        movieServiceMock.Setup(service => service.GetMovies()).Returns(movies);
    }

    [Given("a booking with a valid date span")]
    public void GivenABookingWithAValidDateSpan()
    {
        booking.StartDate = DateTime.Today.AddDays(10);
        booking.EndDate = DateTime.Today.AddDays(17);
    }
    
    [Given("a booking with an invalid date span")]
    public void GivenABookingWithAnInvalidDateSpan()
    {
        booking.StartDate = DateTime.Today.AddDays(10);
        booking.EndDate = DateTime.Today.AddDays(18);
    }
    
    [Given("a list with no movies")]
    public void GivenAListWithNoMovies()
    {
        movieServiceMock.Setup(service => service.GetMovies()).Returns(movies);
    }

    [When("i create a booking")]
    public void WhenICreateABooking()
    {
        try
        {
            booking = bookingService.CreateBooking(booking, movies);
        }
        catch (CustomerHasOverdueBookingException e)
        {
            _customerHasOverdueBookingException = e;
        }
        catch (MovieUnavailableException e)
        {
            _movieUnavailableException = e;
        }
        catch (InvalidDateSpanException e)
        {
            _invalidDateSpanException = e;
        }
        catch (ArgumentException e)
        {
            _argumentException = e;
        }
    }
    
    [Then("the booking should be added successfully")]
    public void ThenTheBookingShouldBeAddedSuccessfully()
    {
        Assert.IsType<Booking>(booking);
    }
    
    [Then("the booking should throw a CustomerHasOverDueBookingException")]
    public void ThenTheBookingShouldThrowACustomerHasOverDueBookingException()
    {
        Assert.NotNull(_customerHasOverdueBookingException);
    }
    
    [Then("the booking should throw a MovieUnavailableException")]
    public void ThenTheBookingShouldThrowAMovieUnavailableException()
    {
        Assert.IsType<MovieUnavailableException>(_movieUnavailableException);
        Assert.NotNull(_movieUnavailableException);
    }

    [Then("the booking should throw a InvalidDateSpanException")]
    public void ThenTheBookingShouldThrowAInvalidDateSpanException()
    {
        Assert.IsType<InvalidDateSpanException>(_invalidDateSpanException);
        Assert.NotNull(_invalidDateSpanException);
    }

    [Then("the booking should throw an ArgumentException")]
    public void ThenTheBookingShouldThrowAnArgumentException()
    {
        Assert.IsType<ArgumentException>(_argumentException);
        Assert.NotNull(_argumentException);
    }
}
