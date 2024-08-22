using NewDemoWebApp.Service.User.Create.Request;
using NewDemoWebApp.Service.User.Search.Request;
using static NewDemoWebApp.Common.ApiResponse;

namespace NewDemoWebApp.ServiceInterface.User
{
    public interface IUserService
    {
        Task<Response> Search(SearchUserRequest model);
        Task<Response> Create(CreateUserRequest model);
    }
}
