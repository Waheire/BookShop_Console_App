using BookShop_Console_App.Models;
using BookShop_Console_App.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookShop_Console_App.Services
{
    internal class BookServices : IBook
    {
        private readonly HttpClient _httpClient;
        private readonly string _URL = "http://localhost:3000/books";

        public BookServices() {
        
        _httpClient = new HttpClient();
        }

        public Task<string> AddBook(AddBook newbook)
        {
            
        }

        public async Task<List<Book>> GetAll()
        {
            var response = await _httpClient.GetAsync(_URL);
            var content =  await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(content);
        }
    }
}
