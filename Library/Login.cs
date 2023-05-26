using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using static Library.Program;

namespace Library
{
    class Login
    {
        public static bool logged_in = false;

        public class user_schema
        {
            public string name = "";
            public string ssid = "";
            public string password = "";
            public int ID = -1;
            public bool admin = false;
            static public user_schema[] getAllUsers()
            {
                string[] dbUsers = System.IO.File.ReadAllLines(@"C:\\Users\\Jack\\Downloads\\Library-main\\Library\\users.txt");

                List<user_schema> users = new List<user_schema>();

                for (int i = 0; i < dbUsers.Length; i++)
                {
                    string line = dbUsers[i].Trim();
                    string[] parts = line.Split(',');
                    users.Add(new user_schema
                    {
                        name = parts[0].Trim(),
                        ssid = parts[1].Trim(),
                        password = parts[2].Trim(),
                        ID = i,
                        admin = Convert.ToBoolean(int.Parse(parts[3].Trim()))
                    });
                }
                return users.ToArray();
            }
            static public user_schema getUser(int id) {
                string[] dbUsers = System.IO.File.ReadAllLines(@"C:\\Users\\Jack\\Downloads\\Library-main\\Library\\users.txt");

                if (id >= dbUsers.Length)
                {
                    user_schema no_result = new user_schema
                    {
                        name = "ID out of range",
                        ssid = "",
                        password = "",
                        ID = -1,
                        admin = false
                    };
                    return no_result;
                }

                string line = dbUsers[id].Trim();
                string[] parts = line.Split(',');

                return new user_schema
                {
                    name = parts[0].Trim(),
                    ssid = parts[1].Trim(),
                    password = parts[2].Trim(),
                    ID = id,
                    admin = Convert.ToBoolean(Convert.ToInt16(parts[3].Trim()))
                }; ;
            }
        }

        public static user_schema user = new user_schema();

        
        public static bool verify(string name, string password)
        {
            user_schema[] users = user_schema.getAllUsers();

            for (int i = 0; i < users.Length; i++)
            {
                if ((users[i].name == name || users[i].ssid == name) && users[i].password == password && name != "" && password != "" && users[i].password != "")
                {
                    logged_in = true;
                    user = users[i];

                    return true;
                }
            }
            return false;
        }

        public static void LogIn()
        {
            Console.WriteLine("Enter your username or SSID:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            if (verify(name, password))
                Console.Write("Successfully logged in.");
            else
                Console.Write("Failure, wrong password or username");

            return;
        }
        public static void register()
        {
            bool succeeded;
            string name;
            string ssid;
            string password;

            string[] dbUsers = System.IO.File.ReadAllLines(@"C:\\Users\\Jack\\Downloads\\Library-main\\Library\\users.txt");
            user_schema[] users = user_schema.getAllUsers();
            do
            {
                succeeded = true;

                Console.WriteLine("Register an account\nEnter your username:");
                 name = Console.ReadLine();
                Console.WriteLine("Enter your SSID:");
                 ssid = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                 password = Console.ReadLine();

                for (int i = 0; i < users.Length; i++) // check if the name or SSID exists
                {
                    if (users[i].name == name || users[i].ssid == ssid || name == "" || ssid == "" || password == "")
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The SSID or username is already registered or you have note entered one of the fields. Please try again");
                        Console.ForegroundColor = ConsoleColor.White;
                        succeeded = false;
                    }
                }
            } while (!succeeded);


            string to_append = name + ", " + ssid + ", " + password + ", 0";
            System.IO.File.AppendAllText(@"C:\\Users\\Jack\\Downloads\\Library-main\\Library\\users.txt", to_append + "\n");

            logged_in = true;
            user.ID = users.Length;
            user.name = name;
            user.ssid = ssid;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully registered!");
            Console.ForegroundColor = ConsoleColor.White;

            return;
        }
    }
}

