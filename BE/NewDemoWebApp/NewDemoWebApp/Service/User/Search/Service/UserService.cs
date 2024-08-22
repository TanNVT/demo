using NewDemoWebApp.Common;
using NewDemoWebApp.Service.User.Search.Request;
using NewDemoWebApp.ServiceInterface.User;
using static NewDemoWebApp.Common.ApiResponse;

namespace NewDemoWebApp.Service.User.Search.Service
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public async Task<Response> Search(SearchUserRequest model)
        {
            
            return Ok();
        }
    }
}
