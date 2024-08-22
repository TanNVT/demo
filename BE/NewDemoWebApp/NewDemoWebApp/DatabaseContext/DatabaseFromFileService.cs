using System;

namespace NewDemoWebApp.DatabaseContext
{
    public class DatabaseFromFileService
    {
        public List<User> Users { get; } = new List<User>();
        public List<Employee> Employee { get; } = new List<Employee>();

        public DatabaseFromFileService()
        {

        }

        static List<User> LoadDataUserFromFile(string filePath)
        {
            var data = new List<User>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines[1..]) // Skip the header
            {
                var guid = Guid.NewGuid();
                var values = line.Split(", ");
                data.Add(new User
                {
                    Id = Guid.TryParse(values[0], out guid) ? guid: Guid.Empty,
                    Username = values[1],
                    Password = values[2],
                    Email = values[3],
                    Role = values[4]
                });
            }
            return data;
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void AddEmployee(User user)
        {
            Users.Add(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return Users;
        }
        public IEnumerable<Employee> GetEmployee()
        {
            return Employee;
        }
    }
}
