using Database;
using Domain;
using Domain.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            var stock = stocks.FirstOrDefault(s => s.ProductId == productInStock.ProductId);

            if (stock != null)
            {
                int newQuantity = stock.ProductQuantity + productInStock.ProductQuantity;

                double newPrice = ((stock.ProductPrice * stock.ProductQuantity) +
                (productInStock.ProductPrice * productInStock.ProductQuantity))
                / (newQuantity);

                List<Stock> tempstock = DbContext<Stock>.ReadJson(Paths.stock);
                foreach (Stock s in tempstock)
                {
                    if (s.StockId == stock.StockId)
                    {
                        s.ProductPrice = newPrice;
                        s.ProductQuantity = newQuantity;
                    }
                }
                DbContext<Stock>.WriteJson(tempstock, Paths.stock);

                string logtext = $"Bye product by id {productInStock.ProductId} \n" +
                                 $"Last Quantity is {stock.ProductQuantity} new Quantity is {newQuantity}\n" +
                                 $"Last price is {stock.ProductPrice} and new Price is {newPrice} ";
                Logs.Log(logtext);


                return $"The {stock.StockName} was updated.";
            }
            else
            {
                productInStock.StockId = stocks.Max(r => r.StockId) + 1;
                var product = products.FirstOrDefault(p => p.ProductId == productInStock.ProductId);
                if (product != null)
                {
                    productInStock.ProductId = product.ProductId;
                    stocks.Add(productInStock);
                    DbContext<Stock>.WriteJson(stocks, Paths.stock);
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
            using (TextWriter writer = File.CreateText(Paths.GetSalesProductlist))
            {
                foreach (var SPL in SalesProductList)
                {
                    writer.WriteLine($"StockName: {SPL.StockName}   ProductName: {SPL.ProductName}   ProductPrice:{SPL.ProductPrice}   ProductQuantity: {SPL.ProductQuantity}   Barcode: {SPL.Barcode}");

                }
            }


            return SalesProductList;
        }

        public string SaleProduct(int productId, int cnt)
        {
            var stock = stocks.FirstOrDefault(p => p.ProductId == productId);
            if (stock != null)
            {
                int quantity = GetProductQuantity(productId);
                if (quantity > cnt)
                {
                    int temp_Quantity = stock.ProductQuantity - cnt;

                    List<Stock> tempstock = DbContext<Stock>.ReadJson(Paths.stock);
                    foreach (Stock s in tempstock)
                    {
                        if (s.StockId == stock.StockId)
                        {
                            s.ProductQuantity = temp_Quantity;
                        }
                    }
                    DbContext<Stock>.WriteJson(tempstock, Paths.stock);

                    string logtext = $"Sale in product by id {productId} in {cnt} item\n" +
                        $"Last Quantyty is {quantity} new Quantyty is {temp_Quantity} ";
                    Logs.Log(logtext);

                    return $"{cnt} items of {stock.StockName} were sold successfully";
                }
                else
                {
                    return "Insufficient stock";
                }
            }
            else { return "This product is not in stock"; }
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
