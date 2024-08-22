using demoWebApp.Entities.IDateTracking;
using System;

namespace NewDemoWebApp.Entities
{

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class User : IDateTracking
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}