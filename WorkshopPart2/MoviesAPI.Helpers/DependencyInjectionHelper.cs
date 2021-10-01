using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.DataAccess;
using MoviesAPI.DataAccess.Implementations;
using MoviesAPI.Domain;
using MoviesAPI.Domain.Models;
using MoviesAPI.Services.Implementations;
using MoviesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesAPI.Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoviesAppDbContext>(x =>
                x.UseSqlServer(connectionString));
        }

        public static void InjectRepository(IServiceCollection services)
        {
            services.AddTransient<IRepository<Movie>, MovieRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
        }
    }
}
