using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RentNChillMovies.Models;
using Microsoft.AspNetCore.Identity;

namespace RentNChillMovies.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieProducer> MovieProducers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserMovie>().HasKey(ab => new { ab.MovieId, ab.UserId });
            modelBuilder.Entity<UserLike>().HasKey(ba => new { ba.MovieId, ba.UserId });
            modelBuilder.Entity<MovieReview>().HasKey(a => new { a.MovieId, a.UserId });
        }
       
       
    }
}
