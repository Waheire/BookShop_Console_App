using BookShop_Console_App.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop_Console_App.Controller
{
    internal class UserController
    {
       
        UserService userService = new UserService();
        ProductController productController = new ProductController();
        OrderController orderController = new OrderController();

        public async Task UserInit()
        {
            Console.WriteLine("===== Welcome to the Bookshop  =====");
            Console.WriteLine("1. Login");
          
            Console.WriteLine("2. Register");

            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();
            //convert to int
            var output = int.TryParse(choice, out int option);

            //switch
            await RedirectUser(option);

        }

        public async Task RedirectUser(int option)
        {
            if (option == 1) 
            { 
                await UserLoginView(); 
            } 
            else if (option == 2)
            {
                await RegisterUser();
            }
            else
            {
                Console.WriteLine("Invalid input. Please Try again.");
                await UserInit();
            }
           
        }

        //login as user 
        public async Task UserLoginView() 
        {
            Console.WriteLine("===== Login =====");
            Console.WriteLine("Enter Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            var password = Console.ReadLine();

           
                await Login(username, password);
        }

        public async Task Login(string username, string password)
        {
            //get all users
            var users = await userService.GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                if (user.Username == "admin")
                {
                    await AdminView(user.Id);
                }
                else
                {
                    await userView(user.Id);
                }
            }
            else 
            {
                Console.WriteLine("Invalid credentials. Please try again ");
                await UserInit();
            }
        }

        public async Task  AdminView(int id)
        {
            Console.WriteLine("===== Loggedin ss Admin =====");

            await productController.ProductInit();
        }

        //user view 
        public async Task userView(int id) 
        {
            //show all products
           await productController.ShowProducts();
            //buy product
           await orderController.makeOrderView(id);

        }

        //register 
        public async Task RegisterUser() 
        {
            Console.WriteLine("======== Register ========");
            Console.WriteLine();
            Console.WriteLine("Enter Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Enter Email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            var password = Console.ReadLine();
            

            var newUser = new AddUser() { Username = username, Email = email, Password = password };
            await AddUserRequest(newUser);
        }

        //register user request
        public async Task AddUserRequest(AddUser addUser) 
        {
            //communicate with service
            var response = await userService.AddUserAsync(addUser);
            Console.WriteLine(response);
            await UserInit();
        }

        //show all users
        public async Task ShowAllUsers()
        {
            var products = await userService.GetAllUsersAsync();
            //return products;
        }


        //update user 
        //public Task UpdateUser() { }

        //delete user 
        //public Task DeleteUser() { } 


    }
}
