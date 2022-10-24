using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }
        public decimal Withdrawal { get; set; }
        public string  UserName { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

       
    }
}
