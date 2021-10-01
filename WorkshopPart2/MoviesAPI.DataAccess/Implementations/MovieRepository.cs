using MoviesAPI.Domain;
using MoviesAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesAPI.DataAccess.Implementations
{
    public class MovieRepository : IRepository<Movie>
    {
        private MoviesAppDbContext _moviesAppDbContext;

        public MovieRepository(MoviesAppDbContext moviesAppDbContext)
        {
            _moviesAppDbContext = moviesAppDbContext;
        }

        public void Add(Movie entity)
        {
            _moviesAppDbContext.Movies.Add(entity);
            _moviesAppDbContext.SaveChanges();
        }

        public void Delete(Movie entity)
        {

            _moviesAppDbContext.Movies.Remove(entity);
            _moviesAppDbContext.SaveChanges();
        }

        public List<Movie> GetAll()
        {
            return _moviesAppDbContext
                .Movies
                .ToList();
        }

        public Movie GetById(int id)
        {
            return _moviesAppDbContext
                .Movies
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Movie entity)
        {
            _moviesAppDbContext.Movies.Update(entity);
            _moviesAppDbContext.SaveChanges();
        }
    }
}
