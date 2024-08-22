using NewDemoWebApp.Common.DTO;

namespace NewDemoWebApp.Service.User.Search.Request
{
    public class SearchUserRequest : PagingDTO
    {


        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}
