using Microsoft.EntityFrameworkCore;
using MohaymenProject;
using MohaymenProject.DataBase;
using System.Text.RegularExpressions;

class Program
{
    public static string instruction = string.Empty;
    public static string key1 = string.Empty;
    public static string key2 = string.Empty;
    public static string value1 = string.Empty;
    public static string value2 = string.Empty;


    public static AppDbContext context = new AppDbContext();
    public static UserServices service = new UserServices(context);
    static void Main(string[] args)
    {
        Greeting();   
    }

    private static void Greeting()
    {
        Console.WriteLine("Hello , welcome to the user manager app , here is the login/Signup section  please login");
        ProcessInputData();

        if (instruction == "Register")
        {
            var result = service.UserRegister(instruction, key1, key2, value1, value2);
            if (result == true)
            {
                ShowMenu();
            }
            else return;

        }
        else if (instruction == "Login")
        {
            var result = service.UserLogin(instruction, key1, key2, value1, value2);
            if (result == true)
            {
                ShowMenu();
            }
            else return;

        }
        else
        {
            Console.WriteLine("You don't have permission to do this!!");
            return;
        }
    }



  



    private static void ShowMenu()
    {
        Console.WriteLine("WELCOME TO Main MENU");
        while (true)
        {
            ProcessInputData() ;
            if (instruction == "Change")
            {
                service.ChangeStatus(value1);
            }
            else if ( instruction == "Search")
            {
                service.Search(value1);
            }
            else if (instruction == "ChangePassword")
            {
                service.ChangePass( value1 , value2);
            }
            else if (instruction == "Logout")
            {

            }
        }
    }

    public static void ProcessInputData()
    {
        Console.WriteLine("Write Your Command");
        string input = Console.ReadLine();

        string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
        {
            Console.WriteLine("Invalid command format.");
            return;
        }
        key1 = null; key2 = null; value1 = null;value2 = null;
        instruction = parts[0];

        if (instruction == "Change" || instruction == "Search")
        {
            value1 = parts[2];
            return;
        }
        if (instruction =="Logout")
        {
            Console.WriteLine("Succesfully loged out");
            Greeting();
            return;
        }



        int keyCounter = 1;

        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].StartsWith("--"))
            {
                string key = parts[i].Substring(2); 

                if (i + 1 < parts.Length)
                {
                    string value = parts[i + 1];

                    if (keyCounter == 1)
                    {
                        key1 = key;
                        value1 = value;
                    }
                    else if (keyCounter == 2)
                    {
                        key2 = key;
                        value2 = value;
                    }
                    else
                    {
                        Console.WriteLine($"Too many key-value pairs. Only two key-value pairs are supported.");
                        return;
                    }

                    keyCounter++; 
                    i++; 
                }
                else
                {
                    Console.WriteLine($"Missing value for the flag: {parts[i]}");
                }
            }
            else
            {
                Console.WriteLine($"Invalid format after instruction: {parts[i]}");
            }
        }

        
    }


}
