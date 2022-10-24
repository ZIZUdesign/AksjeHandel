using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte [] PasswordHash { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual List<BoughtStock> BoughtStockItems  { get; set; }
        public virtual List<SoldStock> SoldStockItems { get; set; }


    }
}
