namespace NewDemoWebApp.Service.User.Update.Request
{
    public class UpdateUserRequest
    {
        public Guid? Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
