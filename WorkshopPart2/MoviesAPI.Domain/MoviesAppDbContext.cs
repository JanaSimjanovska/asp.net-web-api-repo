using Microsoft.EntityFrameworkCore;
using MoviesAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesAPI.Domain
{
    public class MoviesAppDbContext : DbContext
    {

        public MoviesAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .IsRequired()
                .HasMaxLength(4);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Genre)
                .HasMaxLength(100);
        }
    }
}
