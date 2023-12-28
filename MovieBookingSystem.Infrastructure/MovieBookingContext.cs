using Microsoft.EntityFrameworkCore;
using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Infrastructure;

public class MovieBookingContext : DbContext
{
    public MovieBookingContext(DbContextOptions<MovieBookingContext> options) : base(options) {}
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<MovieBookings> MovieBookings { get; set; }
    
}