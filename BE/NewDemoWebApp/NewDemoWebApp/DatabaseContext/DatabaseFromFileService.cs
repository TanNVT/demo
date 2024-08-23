using NewDemoWebApp.Entities;
using System;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace NewDemoWebApp.DatabaseContext
{
    public class DatabaseFromFileService
    {
        public List<User> Users { get; private set; } = new List<User>();
        public List<Employee> Employee { get; private set; } = new List<Employee>();
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
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
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

        public void RemoveLine<T>(Func<T, bool> condition) where T : class, new()
        {
            if (!File.Exists(_userDataFileName))
            {
                return;
            }

            var lines = File.ReadAllLines(_userDataFileName).ToList();
            var header = lines[0]; // Assumes the first line is the header

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Filter out the lines that match the condition
            var filteredLines = lines.Skip(1).Where(line =>
            {
                var values = line.Split(", ");
                var entity = new T();

                for (int i = 0; i < properties.Length; i++)
                {
                    var property = properties[i].PropertyType;
                    if(properties[i].PropertyType.Name == "Guid")
                    {
                        properties[i].SetValue(entity, Guid.TryParse(values[i], out Guid result) ? result : Guid.Empty);
                        continue;
                    }
                    if (properties[i].PropertyType.IsGenericType && properties[i].PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && properties[i].PropertyType.GetGenericArguments()[0].Name == "DateTime")
                    {
                        properties[i].SetValue(entity, DateTime.TryParse(values[i], out DateTime result) ? result : null);
                        continue;
                    }
                    properties[i].SetValue(entity, Convert.ChangeType(values[i], properties[i].PropertyType));
                }

                return !condition(entity);
            }).ToList();

            // Write the filtered data back to the file
            using (var writer = new StreamWriter(_userDataFileName, false))
            {
                writer.WriteLine(header);
                foreach (var line in filteredLines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        public void UpdateList<T>(List<T> UpdatedDataList) where T : class, new()
        {
            if (!File.Exists(_userDataFileName))
            {
                return;
            }

            var lines = File.ReadAllLines(_userDataFileName).ToList();
            var header = lines[0];
            var updatedLines = UpdatedDataList;
            using (var writer = new StreamWriter(_userDataFileName, false))
            {
                writer.WriteLine(header);
                foreach (var entity in updatedLines)
                {
                    var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    var values = string.Join(", ", properties.Select(p => p.GetValue(entity)?.ToString()));
                    writer.WriteLine(values);
                }
            }

        }



        public void RemoveUser(Guid id)
        {
            Users = Users.Where(x => x.Id != id).ToList();
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
            if (Employee != null && Employee.Count > 0)
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
