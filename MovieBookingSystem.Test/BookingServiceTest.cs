using System.Runtime.InteropServices;
using Moq;
using MovieBookingSystem.Core.Exceptions;
using MovieBookingSystem.Core.IServices;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;
using MovieBookingSystem.Domain.Services;

namespace MovieBookingSystem.Test
{
    public class BookingServiceTest
    {

        [Theory]
        [MemberData(nameof(BookingTestData))]
        public void CreateBookingSuccessful(List<Movie> movies, Booking booking)
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var bookingRepoMock = new Mock<IBookingRepository>();

            var bookingService = new BookingService(bookingRepoMock.Object, movieServiceMock.Object);

            movieServiceMock.Setup(service => service.GetMovies())
                .Returns(movies);

            bookingRepoMock.Setup(repository => repository.CreateBooking(It.IsAny<Booking>()))
                .Returns(booking);

            // Act
            var result = bookingService.CreateBooking(booking, movies);

            // Assert
            Assert.IsType<Booking>(result);
            foreach (var expectedMovie in movies)
            {
                Assert.Contains(expectedMovie, booking.Movies);
            }
            bookingRepoMock.Verify(repository => repository.CreateBooking(It.IsAny<Booking>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(BookingTestDataWithUnavailableMovie))]
        public void CreateBookingFailWithUnavailableMovie(List<Movie> movies, Booking booking)
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var bookingRepoMock = new Mock<IBookingRepository>();

            var bookingService = new BookingService(bookingRepoMock.Object, movieServiceMock.Object);

            movieServiceMock.Setup(service => service.GetMovies())
                .Returns(movies);

            bookingRepoMock.Setup(repository => repository.CreateBooking(It.IsAny<Booking>()))
                .Returns(booking);

            // Act
            Action act = () => bookingService.CreateBooking(booking, movies);

            // Assert
            Assert.Throws<MovieUnavailableException>(act);
        }

        [Theory]
        [MemberData(nameof(BookingTestData))]
        public void CreateBookingFailWhenCustomerHasOverdueBooking(List<Movie> movies, Booking booking)
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var bookingRepoMock = new Mock<IBookingRepository>();

            var bookingService = new BookingService(bookingRepoMock.Object, movieServiceMock.Object);

            movieServiceMock.Setup(service => service.GetMovies())
                .Returns(movies);

            bookingRepoMock.Setup(repository => repository.CreateBooking(It.IsAny<Booking>()))
                .Returns(booking);

            booking.Customer.HasOverdueBooking = true;

            // Act
            Action act = () => bookingService.CreateBooking(booking, movies);

            // Assert
            Assert.Throws<CustomerHasOverdueBookingException>(act);
        }

        [Theory]
        [MemberData(nameof(BookingTestData))]
        public void AddMoviesToBooking_WithAvailableMovies_ShouldReturnSelectedMovies(List<Movie> movies, Booking booking)
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            movieServiceMock.Setup(service => service.GetMovies())
                .Returns(movies);

            var bookingRepoMock = new Mock<IBookingRepository>();

            var bookingService = new BookingService(bookingRepoMock.Object, movieServiceMock.Object);

            // Act
            var result = bookingService.AddMoviesToBooking(movies);
            booking.Movies = result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.True(result.All(movie => movies.Any(selectedMovie => selectedMovie.Id == movie.Id)));
            Assert.Equal(2, booking.Movies.Count);
        }

        public static IEnumerable<object[]> BookingTestData()
        {
            yield return new object[]
            {
                new List<Movie>
                {
                    new Movie
                    {
                        Id = 1, Title = "Star Wars", Description = "Star Wars", Rating = 5, ReleaseYear = 1977,
                        QuantityAvailable = 2
                    },
                    new Movie
                    {
                        Id = 2, Title = "Lord of The Rings", Description = "Lord of the rings", Rating = 5,
                        ReleaseYear = 2001, QuantityAvailable = 1
                    }
                },
                new Booking
                {
                    Id = 1,
                    Customer = new Customer { Id = 1, Name = "John", HasOverdueBooking = false },
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1)
                }
            };
        }

        public static IEnumerable<object[]> BookingTestDataWithUnavailableMovie()
        {
            yield return new object[]
            {
                new List<Movie>
                {
                    new Movie
                    {
                        Id = 1, Title = "Star Wars", Description = "War in space", Rating = 5, ReleaseYear = 1977,
                        QuantityAvailable = 2
                    },
                    new Movie
                    {
                        Id = 3, Title = "Indiana Jones", Description = "Temple explorer man", Rating = 5,
                        ReleaseYear = 1981, QuantityAvailable = 0
                    }
                },
                new Booking
                {
                    Id = 1,
                    Customer = new Customer { Id = 1, Name = "John", HasOverdueBooking = false },
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1)
                }
            };
        }
    }
}