using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookShop_Console_App.Services.IService
{
    public interface IProduct
    {
        //get all products
        Task<List<Product>> GetProductsAsync();
        //get product by Id
        Task<Product> GetProductByIdAsync(int id);
        //add product
        Task<string> AddProductAsync(AddProduct newProduct);
        //update product
        Task<string> UpdateProductAsync(int id, AddProduct updatedProduct);
        //delete product
        Task<string> DeleteProductAsync(int id);
    }
}
