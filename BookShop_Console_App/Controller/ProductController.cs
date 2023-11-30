using BookShop_Console_App.Services;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop_Console_App.Controller
{
    public class ProductController
    {

        ProductService productService = new ProductService();
        OrderController orderController = new OrderController();    


        public async Task ProductInit()
        {
            Console.WriteLine("===== Manage Products =====");
            Console.WriteLine("1. View All Products");
            Console.WriteLine("2. Add A Product");
            Console.WriteLine("3. Update A Product");
            Console.WriteLine("4. Delete A Product");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===== Manage Orders =====");
            Console.WriteLine("5. View All Orders");

            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();
            //convert to int
            var output = int.TryParse(choice, out int option);

            //switch
            await RedirectUser(option);


        }

        public async Task RedirectUser(int option)
        {
            switch (option)
            {
                case 1:
                    await ShowProducts();
                    break;
                case 2:
                    await AddProductView();
                    break;
                case 3:
                    await UpdateProductView();
                    break;
                case 4:
                    await DeleteProduct();
                    break;
                case 5:
                    await orderController.ShowOrders();
                    break;
            }

            Console.WriteLine();
        }

        //add product view
        public async Task AddProductView()
        {
            Console.WriteLine("======== Add a new Product ========");
            Console.WriteLine();
            Console.WriteLine("Enter Name: ");
            var Name = Console.ReadLine();
            Console.WriteLine("Enter Description: ");
            var Description = Console.ReadLine();
            Console.WriteLine("Enter Price: ");
            var ProductPrice = Console.ReadLine();
            var res = int.TryParse(ProductPrice, out int Price);

            var newProduct = new AddProduct() { Name = Name, Description = Description, Price = Price };
            await AddProductRequest(newProduct);
        }

        //add product 
        public async Task AddProductRequest(AddProduct newProduct)
        {
            //communicate with service
            var response = await productService.AddProductAsync(newProduct);
            Console.WriteLine(response);
            await ProductInit();
        }

        //show all products
        public async Task ShowProducts()
        {
            var products = await productService.GetProductsAsync();
            Console.WriteLine($"Id  \t Name \t Description \t Price");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}  \t {product.Name}  \t {product.Description}  \t {product.Price}");
            }
        }

        //show all products
        public async Task ShowAllProducts()
        {
            var products = await productService.GetProductsAsync();
            //return products;
        }

        //update product view 
        public async Task UpdateProductView()
        {
            await ShowProducts();

            Console.WriteLine("Select Product to Update by Id: ");
            var productId = Console.ReadLine();
            var output = int.TryParse(productId, out int ProductId);
            Console.WriteLine("Product Name: ");
            var Name = Console.ReadLine();
            Console.WriteLine("Product Description: ");
            var Description = Console.ReadLine();
            Console.WriteLine("Product Price: ");
            var ProductPrice = Console.ReadLine();
            var res = int.TryParse(ProductPrice, out int Price);

            var updatedProduct = new AddProduct() { Name = Name, Description = Description, Price = Price };
            await updateProductRequest(ProductId, updatedProduct);
        }

        public async Task updateProductRequest(int productId, AddProduct updatedProduct) 
        {
            var response = await productService.UpdateProductAsync(productId, updatedProduct);
            Console.WriteLine(response);
            await ProductInit();
        }

        //delete product 
        public async Task DeleteProduct() 
        {
            await ShowProducts();
            Console.WriteLine("Select Product to delete by Id : ");
            var productId = Console.ReadLine();
            var output = int.TryParse(productId, out int ProductId);
            var response = await productService.DeleteProductAsync(ProductId);
            Console.WriteLine(response);
            await ProductInit();
        }
    }
}
