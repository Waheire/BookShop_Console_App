using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop_Console_App.Services.IService
{
    public interface IUser
    {
        //login user 
        Task<string> LoginUserAsync();
        //get all users
        Task<List<User>> GetAllUsersAsync();
        //get user by Id
        Task<User> GetUserByIdAsync(int id);
        //get user by Name
        Task<User> GetUserByNameAsync(string username);
        //create user
        Task<string> AddUserAsync(AddUser newUser);
        //update user
        Task<string> UpdateUserAsync(int id, AddUser updatedUser);
        //delete User
        Task<string> DeleteUserAsync(int id);
    }
}
