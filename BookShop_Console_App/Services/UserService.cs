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
    internal class UserService : IUser
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://localhost:3000/Users";

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> AddUserAsync(AddUser newUser)
        {
            var content = JsonConvert.SerializeObject(newUser);
            //convert to string
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, body);
            if (response.IsSuccessStatusCode)
            {
                return "User added Successfully";
            }
            return "";
        }

        public async Task<string> DeleteUserAsync(int id)
        {
           var response = await _httpClient.GetAsync(_url+"/"+id);
            if (response.IsSuccessStatusCode)
            {
                return "User Deleted Successfully!";
            }
            return "";

        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            //read content
            var content = await response.Content.ReadAsStringAsync();
            //convert string to list
            var users = JsonConvert.DeserializeObject<List<User>>(content);
            if (response.IsSuccessStatusCode && users != null)
            {
                return users;
            }
            return new List<User> { new User() };
        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_url + "/" + id);
            //read content
            var content = await response.Content.ReadAsStringAsync();
            //convert string of one user to User Data Type
            var user = JsonConvert.DeserializeObject<User>(content);
            if (response.IsSuccessStatusCode && user != null)
            {
                return user;
            }
            return new User();
        }

       

        public async Task<User> GetUserByNameAsync(string username)
        {
            var response = await _httpClient.GetAsync(_url + "/" + username);
            //read content
            var content = await response.Content.ReadAsStringAsync();
            //convert string of one user to User Data Type
            var user = JsonConvert.DeserializeObject<User>(content);
            if (response.IsSuccessStatusCode && user != null)
            {
                return user;
            }
            return new User();
        }

        public Task<string> LoginUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateUserAsync(int id, AddUser updatedUser)
        {
            var content = JsonConvert.SerializeObject(updatedUser);
            //convert to string
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url + "/" + id, body);
            if (response.IsSuccessStatusCode)
            {
                return "User Updated Successfully";
            }
            return "";
        }
    }
}
