using Backend.Core.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly IApiService _apiService;

        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewEsp(string espId)
        {
            var check = _apiService.CheckIfExisting(espId);

            if (check == false)
            {
                var apiKey = _apiService.CreateApiAsync(espId);
                return Ok(apiKey);
            }
            return BadRequest("Esp er allerede registrert");
        }
    }
}
