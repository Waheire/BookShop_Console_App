using BookShop_Console_App.Services.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop_Console_App.Services
{
    internal class ProductService : IProduct
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://localhost:3000/Products";


        public ProductService()
        {
            _httpClient = new HttpClient();
        }


        public async Task<string> AddProductAsync(AddProduct newProduct)
        {
          
            var content = JsonConvert.SerializeObject(newProduct);
            //convert to string
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, body);
            if (response.IsSuccessStatusCode) 
            {
                return "Product added Successfully";
            }
            return "";
        }

        public async Task<string> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_url + "/"+ id);
            if (response.IsSuccessStatusCode) 
            {
                return "Product Deleted Successfully!";
            }
            return "";
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_url+ "/"+ id);
            //read content
            var content = await response.Content.ReadAsStringAsync();
            //convert string of one product to Product Data Type
            var product = JsonConvert.DeserializeObject<Product>(content);
            if (response.IsSuccessStatusCode && product != null)
            {
                return product;
            }
            return new Product();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            //read content
            var content = await response.Content.ReadAsStringAsync();
            //convert string to list
            var products = JsonConvert.DeserializeObject<List<Product>>(content);
            if (response.IsSuccessStatusCode && products != null) 
            {
                return products;
            }
            return new List<Product> { new Product() };
        }

        public async Task<string> UpdateProductAsync(int id, AddProduct updatedProduct)
        {
            var content = JsonConvert.SerializeObject(updatedProduct);
            //convert to string
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url+"/"+id, body);
            if (response.IsSuccessStatusCode)
            {
                return "Product Updated Successfully";
            }
            return "";
        }
    }
}
