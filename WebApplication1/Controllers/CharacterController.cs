using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class CharacterController : Controller
    {
        private readonly HttpClient httpClient;

        public CharacterController()
        {
            httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var apiUrl = "https://rickandmortyapi.com/api/character";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var characters = JsonConvert.DeserializeObject<ApiResponse<Character>>(content);
                return View(characters.Results);
            }

            return View(new List<Character>());
        }

        public class ApiResponse<T>
        {
            public Info Info { get; set; }
            public List<T> Results { get; set; }
        }

        public class Info
        {
            public int Count { get; set; }
            public int pages { get; set; }
            public string Next { get; set; }
            public string Prev { get; set; }
        }
    }
}
