using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerUWP.ViewModels;

namespace TaskSchedulerUWP.Services
{
    public class TaskApiService
    {
        public static string UrlBase { get; set; }

        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            IEnumerable<Models.Task> tasks = new List<Models.Task>();
            Uri geturi = new Uri($"{UrlBase}/api/Task/getTasks"); //replace your url  
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(geturi);
            if (responseGet.IsSuccessStatusCode)
            {
                string response = await responseGet.Content.ReadAsStringAsync();
                tasks = JsonConvert.DeserializeObject<IEnumerable<Models.Task>>(response);
            }

            return tasks;
        }
    }
}
