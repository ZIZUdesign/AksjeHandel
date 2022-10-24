using AksjeHandel.Interfaces;
using AksjeHandel.Models;
using AksjeHandel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Controllers
{
    [Route("[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transaction;

        public TransactionController(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task<ActionResult> BuyStocks(BuyViewModel buyViewModel)
        {
            var result = await _transaction.BuyStock(buyViewModel);
            if (result.IsSuccess && result != null )
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        public async Task<ActionResult> SellStocks (SellViewModel sellViewModel)
        {
            var result = await _transaction.SellStock(sellViewModel);
            if (result.IsSuccess && result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        public async Task<IActionResult> GetUserBoughtStocks(string userName)
        {
            var boughtStocks = await _transaction.GetBoughtStocks(userName);
            if (boughtStocks.Count < 1)
            {
                return BadRequest("You have not bought a stock yet! ");
            }

            return Ok(boughtStocks);

        }

        public async Task<IActionResult> GetUserSoldStocks(string userName)
        {
            var soldStocks = await _transaction.GetSoldStocks(userName);
            if (soldStocks.Count < 1)
            {
                return BadRequest("You have not bought a stock yet! ");
            }

            return Ok(soldStocks);

        }

        public async Task<IActionResult> GetUserStockToBeSold(StockDetails stockDetails)
        {
            var stockToBeSoldDB = await _transaction.GetUserStock(stockDetails);
            if (stockToBeSoldDB == null)
            {
                return BadRequest("Stock not found");
            }

            return Ok(stockToBeSoldDB);

        }
    }
}
