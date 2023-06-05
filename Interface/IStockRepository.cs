using Domain;
using Domain.ViewModels;

namespace Interface
{
    public interface IStockRepository
    {
        string SaleProduct(int productId, int cnt);
        string BuyProduct(Stock productInStock);
        List<StockProductViewModel> GetSalesProductList();
    }
}