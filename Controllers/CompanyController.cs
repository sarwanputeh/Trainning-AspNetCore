using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrixNetCoreApp.Controllers
{
    [Route("api/[controller]")] //localhost:9999/api/company
    [ApiController]
    public class CompanyController : ControllerBase
    {

        [Route("")] // api/company/
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { message = "Company Index" });
        }

        [Route("about")] // api/company/about
        [HttpGet]
        public IActionResult About()
        {
            return Ok(new { message = "About us" });
        }

        [Route("product/{id}/name/{productname}")] // api/company/product/20/name/coke
        [HttpGet]
        public IActionResult GetProductById(int id, string productname)
        {
            return Ok(new { message = $"Product Id: {id} {productname}" });
        }

        //api/company/search?name=JJ&age=10
        [Route("search")]
        [HttpGet]
        public IActionResult Search([FromQuery] string name, [FromQuery] int age)
        {
            return Ok(new { message = $"{name} {age}" });
        }

    }
}
