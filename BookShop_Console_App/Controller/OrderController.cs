using BookShop_Console_App.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop_Console_App.Controller
{
    public class OrderController
    {
        OrderService orderService = new OrderService();




        //show all orders
        //show order by userId
        //make an order
        public async Task makeOrderView(int userId) 
        {
            Console.WriteLine("======== Buy Product ========");
            Console.WriteLine("Select Product by Id: ");
            var itemId = Console.ReadLine();
            var output = int.TryParse(itemId, out int ItemId);

            var newOrder = new AddOrder() { userId = userId, productId = ItemId  };
            await AddOrderRequest(newOrder, userId);
        }

        public async Task AddOrderRequest(AddOrder newOrder, int id)
        {
            //communicate with service
            var response = await orderService.AddOrderAsync(newOrder);
            Console.WriteLine(response);
            await showMyOrders(id);
        }

        //get orders by userId
        public async Task showMyOrders(int userId)
        {
            var orders = await orderService.GetAllOrdersAsync();

            var userOrders = orders.Where(x => x.UserId == userId).ToList();
            Console.WriteLine("======== My Orders ========");
            Console.WriteLine($"Id \t Description \t Price");
            foreach (var order in userOrders)
            {
                Console.WriteLine($"{order.Id}  \t {order.UserId}  \t {order.ProductId}");
            }
            await makeOrderView(userId);

        }

        //show all orders -only for admin
        public async Task showOrdersView() 
        {
            Console.WriteLine("===== Manage Orders =====");
            Console.WriteLine("1. View All Orders");
            //Console.WriteLine("2. Make Order");
            // Console.WriteLine("3. Update Order");
            // Console.WriteLine("4. Delete Order");

            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();
            //convert to int
            var output = int.TryParse(choice, out int option);

            //switch
            await RedirectUser(option);
        }

        private async Task RedirectUser(int option)
        {
            switch (option)
            {
                case 1:
                    await ShowOrders();
                    break;
            }

            Console.WriteLine();
        }

        public async Task ShowOrders()
        {
           
            //var products = await productController.ShowAllProducts();
            //var users = await userController.ShowAllUsers();
            var orders = await orderService.GetAllOrdersAsync();
            //var results = from order in orders
            //             join user in users on order.UserId equals user.Id
            //             join product in products on order.ProductId equals product.Id
            //             select new
            //             {
            //                 OrderId = order.Id,
            //                 Username = user.Username,
            //                 ProductName = product.Name,
            //                 Price = product.Price
            //             };
            Console.WriteLine("======== All Orders ========");
            Console.WriteLine($"Id \t User Id \t Product Id ");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Id}  \t {order.UserId}  \t {order.ProductId} ");
            }

           // await productController.ProductInit();
        }
    }
}
