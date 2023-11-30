using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop_Console_App.Services.IService
{
    public interface IOrder
    {
        //get all orders
        Task<List<Order>> GetAllOrdersAsync();
        //get order by Id
        Task<Order> GetOrderByIdAsync(int id);
        //add order
        Task<string> AddOrderAsync(AddOrder newOrder);
        //update order
        Task<string> UpdateOrderAsync( int id, AddOrder updatedOrder);
        //delete order
        Task<string> DeleteOrderAsync(int id);
    }
}
