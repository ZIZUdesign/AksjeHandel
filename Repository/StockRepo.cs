using AksjeHandel.DAL;
using AksjeHandel.Interfaces;
using AksjeHandel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Repository
{
    public class StockRepo : IStock
    {
        private readonly AksjeContext _context;

        public StockRepo(AksjeContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllStocks()
        {
            try
            {
                List<Stock> allStocks = await _context.Stocks.Select(s => new Stock
                { 
                    Id = s.Id,
                    StockName = s.StockName,
                    Description = s.Description,
                    CompanyName = s.CompanyName,
                    Quantity = s.Quantity,
                    Price  = s.Price
                    

                }).ToListAsync();


                return allStocks;
            }
            
            catch
            {
                return null;
            }
            
        }

        public async Task<Stock> GetOneStock(int id)
        {
            Stock stock = await _context.Stocks.FindAsync(id);

            var oneStock = new Stock()
            {
                Id = stock.Id,
                StockName = stock.StockName,
                CompanyName = stock.CompanyName,
                Description = stock.Description,
                Price = stock.Price,
                Quantity = stock.Quantity
            };

            return oneStock; 

        }

        public async Task<List<Stock>> SearchResult(string searchTerm)
        {
           // if (string.IsNullOrEmpty(searchTerm)) 
                //return await GetAllStocks();

            var searchTermLowerCase = searchTerm.Trim().ToLower();

            var stocks = await _context.Stocks.Where(s => 
                    s.StockName.ToLower().Contains(searchTermLowerCase)).ToListAsync();
            var searchResultList = new List<Stock>();

            foreach (var stock in stocks)
            {
                var searchedStock = new Stock
                {
                    Id = stock.Id, 
                    StockName = stock.StockName, 
                    CompanyName = stock.CompanyName,
                    Price = stock.Price,
                    Quantity = stock.Quantity,
                    Description = stock.Description
                };

                searchResultList.Add(searchedStock);
            }

            return searchResultList;


        }
    }
}
