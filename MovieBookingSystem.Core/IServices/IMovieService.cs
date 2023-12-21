﻿using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Core.IServices;

public interface IMovieService
{
    public List<Movie> GetMovies();
    public Movie GetMovieById(int id);
    public Movie CreateMovie(Movie movie);
    public Movie UpdateMovie(Movie movie);
    public void DeleteMovie(int id);
    
}