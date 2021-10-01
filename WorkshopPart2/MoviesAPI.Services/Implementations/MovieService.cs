using MoviesAPI.DataAccess;
using MoviesAPI.Domain.Models;
using MoviesAPI.Mappers;
using MoviesAPI.Models.ViewModels;
using MoviesAPI.Services.Interfaces;
using MoviesAPI.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesAPI.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void AddMovie(MovieModel movieModel)
        {
            if (string.IsNullOrEmpty(movieModel.Title))
            {
                throw new MovieException("The property Title is required!");
            }

            if(movieModel.Id != 0)
            {
                throw new MovieException("Id must not be set!");
            }
            if(movieModel.Description.Length > 500)
            {
                throw new MovieException("The property Description cannot be longer than 500 characters.");
            }

            Movie movieForDb = movieModel.ToMovie();
            _movieRepository.Add(movieForDb);
            
        }

        public void DeleteMovie(int id)
        {
            Movie movieDb = _movieRepository.GetById(id);
            if(movieDb == null)
            {
                throw new NotFoundException($"There is no movie with id {id}!");
            }
            _movieRepository.Delete(movieDb);
        }

        public List<MovieModel> GetAllMovies()
        {
            List<Movie> moviesDb = _movieRepository.GetAll();
            List<MovieModel> movieModels = new List<MovieModel>();

            foreach(Movie movie in moviesDb)
            {
                movieModels.Add(movie.ToMovieModel());
            }

            return movieModels;
        }

        public MovieModel GetMovieById(int id)
        {
            Movie movieDb = _movieRepository.GetById(id);
            if(movieDb == null)
            {
                throw new NotFoundException($"There is no movie with id {id}!");
            }
            return movieDb.ToMovieModel();
        }

        public void UpdateMovie(MovieModel movieModel)
        {
            Movie movieDb = _movieRepository.GetById(movieModel.Id);
            if (movieDb == null)
            {
                throw new NotFoundException($"There is no movie with id {movieModel.Id}!");
            }

            if (string.IsNullOrEmpty(movieModel.Title))
            {
                throw new MovieException("The property Title is required!");
            }

           

            if (movieModel.Description.Length > 500)
            {
                throw new MovieException("The property Description cannot be longer than 500 characters.");
            }

            movieDb.Title = movieModel.Title;
            movieDb.Year = movieModel.Year;
            movieDb.Genre = movieModel.Genre;
            movieDb.Description = movieModel.Description;


        }
    }
}
