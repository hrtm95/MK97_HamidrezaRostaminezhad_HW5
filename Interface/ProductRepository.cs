using Database;
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
        private string path = @"..\..\..\..\Database\Product.Json";


        // public string AddProduct(Product product)??
        public bool AddProduct(Product product)
        {
            List<Product>? Products = GetProductList();
            if (CheckProductName(product.ProductName))
            {
                Products.Add(product);
                DbContext<Product>.WriteJson(Products, path);
                return true;
            }                
            return false;
        }

        public string GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product>? GetProductList()
        {
            List<Product>? products =  DbContext<Product>.ReadJson(path);
            return products;
        }
        private bool CheckProductName(string ProductName)
        {
            if (Regex.IsMatch(ProductName, @"^[A-Z]+([a-z]{3})+.+_+([\d]{3})$"))
                return true;
            return false;
        }
    }
}