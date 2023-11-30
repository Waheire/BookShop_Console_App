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
    public class OrderService : IOrder
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://localhost:3000/Orders";

        public OrderService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> AddOrderAsync(AddOrder newOrder)
        {
            var content = JsonConvert.SerializeObject(newOrder);
            //convert to string
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, body);
            if (response.IsSuccessStatusCode)
            {
                return "Order added Successfully";
            }
            return "";
        }

        public async Task<string> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_url+"/"+id);
            if (response.IsSuccessStatusCode) 
            {
                return "Order Deleted Successfully";
            }
            return "";
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            //read content
            var content = await response.Content.ReadAsStringAsync();
            //convert string to list
            var orders = JsonConvert.DeserializeObject<List<Order>>(content);
            if (response.IsSuccessStatusCode && orders != null)
            {
                return orders;
            }
            return new List<Order> { new Order() };
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_url + "/" + id);
            //read content
            var content = await response.Content.ReadAsStringAsync();
            //convert string of one product to Product Data Type
            var order = JsonConvert.DeserializeObject<Order>(content);
            if (response.IsSuccessStatusCode && order != null)
            {
                return order;
            }
            return new Order();
        }

        public async Task<string> UpdateOrderAsync(int id, AddOrder updatedOrder)
        {
            var content = JsonConvert.SerializeObject(updatedOrder);
            //convert to string
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url+"/"+id, body);
            if (response.IsSuccessStatusCode)
            {
                return "Order Updated Successfully";
            }
            return "";
        }
    }
}
