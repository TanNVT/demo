using NewDemoWebApp.Common;
using NewDemoWebApp.DatabaseContext;
using NewDemoWebApp.Entities;
using NewDemoWebApp.Service.User.Create.Request;
using NewDemoWebApp.Service.User.Search.Request;
using NewDemoWebApp.Service.User.Search.Responses;
using NewDemoWebApp.ServiceInterface.User;
using static NewDemoWebApp.Common.ApiResponse;

namespace NewDemoWebApp.Service.User
{
    public class UserService : IUserService
    {
        private readonly DatabaseFromFileService _databaseContext;
        public UserService(DatabaseFromFileService databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Response> Search(SearchUserRequest model)
        {
            var query = _databaseContext.GetUsers().Where(x => (string.IsNullOrWhiteSpace(model.Username) || x.Username.Contains(model.Username))
                                                               && (string.IsNullOrWhiteSpace(model.Email) || x.Email.Contains(model.Email))
                                                               && (string.IsNullOrWhiteSpace(model.Role) || x.Role == model.Role)).Select(x => new SearchUserResponse
                                                               {
                                                                   Id = x.Id,
                                                                   Role = x.Role,
                                                                   Username = x.Username,
                                                                   Email = x.Email,
                                                               });
            var queryAsQueryAble = query.AsQueryable();
            var data = !string.IsNullOrEmpty(model.SortField) ? queryAsQueryAble.SortDesc(model.SortField, model.IsSortDesc) : queryAsQueryAble.OrderBy(x => x.Username);
            var dataWithPage = data.Skip((model.PageIndex - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();
            return Ok(dataWithPage);
        }
        public async Task<Response> Create(CreateUserRequest model)
        {
            var user = new NewDemoWebApp.Entities.User
            {
                Username = model.Username,
                Email = model.Email,
                Role = model.Role,
                Password = model.Password,
                Id = Guid.NewGuid(),
            };
            _databaseContext.AddUser(user);
            return Ok();
        }

        public async Task<Response> Delete(Guid id)
        {
            _databaseContext.RemoveUser(id);
            _databaseContext.RemoveLine<Entities.User>(x => x.Id == id);
            return Ok();
        }
    }
}
