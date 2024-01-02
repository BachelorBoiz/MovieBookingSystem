using Moq;
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
    private List<Movie> movies = new List<Movie>();
    private IBookingService bookingService;
    private readonly Mock<IMovieService> movieServiceMock = new Mock<IMovieService>();
    private readonly Mock<IBookingRepository> bookingRepoMock = new Mock<IBookingRepository>();

    public CreateBookingSteps()
    {
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
            QuantityAvailable = 1
        };
        var movie2 = new Movie
        {
            Id = 2,
            Title = "Star Wars",
            Description = "War in space",
            Rating = 4,
            ReleaseYear = 1977,
            QuantityAvailable = 1
        };
        movies.Add(movie1);
        movies.Add(movie2);
        
        movieServiceMock.Setup(service => service.GetMovies()).Returns(movies);
    }

    [When("i create a booking")]
    public void WhenICreateABooking()
    {
        booking = bookingService.CreateBooking(booking, movies);
    }
    
    [Then("the booking should be added successfully")]
    public void ThenTheBookingShouldBeAddedSuccessfully()
    {
        Assert.IsType<Booking>(booking);
    }
    
}
