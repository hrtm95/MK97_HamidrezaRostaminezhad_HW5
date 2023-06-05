using Database;
using Domain;
using Domain.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class StockRepository : IStockRepository
    {

        private readonly List<Product> products = DbContext<Product>.ReadJson(Paths.product);
        private readonly List<Stock> stocks = DbContext<Stock>.ReadJson(Paths.stock);

        public string BuyProduct(Stock productInStock)
        {

            var stock = stocks.FirstOrDefault (s => s.StockName == productInStock.StockName);

            if (stock != null)
            {
                stock.ProductQuantity = stock.ProductQuantity + productInStock.ProductQuantity;

                double productPrice = ((stock.ProductPrice * stock.ProductQuantity)+
                    (productInStock.ProductPrice * productInStock.ProductQuantity))                    
                    / (stock.ProductQuantity + productInStock.ProductQuantity);

                stocks.Remove(stocks.FirstOrDefault(s => s.StockName == productInStock.StockName));
                stocks.Add(stock);
                DbContext<Stock>.WriteJson(stocks, Paths.stock);
                return $"The {stock.StockName} was updated.";
            }
            else
            {
                productInStock.StockId = stocks.Max(r => r.StockId) + 1;

                var product = (from p in products
                               where p.ProductName == productInStock.StockName
                               select p).FirstOrDefault();
                if (product != null)
                {
                    productInStock.ProductId = product.ProductId;
                    stocks.Add(productInStock);
                    DbContext<Stock>.WriteJson (stocks, Paths.stock);
                    return $"The {product.ProductName} was added to stock.";
                }
                else
                {
                    return $"The {productInStock.StockName} was not in the products list.";
                }
            }
        }

        public List<StockProductViewModel> GetSalesProductList()
        {
            var SalesProductList = (from p in products
                                    join s in stocks
                                    on p.ProductId equals s.ProductId
                                    select new StockProductViewModel()
                                    {
                                        StockId = s.StockId,
                                        StockName = s.StockName,
                                        ProductId = s.ProductId,
                                        ProductQuantity = s.ProductQuantity,
                                        ProductPrice = s.ProductPrice,
                                        ProductName = p.ProductName,
                                        Barcode = p.Barcode,
                                    }).ToList();
            return SalesProductList;
        }

        public string SaleProduct(int productId, int cnt)
        {
            var stock = stocks.FirstOrDefault(p => p.ProductId == productId);
            int quantity = GetProductQuantity(productId);
            if (quantity > cnt)
            {
                stock.ProductQuantity = stock.ProductQuantity-cnt;
                stocks.Remove(stocks.FirstOrDefault(s => s.StockName == stock.StockName));
                stocks.Add(stock);
                DbContext<Stock>.WriteJson(stocks, Paths.product);
                return $"{cnt} items of {stock.StockName} were sold successfully";
            }
            else
            {
                return "Insufficient stock";
            }
        }
        private int GetProductQuantity(int productId)
        {
            var quantity = (from s in stocks
                            where s.ProductId == productId
                            select s.ProductQuantity).FirstOrDefault();
            return quantity;
        }
    }
}
