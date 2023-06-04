using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interface
{
    public class ProductRepository : IProductRepository
    {
        public string AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public string GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductList()
        {
            throw new NotImplementedException();
        }
        private bool CheckProductName(string ProductName)
        {
            if (Regex.IsMatch(ProductName, @"^[A-Z]+([a-z]{3})+.+_+([\d]{3})$"))
                return true;
            return false;
        }
    }
}
