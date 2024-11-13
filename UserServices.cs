using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MohaymenProject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohaymenProject
{
    public class UserServices
    {
        public static AppDbContext context;
        public static User service_user; 

        public UserServices(AppDbContext s_context)
        {
            context = s_context;
        }
     

        public bool UserRegister(string instruction, string key1, string key2, string value1, string value2)
        {
            var existingUser = context.Users.FirstOrDefault(u => u.Username == value1);
            if (existingUser != null)
            {
                Console.WriteLine("Register failed! Username already exists.");
                return false;
            }
            else
            {
                var newUser = new User
                {
                    Username = value1,
                    Password = value2,
                    Status = true
                };
                context.Users.Add(newUser);
                service_user = newUser;
                context.SaveChanges();
                Console.WriteLine("User added to the database.");
            }
            Console.WriteLine($"Registering user with {key1}: {value1}, {key2}: {value2}");
            return true;

        }

        public bool UserLogin(string instruction, string key1, string key2, string value1, string value2)
        {

            var existingUser = context.Users.FirstOrDefault(u => u.Username == value1);
            if (existingUser == null)
            {
                Console.WriteLine("Login failed! Username does not exist.");
                return false;
            }
            else
            {
                service_user = existingUser;
                if (existingUser.Password == value2)
                {
                    Console.WriteLine("Login successful!");
                    
                }
                else
                {
                    Console.WriteLine("Login failed! Incorrect password.");
                    return false;
                }
            }

            Console.WriteLine($"Logging in user with {key1}: {value1}, {key2}: {value2}");
            return true;
        }

        internal void ChangeStatus(string value1)
        {
            bool isavalble = true;
            if(value1 == "[available]") { isavalble = true; }
            else if (value1 == "[not") { isavalble = false; }
            var existingUser = context.Users.FirstOrDefault(u => u.Username == service_user.Username);
            existingUser.Status = isavalble;
            context.SaveChanges();
            Console.WriteLine("Status Changed");
        }

        internal void Search(string value1)
        {
            string lowercaseValue = value1.ToLower();

            var matchingUsers = context.Users
                .Where(u => u.Username.ToLower().StartsWith(lowercaseValue))
                .ToList();

            if (matchingUsers.Count == 0)
            {
                Console.WriteLine($"No users found starting with '{value1}'.");
                return;
            }

            Console.WriteLine($"Users starting with '{value1}':");

            int index = 1;
            foreach (var user in matchingUsers)
            {
                string status = user.Status ? "available" : "not available";
                Console.WriteLine($"{index}- {user.Username} | status: {status}");
                index++;
            }
        }


        internal void ChangePass( string value1, string value2)
        {
            var existingUser = context.Users.FirstOrDefault(u => u.Username == service_user.Username);

            if (existingUser == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            if (existingUser.Password != value1)
            {
                Console.WriteLine("Password change failed! Incorrect old password.");
                return;
            }

            existingUser.Password = value2;
            context.SaveChanges();
            Console.WriteLine("Password changed successfully.");
        }

    }

}
