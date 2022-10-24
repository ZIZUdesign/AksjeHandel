using AksjeHandel.Models;
using Microsoft.EntityFrameworkCore;

namespace AksjeHandel.DAL
{
    public class AksjeContext : DbContext
    {
        public  AksjeContext(DbContextOptions<AksjeContext> options) : base (options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Here goes database configuration 

        //}

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BoughtStock> BoughtStocks { get; set; }
        public DbSet<SoldStock> SoldStocks{ get; set; }
        public DbSet<Wallet> Wallets { get; set; }


       


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
