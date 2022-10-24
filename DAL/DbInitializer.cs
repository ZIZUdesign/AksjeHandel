using AksjeHandel.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.DAL
{
    public class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AksjeContext>();

                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                //context.Database.EnsureCreatedAsync();

            var stocks = new List<Stock>()
            {
                new Stock
                {
                    StockName = "Angular Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 2000,
                    //PictureUrl = "/images/products/sb-ang1.png",
                    Quantity = 100,
                    CompanyName = "Hondia"
                },

                 new Stock
                {
                    StockName = "React Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1000,
                    //PictureUrl = "/images/products/sb-ang1.png",
                    Quantity = 100,
                    CompanyName = "Telsa"
                },

                  new Stock
                {
                    StockName = "Javascript Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 3000,
                    //PictureUrl = "/images/products/sb-ang1.png",
                    Quantity = 100,
                    CompanyName = "Coca-cola"
                },

                   new Stock
                {
                    StockName = "DotNetCore Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 20000,
                    //PictureUrl = "/images/products/sb-ang1.png",
                    Quantity = 100,
                    CompanyName = "DNB"
                },

                    new Stock
                {
                    StockName = "Java Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 5000,
                    //PictureUrl = "/images/products/sb-ang1.png",
                    Quantity = 100,
                    CompanyName = "SpareBank"
                },

                     new Stock
                {
                    StockName = "PHP Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 8000,
                    //PictureUrl = "/images/products/sb-ang1.png",
                    Quantity = 100,
                    CompanyName = "Google"
                },

                      new Stock
                {
                    StockName = "Linux Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 20000,
                    //PictureUrl = "/images/products/sb-ang1.png",
                    Quantity = 100,
                    CompanyName = "Facebook"
                }

            };
                var wallets = new List<Wallet>()
            {
                    new Wallet
                    {
                        Id = 1,
                        Balance = 9000,
                        Deposit = 9000,
                        Withdrawal = 10,
                        UserName = "tdf",
                        UserId = 1
                    }

             };

                foreach (var stock in stocks)
                {
                    context.Stocks.Add(stock);
                }

                context.SaveChanges();



                foreach (var wallet in wallets)
                {
                    context.Wallets.Add(wallet);
                }

                context.SaveChanges();
            }
                

           
        }
    }
}
