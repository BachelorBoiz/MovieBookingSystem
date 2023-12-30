using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Infrastructure;

public class DbInitializer : IDbInitializer
{
    public void Initialize(MovieBookingContext context)
    {
        // Delete the database, if it already exists.
        context.Database.EnsureDeleted();

        // Create the database, if it does not already exists.
        context.Database.EnsureCreated();

        // Look for any bookings.
        if (context.Bookings.Any())
        {
            // DB has been seeded
            return;   
        }
        List<Movie> movies = new List<Movie>
        {
            new Movie { Title="Movie 1", ReleaseYear = 1999, Rating = 1, QuantityAvailable = 1 , Description = "Description 1"},
            new Movie { Title="Movie 2", ReleaseYear = 2023, Rating = 2, QuantityAvailable = 5 , Description = "Description 2"},
            new Movie { Title="Movie 3", ReleaseYear = 2005, Rating = 3, QuantityAvailable = 3 , Description = "Description 3"},
            new Movie { Title="Movie 4", ReleaseYear = 2012, Rating = 4, QuantityAvailable = 2 , Description = "Description 4"},
        };
        List<Customer> customers = new List<Customer>
        {
            new Customer { Name="Bobby Bobsen"},
            new Customer { Name="Fred Frederiksen"}
        };
        
        DateTime date = DateTime.Today.AddDays(4);
        List<Booking> bookings = new List<Booking>
        {
            new Booking { StartDate=date, EndDate=date.AddDays(14)},
            new Booking { StartDate=date, EndDate=date.AddDays(14)},
            new Booking { StartDate=date, EndDate=date.AddDays(14)}
        };
        
        context.Movies.AddRange(movies);
        context.Customers.AddRange(customers);
        context.SaveChanges();
        
        // Adding bookings and associating movies with bookings
        foreach (var booking in bookings)
        {
            context.Bookings.Add(booking);

            // Choose movies to associate with each booking
            // Choose first 2 movies
            var moviesForBooking = context.Movies.Take(2).ToList(); 

            foreach (var movie in moviesForBooking)
            {
                var movieBooking = new MovieBookings { MovieId = movie.Id, BookingId = booking.Id };
                //booking.Movies ??= new List<MovieBookings>();
                //booking.Movies.Add(movieBooking);
                
                context.MovieBookings.Add(movieBooking);
            }
        }
        context.SaveChanges();
    }
}