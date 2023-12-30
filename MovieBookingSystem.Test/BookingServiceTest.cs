using System.Runtime.InteropServices;
using Moq;
using MovieBookingSystem.Core.IServices;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;
using MovieBookingSystem.Domain.Services;

namespace MovieBookingSystem.Test
{
    public class BookingServiceTest
    {

        [Theory]
        [MemberData(nameof(SuccessfulBookingTestData))]
        public void CreateBookingSuccessful(List<Movie> availableMovies, Booking booking)
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var bookingRepoMock = new Mock<IBookingRepository>();

            var bookingService = new BookingService(bookingRepoMock.Object, movieServiceMock.Object);

            movieServiceMock.Setup(service => service.GetMovies())
                .Returns(availableMovies);

            bookingRepoMock.Setup(repository => repository.CreateBooking(It.IsAny<Booking>()))
                .Returns(booking);

            // Act
            var result = bookingService.CreateBooking(booking, availableMovies);

            // Assert
            Assert.IsType<Booking>(result);
            Assert.Equal(booking.Id, result.Id);
            foreach (var expectedMovie in availableMovies)
            {
                Assert.Contains(expectedMovie, booking.Movies);
            }
            bookingRepoMock.Verify(repository => repository.CreateBooking(It.IsAny<Booking>()), Times.Once);
        }

        public static IEnumerable<object[]> SuccessfulBookingTestData()
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
    }
}