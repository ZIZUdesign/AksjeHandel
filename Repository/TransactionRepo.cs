using AksjeHandel.DAL;
using AksjeHandel.Interfaces;
using AksjeHandel.Models;
using AksjeHandel.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Repository
{
    public class TransactionRepo : ITransaction
    {
        private readonly AksjeContext _context;

        public TransactionRepo(AksjeContext context)
        {
            _context = context;
        }

       

        public async Task<UserManagerResponse> BuyStock(BuyViewModel buyViewModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == buyViewModel.userName);
            if (user == null)
                return new UserManagerResponse
                { 
                  Message = "User not found"
                };
            var stockToBeBought = _context.Stocks.FirstOrDefault(s => s.Id == buyViewModel.stockId);
            if (stockToBeBought == null)
                return new UserManagerResponse
                {
                    Message = "The stock is Sold out"
                };
           

            // check if the user has sufficient balance in his/her wallet 
            var userWallet = _context.Wallets.FirstOrDefault(w => w.UserName == buyViewModel.userName);
            if (userWallet.Balance < (stockToBeBought.Price * buyViewModel.quantity))
            {
                return new UserManagerResponse
                {
                    Message = "You do not have sufficient balance.... make a deposit first"
                };
            }

            var boughtStock = _context.BoughtStocks.FirstOrDefault(bs => bs.Id == buyViewModel.stockId);
            if (boughtStock != null)
            {
                //await UpdateBoughtStocksDB(buyViewModel);
                boughtStock.Id = boughtStock.Id;
                boughtStock.StockName = boughtStock.StockName;
                stockToBeBought.UserName = buyViewModel.userName;
                boughtStock.Price = boughtStock.Price;
                boughtStock.CompanyName = boughtStock.CompanyName;
                boughtStock.Quantity = boughtStock.Quantity + buyViewModel.quantity;
                boughtStock.Description = boughtStock.Description;
            }
            else
            {
                var newStock = new BoughtStock()
                {
                    Id = stockToBeBought.Id,
                    StockName = stockToBeBought.StockName,
                    Description = stockToBeBought.Description,
                    CompanyName = stockToBeBought.CompanyName,
                    Quantity = buyViewModel.quantity,
                    Price = stockToBeBought.Price,
                    UserName = buyViewModel.userName

                };
                user.BoughtStockItems.Add(newStock);
                _context.BoughtStocks.Add(newStock);
            }

            userWallet.Balance = userWallet.Balance - (stockToBeBought.Price * buyViewModel.quantity);
            await _context.SaveChangesAsync();

            return new UserManagerResponse
            {
                StockId = stockToBeBought.Id,
                UserId = user.UserId,
                UserName = user.UserName,
                IsSuccess = true,
                Message = "Transaction completed succeffully"

            };

        }

       

        public async Task<UserManagerResponse> SellStock(SellViewModel sellViewModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == sellViewModel.UserName);
            var userWallet = _context.Wallets.FirstOrDefault(w => w.UserName == sellViewModel.UserName);
            
            if (user == null )
                return new UserManagerResponse
                { 
                  Message = "User not found"
                };
            
            var boughtStocks = _context.BoughtStocks.Where(b => b.UserName == sellViewModel.UserName).ToList();
            var stockToBeSold = boughtStocks.FirstOrDefault(b => b.Id == sellViewModel.StockId);
            if (stockToBeSold == null)
                return new UserManagerResponse
                {
                    Message = "Stock not found"
                };

            if (stockToBeSold.Quantity >= sellViewModel.Quantity)
            {
                if (stockToBeSold.Quantity != sellViewModel.Quantity)
                {
                    await UpdateDB(sellViewModel);
                }
                else
                {

                    //boughtStocks.Remove(stockToBeSold);
                    user.BoughtStockItems.Remove(stockToBeSold);
                    _context.BoughtStocks.Remove(stockToBeSold);
                }
            }
            else
            {
                return new UserManagerResponse
                {
                    Message = "You have less stocks"
                };
            }

            var existingSoldStock = _context.SoldStocks.FirstOrDefaultAsync(st => 
                          st.Id == sellViewModel.StockId);
            if (existingSoldStock != null)
            {
                await UpdatSoldStocksDB(sellViewModel);
            }

            else
            {
                var soldStock = new SoldStock
                {
                    Id = stockToBeSold.Id,
                    StockName = stockToBeSold.StockName,
                    Description = stockToBeSold.Description,
                    CompanyName = stockToBeSold.CompanyName,
                    Quantity = sellViewModel.Quantity,
                    Price = stockToBeSold.Price,  /// price should have been modified ....
                    UserName = sellViewModel.UserName

                };

               await _context.SoldStocks.AddAsync(soldStock);

            }

           

          userWallet.Balance += (stockToBeSold.Price * sellViewModel.Quantity);
           
          await _context.SaveChangesAsync();

            return new UserManagerResponse
            {
                StockId = stockToBeSold.Id,
                UserId = user.UserId,
                UserName = user.UserName,
                IsSuccess = true,
                Message = "Transaction completed succeffully"
            };
            
        }

        public async Task<List<BoughtStockVM>> GetBoughtStocks(string userName)
        {
            try
            {
                var boughtStocksDB = await _context.BoughtStocks.Where(stock => stock.UserName == userName).ToListAsync();
                var boughtStocks = new List<BoughtStockVM>();

                //boughtStocksDB.ForEach(stock => new BoughtStockVM
                //{
                //    StockName = stock.StockName,
                //    CompanyName = stock.CompanyName,
                //    Description = stock.Description,
                //    Price = stock.Price,
                //    Quantity = stock.Quantity
                //});

                foreach (var stock in boughtStocksDB)
                {
                    var  userStocks = new BoughtStockVM 
                    {
                        Id = stock.Id,
                        StockName = stock.StockName,
                        CompanyName = stock.CompanyName,
                        Description = stock.Description,
                        Price = stock.Price,
                        Quantity = stock.Quantity
                    };

                    boughtStocks.Add(userStocks);

                }

                return boughtStocks;

               

            }
            catch
            {
                return null;

            }
           

        }

        public async Task<List<SoldStockVM>> GetSoldStocks(string userName)
        {
            try
            {
                var soldStocksDB = await _context.SoldStocks.Where(stock =>
                        stock.UserName == userName).ToListAsync();
                var soldStocks = new List<SoldStockVM>();

                //boughtStocksDB.ForEach(stock => new BoughtStockVM
                //{
                //    StockName = stock.StockName,
                //    CompanyName = stock.CompanyName,
                //    Description = stock.Description,
                //    Price = stock.Price,
                //    Quantity = stock.Quantity
                //});

                foreach (var stock in soldStocksDB)
                {
                    var userStocks = new SoldStockVM
                    {
                        Id = stock.Id,
                        StockName = stock.StockName,
                        CompanyName = stock.CompanyName,
                        Description = stock.Description,
                        Price = stock.Price,
                        Quantity = stock.Quantity
                    };

                    soldStocks.Add(userStocks);

                }

                return soldStocks;



            }
            catch
            {
                return null;

            }


        }


        public async Task<BoughtStockVM> GetUserStock(StockDetails stockDetails)
        {
            try
            {
                var boughtStocksDB = await _context.BoughtStocks.Where(stock => stock.UserName == stockDetails.UserName).ToListAsync();
                var stockToBeSoldDB = boughtStocksDB.FirstOrDefault(stock => stock.Id == stockDetails.Id);

              

                    var stockToBeSold = new BoughtStockVM
                    {
                        Id = stockToBeSoldDB.Id,
                        StockName = stockToBeSoldDB.StockName,
                        CompanyName = stockToBeSoldDB.CompanyName,
                        Description = stockToBeSoldDB.Description,
                        Price = stockToBeSoldDB.Price,
                        Quantity = stockToBeSoldDB.Quantity

                    };

                    return stockToBeSold;

          
            }


            catch
            {
                return null;

            }



        }

        private async Task<bool> UpdateDB(SellViewModel sellViewModel)
        {
           try
            {
                var boughtStocks = _context.BoughtStocks.Where(b => b.UserName == sellViewModel.UserName).ToList();
                var stockToBeSold = boughtStocks.FirstOrDefault(b => b.Id == sellViewModel.StockId);

                stockToBeSold.Id = sellViewModel.StockId;
                stockToBeSold.StockName = stockToBeSold.StockName;
                stockToBeSold.UserName = sellViewModel.UserName;
                stockToBeSold.Price = stockToBeSold.Price;
                stockToBeSold.CompanyName = stockToBeSold.CompanyName;
                stockToBeSold.Quantity = stockToBeSold.Quantity - sellViewModel.Quantity;
                stockToBeSold.Description = stockToBeSold.Description;

                await _context.SaveChangesAsync();
                return true;

            }
            catch 
            {
                return false; 
            }

        }

        private async Task<bool> UpdatSoldStocksDB(SellViewModel sellViewModel)
        {
            try
            {
                var soldStocks = _context.SoldStocks.Where(b => b.UserName == sellViewModel.UserName).ToList();
                var stockToBeSold = soldStocks.FirstOrDefault(b => b.Id == sellViewModel.StockId);
               
                if (stockToBeSold != null)
                {
                    stockToBeSold.Id = stockToBeSold.Id;
                    stockToBeSold.StockName = stockToBeSold.StockName;
                    stockToBeSold.UserName = sellViewModel.UserName;
                    stockToBeSold.Price = stockToBeSold.Price;
                    stockToBeSold.CompanyName = stockToBeSold.CompanyName;
                    stockToBeSold.Quantity = stockToBeSold.Quantity + sellViewModel.Quantity;
                    stockToBeSold.Description = stockToBeSold.Description;
                }
               
                await _context.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }







        }
    }
}
