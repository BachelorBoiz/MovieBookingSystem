using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieBookingContext _context;

    public MovieRepository(MovieBookingContext context)
    {
        _context = context;
    }
    public List<Movie> GetMovies()
    {
        return _context.Movies.ToList();
    }

    public Movie GetMovieById(int id)
    {
        return _context.Movies.Find(id) ?? throw new InvalidOperationException();
    }

    public Movie CreateMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        return movie;
    }

    public Movie UpdateMovie(Movie movie)
    {
        _context.Movies.Update(movie);
        return movie;
    }

    public void DeleteMovie(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie != null) _context.Movies.Remove(movie);
    }
}