
using Domain;

namespace Interface
{
    public interface IProductRepository
    {
        bool AddProduct(Product product);
        //string ?? AddProduct(Product product);
        List<Product>? GetProductList();
        string GetProductById(int id);

    }
}
