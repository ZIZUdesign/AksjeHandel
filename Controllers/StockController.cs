using AksjeHandel.Interfaces;
using AksjeHandel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AksjeHandel.Controllers
{
    [Route("[controller]/[action]")]
    public class StockController : ControllerBase
    {
        private readonly IStock _db;

        public StockController(IStock db)
        {
            _db = db;
        }

        public async Task<List<Stock>> GetStocks()
        {
            return await _db.GetAllStocks();

            
        }

        public async Task<Stock> GetStock(int id)
        {
            return await _db.GetOneStock(id);
        }

        public async Task<List<Stock>> GetSeacrhResult(string searchTerm)
        {
            return await _db.SearchResult(searchTerm);
        }
    }
}
