using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace SEP490_Worker2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WorkerController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [NonAction]
        public async Task CallApiOkr()
        {
            var response = await _httpClient.GetAsync("http://sep490g49-api.eastasia.cloudapp.azure.com/api/worker/Okrs");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response data: {data}");
            }
            else
            {
                Console.WriteLine($"Failed to call API. Status Code: {(int)response.StatusCode}");
            }
        }

        [HttpGet]
        [Route("okr")]
        public IActionResult ScheduleApiCall()
        {
            RecurringJob.AddOrUpdate("CallApiJob", () => CallApiOkr(), Cron.Monthly);

            return Ok("API call scheduled Okr for every month.");
        }

        [NonAction]
        public async Task CallApiNotification()
        {
            var response = await _httpClient.GetAsync("http://sep490g49-api.eastasia.cloudapp.azure.com/api/worker/Notifications");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response data: {data}");
            }
            else
            {
                Console.WriteLine($"Failed to call API. Status Code: {(int)response.StatusCode}");
            }
        }

        [HttpGet]
        [Route("notification")]
        public IActionResult ScheduleNotification()
        {
            RecurringJob.AddOrUpdate("CallApiJob", () => CallApiNotification(), "0 0 * * *");

            return Ok("API call scheduled Notification for every day.");
        }
    }
}
