using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentNChillMovies.Models;

namespace RentNChillMovies.Interfaces
{
    public interface ITransactionRepository
    {
       
        Task PostNewTransaction(int id, string user);
       
    }
}
