using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class StockProductViewModel
    {
        //stock
        public int StockId { get; set; }
        public string StockName { get; set; }
        public string ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        //product
        public string ProductName { get; set; }
        public string Barcode { get; set; }
    }
}
