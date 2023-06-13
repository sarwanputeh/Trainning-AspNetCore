using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrixNetCoreApp.Services.ThaiDate;

namespace OrixNetCoreApp.Controllers
{
    [Route("api/[controller]")] //localhost:9999/api/home
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IThaiDate _thaiDate;
        public HomeController(IThaiDate thaiDate) { 
            _thaiDate = thaiDate;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = $"Orix API Running...{_thaiDate.ShowThaiDate()}"});
        }
    }
}
