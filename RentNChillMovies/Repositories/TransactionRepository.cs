using Microsoft.AspNetCore.Identity;
using RentNChillMovies.Data;
using RentNChillMovies.Interfaces;
using RentNChillMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext dbContext;
      

        public TransactionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

  

        public async Task PostNewTransaction(int id, string user)
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(30);
            var account = dbContext.Accounts.FirstOrDefault(a => a.UserId == user);
            var movie = dbContext.Movies.FirstOrDefault(b => b.MovieId == id);
            var UserObj = dbContext.Users.FirstOrDefault(m => m.Id == user);
            var rentEndDate = dbContext.Rentals.FirstOrDefault(m => m.MovieId == id);

            var rent = new Rental
            {
                UserId = user,
                MovieId = id,
                RentalDateStart = startDate,
                RentalDateEnd = endDate,
                RentalAmount = movie.Price
            };
            //adding Renatal to Renatal list
            dbContext.Add(rent);
            await dbContext.SaveChangesAsync();



            var movies = new UserMovie
            {
                UserId = user,
                IsTrasactionComplete = true,
                MovieId = id,
                RentalEndDate = endDate
            };
            //adding Movies to userMovies list
            dbContext.Add(movies);

            var transaction = new Transaction
            {
                AccountId = account.AccountId,
                TransactionDate = startDate,
                IsCompleted = true,
                TransactionAmount = movie.Price,
                RentId = rent.RentalId,
            };
            //adding Transaction to Transaction list
            dbContext.Add(transaction);
            await dbContext.SaveChangesAsync();

        }
    }
}
