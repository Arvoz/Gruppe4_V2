using Backend.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ApiKeyController : Controller
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        [HttpPost("{remoteId}/register")]
        public async Task<IActionResult> RegisterNewRemote(string remoteId)
        {
            var check = await _apiKeyService.CheckIfExisting(remoteId);

            if (check == true)
            {
                return BadRequest("Allerede registrert");
            }
            var apiKey = await _apiKeyService.CreateApiKey(remoteId);
            return Ok(apiKey);
        }

    }
}
