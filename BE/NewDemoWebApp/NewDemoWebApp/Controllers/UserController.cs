using Microsoft.AspNetCore.Mvc;
using NewDemoWebApp.Common;
using NewDemoWebApp.Service.User;
using NewDemoWebApp.Service.User.Create.Request;
using NewDemoWebApp.Service.User.Search.Request;
using NewDemoWebApp.ServiceInterface.User;
using System.Reflection;

namespace NewDemoWebApp.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route(RouteConst.Employee.Search)]
        public async Task<object> Search([FromQuery]SearchUserRequest model)
        {
            var rs = await _userService.Search(model).ConfigureAwait(false);
            return Ok(rs);
        }
        [HttpPost]
        [Route(RouteConst.Employee.Create)]
        public async Task<object> Create([FromBody]CreateUserRequest model)
        {
            await _userService.Create(model).ConfigureAwait(false);
            return Ok();
        }
        [HttpPut]
        [Route(RouteConst.Employee.Update)]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }
        [HttpDelete]
        [Route(RouteConst.Employee.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            await _userService.Delete(id).ConfigureAwait(false);
            return Ok();
        }



    }
}
