using Database;
using Domain;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class StockRepository : IStockRepository
    {

        private List<Product> products = DbContext<Product>.ReadJson

        public string BuyProduct(Stock productInStock)
        {
            
            throw new NotImplementedException();
        }

        public List<StockProductViewModel> GetSalesProductList()
        {
            throw new NotImplementedException();
        }

        public string SaleProduct(int productId, int cnt)
        {
            throw new NotImplementedException();
        }
    }
}
