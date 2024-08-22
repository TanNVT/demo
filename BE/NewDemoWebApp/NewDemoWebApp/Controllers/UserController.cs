using Microsoft.AspNetCore.Mvc;

namespace NewDemoWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        public UserController()
        {
        }
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }



    }
}
