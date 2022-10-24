using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.Models
{
    public class UserManagerResponse
    {
        public int UserId { get; set; }
        public int  StockId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }


    }
}
