using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWealth.Business.Operations.Setting;

namespace MyWealth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;
        // dependency injection for portfolio processes
        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        // put into maintenance mode
        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToogleMaintenence()
        {
            // It is sent to the setting service for the transactions to be carried out.
            await _settingService.ToggleMaintenence();

            return Ok();
        }







    }
}
