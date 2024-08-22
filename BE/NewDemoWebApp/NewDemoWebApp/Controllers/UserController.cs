using Microsoft.AspNetCore.Mvc;
using NewDemoWebApp.Service.User;
using NewDemoWebApp.Service.User.Create.Request;
using NewDemoWebApp.Service.User.Search.Request;
using NewDemoWebApp.ServiceInterface.User;

namespace NewDemoWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<object> Search([FromQuery]SearchUserRequest model)
        {
            var rs = await _userService.Search(model).ConfigureAwait(false);
            return Ok(rs);
        }
        [HttpPost]
        public async Task<object> Create([FromBody]CreateUserRequest model)
        {
            await _userService.Create(model).ConfigureAwait(false);
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
