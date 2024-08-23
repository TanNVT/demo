using NewDemoWebApp.Service.User.Create.Request;
using NewDemoWebApp.Service.User.Search.Request;
using NewDemoWebApp.Service.User.Update.Request;
using static NewDemoWebApp.Common.ApiResponse;

namespace NewDemoWebApp.ServiceInterface.User
{
    public interface IUserService
    {
        Task<Response> Search(SearchUserRequest model);
        Task<Response> Create(CreateUserRequest model);
        Task<Response> Delete(Guid id);

        Task<Response> Update(UpdateUserRequest model);
    }
}
