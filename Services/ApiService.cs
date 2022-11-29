using NewsApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Services
{
    public class ApiService
    {
        public async Task<Root> GetNewsAsync(string categoryName)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://gnews.io/api/v4/top-headlines?token=8137a1f8a3ae9fbeb0c1c2e6410f9a75&lang=en&topic={categoryName.ToLower()}");
            var newDate = JsonConvert.DeserializeObject<Root>(response);
            return newDate;
        }
    }
}
