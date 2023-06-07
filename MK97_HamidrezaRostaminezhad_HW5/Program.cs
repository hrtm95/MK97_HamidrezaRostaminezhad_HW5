// See https://aka.ms/new-console-template for more information
using Database;
using Domain;
using Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IStockRepository, StockRepository>();
}).Build();

var Product = host.Services.GetService<IProductRepository>();
var Stock = host.Services.GetRequiredService<IStockRepository>();

Console.WriteLine("Hello, World!");

#region

//string path = @"..\..\..\..\Database\Product.Json";

//List<Product> products = new List<Product>()
//{

//    new Product(){ProductId = 1 , Barcode = "barcode1", ProductName = "name1"},
//    new Product(){ProductId = 2 , Barcode = "barcode2", ProductName = "name2"},
//    new Product(){ProductId = 3 , Barcode = "barcode3", ProductName = "name3"},
//    new Product(){ProductId = 4 , Barcode = "barcode4", ProductName = "name4"}
//};
//DbContext<Product>.WriteJson(products, path);
//List<Product>? products =  DbContext<Product>.ReadJson(path);
//foreach(Product product in products)
//{
//    Console.WriteLine(product.ProductName);
//}
//string path = @"..\..\..\..\Database\Stoc.Json";

//List<Stock> stocks = new List<Stock>()
//{

//    new Stock(){StockId = 1 ,StockName = "Stock1", ProductId = 1 , ProductPrice = 1000,ProductQuantity = 3},
//    new Stock(){StockId = 2 ,StockName = "Stock2", ProductId = 2 , ProductPrice = 2000,ProductQuantity = 4}
//};
//DbContext<Stock>.WriteJson(stocks, path);
//List<Stock>? stock = DbContext<Stock>.ReadJson(path);
//foreach (Stock s in stock)
//{
//    Console.WriteLine(s.StockName);
//}
#endregion 
//StockRepository stockRepository = new StockRepository();
//Console.WriteLine(
//stockRepository.BuyProduct(new Stock()
//{
//    ProductId = 5,ProductPrice = 1000 ,ProductQuantity = 1
//}));
//Console.WriteLine(
//stockRepository.SaleProduct(2, 8));


//Stock.GetSalesProductList();

while (true)
{
    Console.Clear();
    Console.WriteLine("Press number list for  continue:");
    Console.WriteLine("1 - Add Product");
    Console.WriteLine("2 - Get Product List");
    Console.WriteLine("3 - Get Product By Id");
    Console.WriteLine("4 - Buy Product");
    Console.WriteLine("5 - Sale Product");
    Console.WriteLine("6 - Get Sales Product List");
    Console.WriteLine("7 - Exit;");
    string fanc = Console.ReadLine();
    if (fanc == "1")
    {
        Console.Clear();
        Console.WriteLine("Enter Product name like ( Hamid_123 ):");
        string pname = Console.ReadLine();
        Console.WriteLine("Enter barcode: ");
        string barcode = Console.ReadLine();
        var pd = Product.GetProductList().FirstOrDefault(p => p.ProductName == pname);
        Console.Clear();
        if (pd != null)
        {
            Console.WriteLine($"{pname} is Exist.");
            Console.WriteLine("press any kye to show menu:");
            Console.ReadKey();

        }
        else
        {
            Product product = new Product() { ProductName = pname, Barcode = barcode };
            if (Product.AddProduct(product))
                Console.WriteLine($"{pname} Add is Succsess.");
            else
                Console.WriteLine($"{pname} is not valid name.");

            Console.WriteLine("Press any kye to show menu:");
            Console.ReadKey();
        }
    }
    else if (fanc == "2")
    {
        Console.Clear();
        List<Product> products = Product.GetProductList();
        foreach (var p in products)
        {
            Console.WriteLine($"ID: {p.ProductId} - Name: {p.ProductName} - Barcode: {p.Barcode}");
        }
        Console.WriteLine("\n\nPress any kye to show menu:");
        Console.ReadKey();
    }
    else if (fanc == "3")
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter Id to show Product:");
        if (int.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            var pname = Product.GetProductById(id);
            if (pname != null)
            {
                Console.WriteLine($"The name of ID {id} is {pname}");
            }
            else
            {
                Console.WriteLine($"ID {id} not Exist");
            }
            Console.WriteLine("press any kye to show  menu");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("!press true function!");
            Console.WriteLine("press any kye to show  menu");
            Console.ReadKey();
        }
    }
    else if (fanc == "4")
    {
        Console.Clear();
        Console.WriteLine("Enter a Stock Name");
        string sname = Console.ReadLine();
        Console.WriteLine("Enter ProductId");
        int pid;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out pid))
                break;
            else
            {
                Console.Clear();
                Console.WriteLine("Press valid number:");
            }
        }
        Console.WriteLine("Enter Quantity of that:");
        int Qtt;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out Qtt))
                break;
            else
            {
                Console.Clear();
                Console.WriteLine("Press valid number:");
            }
        }
        Console.WriteLine("Enter Price of that:");
        int price;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out price))
                break;
            else
            {
                Console.Clear();
                Console.WriteLine("Press valid number:");
            }
        }

        Stock stock = new Stock() { ProductId = pid ,StockName = sname,ProductQuantity = Qtt,ProductPrice = price};
        Console.WriteLine( Stock.BuyProduct(stock));
        Console.WriteLine("Press any kye to show menu:");
        Console.ReadKey();

    }
    else if (fanc == "5")
    {
        Console.Clear();
        Console.WriteLine("Enter ProductId");
        int pid;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out pid))
                break;
            else
            {
                Console.Clear();
                Console.WriteLine("Press valid number:");
            }
        }
        Console.WriteLine("Enter Quantity To sale:");
        int Qtt;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out Qtt))
                break;
            else
            {
                Console.Clear();
                Console.WriteLine("Press valid number:");
            }
        }
        Console.Clear();
        Console.WriteLine( Stock.SaleProduct(pid,Qtt));
        Console.WriteLine("Press any kye to show menu:");
        Console.ReadKey();

    }
    else if (fanc == "6")
    {
        Stock.GetSalesProductList();
        Console.WriteLine("You can find this Sales Product List in Txt on Database.");
    }
    else if (fanc == "7")
    {
        Console.WriteLine("press any kye to exit.");
        break;
    }
    else
    {
        Console.Clear();
        Console.WriteLine("--press true function--");
        Console.WriteLine("press any kye to show  menu");
        Console.ReadKey();
    }
}