using BusinessObjects.Response;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Notifications;
using Repository.Objectives;

namespace Sep490_G49_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WorkersController(IHttpClientFactory httpClientFactory)
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
            RecurringJob.AddOrUpdate("ScheduleOkrJob", () => CallApiOkr(), Cron.Monthly);

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
            RecurringJob.AddOrUpdate("ScheduleNotificationJob", () => CallApiNotification(), "0 0 * * *");

            return Ok("API call scheduled Notification for every day.");
        }
    }
}
