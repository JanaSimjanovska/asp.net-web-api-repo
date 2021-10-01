using MoviesAPI.Domain.Models;
using MoviesAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesAPI.Mappers
{
    public static class MovieMapper
    {
        public static Movie ToMovie(this MovieModel movieModel)
        {
            return new Movie
            {
                Id = movieModel.Id,
                Title = movieModel.Title,
                Year = movieModel.Year,
                Genre = movieModel.Genre,
                Description = movieModel.Description
            };
        }

        public static MovieModel ToMovieModel(this Movie movie)
        {
            return new MovieModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Description = movie.Description
            };
        }
    }
}
