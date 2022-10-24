using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AksjeHandel.ViewModel
{
    public class BoughtStockVM
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public String CompanyName { get; set; }
        public string Message { get; set; }


    }
}
