using AksjeHandel.Models;
using AksjeHandel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Interfaces
{
    public interface ITransaction
    {
        Task<UserManagerResponse> BuyStock(BuyViewModel buyViewModel);
        Task<UserManagerResponse> SellStock(SellViewModel sellViewModel);
        Task<List<BoughtStockVM>> GetBoughtStocks(string UserName);
        Task<List<SoldStockVM>> GetSoldStocks(string UserName);

        Task<BoughtStockVM> GetUserStock(StockDetails stockDetails );
    }
}
