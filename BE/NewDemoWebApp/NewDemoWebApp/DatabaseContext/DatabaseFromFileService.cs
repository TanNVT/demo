using NewDemoWebApp.Entities;
using System;
using System.Reflection;

namespace NewDemoWebApp.DatabaseContext
{
    public class DatabaseFromFileService
    {
        public List<User> Users { get; } = new List<User>();
        public List<Employee> Employee { get; } = new List<Employee>();
        public string _userDataFileName = "dataUser.txt";
        public string _employeeDataFileName = "dataEmployee.txt";

        public DatabaseFromFileService()
        {
            Users = LoadDataUserFromFile(_userDataFileName);
           // Employee = LoadDataUserFromFile(_employeeDataFileName);
        }

        

        static List<User> LoadDataUserFromFile(string filePath)
        {
            var data = new List<User>();
            if (!File.Exists(filePath))
            {
               var file = File.Create(filePath);
                file.Close();
            }
            var lines = File.ReadAllLines(filePath);
            if(lines.Length == 0)
            {
                return new List<User>();
            }
            foreach (var line in lines[1..]) // Skip the header

            {
                var guid = Guid.NewGuid();
                var values = line.Split(", ");
                data.Add(new User
                {
                    Id = Guid.TryParse(values[0], out guid) ? guid : Guid.Empty,
                    Username = values[1],
                    Password = values[2],
                    Email = values[3],
                    Role = values[4]
                });
            }
            return data;
        }

        public void AppendEntityToFile<T>(T entity, string filePath) where T : class
        {
            bool addHeader = !File.Exists(filePath) || new FileInfo(filePath).Length == 0;

            using (var writer = new StreamWriter(filePath, true))
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Add the header if the file is new or empty
                if (addHeader)
                {
                    var header = string.Join(", ", properties.Select(p => p.Name));
                    writer.WriteLine(header);
                }

                // Add the entity data
                var values = string.Join(", ", properties.Select(p => p.GetValue(entity)?.ToString()));
                writer.WriteLine(values);
            }
        }


        public void AddUser(User user)
        {
            Users.Add(user);
            if (user != null && Users.Count > 0)
            {
                AppendEntityToFile<User>(user, _userDataFileName);
            }

        }

        public void AddEmployee(Employee employee)
        {
            Employee.Add(employee);
            if (employee != null && Users.Count > 0)
            {
                AppendEntityToFile<Employee>(employee, _employeeDataFileName);
            }
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
