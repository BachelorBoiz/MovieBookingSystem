using MovieBookingSystem.Core.IServices;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Domain.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repo;

    public MovieService(IMovieRepository repo)
    {
        _repo = repo;
    }

    public List<Movie> GetMovies()
    {
        return _repo.GetMovies();
    }

    public void UpdateMovieQuantity(int movieId)
    {
        throw new NotImplementedException();
    }

    public Movie GetMovieById(int id)
    {
        if (id > 0)
        {
            return _repo.GetMovieById(id);
        }
        return null;
    }

    public Movie CreateMovie(Movie movie)
    {
        return _repo.CreateMovie(movie);
    }

    public Movie UpdateMovie(Movie movie)
    {
        return _repo.UpdateMovie(movie);
    }

    public void DeleteMovie(int id)
    {
        if (id > 0)
        {
            _repo.DeleteMovie(id);
        }
    }
}