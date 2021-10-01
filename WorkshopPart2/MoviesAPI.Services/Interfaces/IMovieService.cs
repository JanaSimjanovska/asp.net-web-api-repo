using MoviesAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesAPI.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieModel> GetAllMovies();
        MovieModel GetMovieById(int id);
        void AddMovie(MovieModel movieModel);
        void UpdateMovie(MovieModel movieModel);
        void DeleteMovie(int id);
    }

    
}
