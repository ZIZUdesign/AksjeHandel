using AksjeHandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Interfaces
{
    public interface IStock 
    {
         Task<List<Stock>> GetAllStocks();
         Task<Stock> GetOneStock(int id );
        Task<List<Stock>> SearchResult(string searchTerm);

    }
}
