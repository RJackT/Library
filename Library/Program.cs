using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using Library;
using static Library.Program.printScr;

namespace Library
{
    public class Program
    {
        class menu_states
        {
            public int active_item = 0;
            public int item_count = 2;
            public bool render = true;
        }

        public class book 
        {
            public int id;
            public string title;
            public string author;
            public int availability;
            public int release;
            public string genre;
            public static book[] FetchAll()
            {
                string path = System.Reflection.Assembly.GetEntryAssembly().Location;
                path = System.IO.Path.GetFullPath(Path.Combine(path, @"..\..\..\..\//books.txt"));
                string[] dbBooks = System.IO.File.ReadAllLines(@path);
                List<book> books = new List<book>();

                for (int i = 0; i < dbBooks.Length; i++)
                {
                    string line = dbBooks[i].Trim();
                    string[] parts = line.Split(',');

                    books.Add(new book
                    {
                        id = i,
                        title = parts[0].Trim(),
                        author = parts[1].Trim(),
                        availability = int.Parse(parts[2].Trim()),
                        release = int.Parse(parts[3].Trim()),
                        genre = parts[4].Trim()
                    });
                }
                return books.ToArray();
            }  // Fetches all books in an array of books

            public static book Fetch(int id)
            {
                string path = System.Reflection.Assembly.GetEntryAssembly().Location;
                path = System.IO.Path.GetFullPath(Path.Combine(path, @"..\..\..\..\//books.txt"));
                string[] dbBooks = System.IO.File.ReadAllLines(path);

                if(id >= dbBooks.Length)
                {
                    book no_result = new book {
                        title = "ID out of range",
                        author = "",
                        availability = 0,
                        release = 0,
                        genre = ""};
                    return no_result;
                }

                string line = dbBooks[id].Trim();
                string[] parts = line.Split(',');

                return new book
                {
                    id = id,
                    title = parts[0],
                    author = parts[1],
                    availability = int.Parse(parts[2].Trim()),
                    release = int.Parse(parts[3].Trim()),
                    genre = parts[4]
                };
            }  // Fetches book as book object.

            public static bool Edit(int id, string title, string author, int availability, int release, string genre)
            {
                string path = System.Reflection.Assembly.GetEntryAssembly().Location;
                path = System.IO.Path.GetFullPath(Path.Combine(path, @"..\..\..\..\//books.txt"));
                string[] dbBooks = System.IO.File.ReadAllLines(path);

                dbBooks[id] = title + "," + author + "," + Convert.ToString(availability) + "," + Convert.ToString(release) + "," + genre;

                File.WriteAllLines(path, dbBooks);
                return true;
            }

            public static bool Add(string title, string author, int availability, int release, string genre)
            {

                string to_append = "\n" + title + ", " + author + ", " + Convert.ToString(availability) + ", " + Convert.ToString(release) + ", " + genre;

                string path = System.Reflection.Assembly.GetEntryAssembly().Location;
                path = System.IO.Path.GetFullPath(Path.Combine(path, @"..\..\..\..\//books.txt"));
                System.IO.File.AppendAllText(@path, to_append);

                return true;
            }
            public static bool Reserve(int id)
            {

                string path = System.Reflection.Assembly.GetEntryAssembly().Location;
                path = System.IO.Path.GetFullPath(Path.Combine(path, @"..\..\..\..\//reserved.txt"));

                string[] dbReserved = System.IO.File.ReadAllLines(path);
                // System.IO.File.AppendAllText(@path, to_append);
                bool uid_found = false;

                for(int i = 0; i < dbReserved.Length; i++)
                {

                    string line = dbReserved[i].Trim();
                    string[] parts = line.Split(',');
                    if (parts[0] == Login.user.ID.ToString())
                    {
                        for (int j = 1; j < parts.Length; j++)
                            {
                                if (int.Parse(parts[j]) == id)
                                {

                                    return false;
                                }
                            }
                            uid_found = true;
                        }
                }

                if(uid_found == false) 
                {
                    string to_append = Login.user.ID + "," + id;

                    System.IO.File.AppendAllText(@path, to_append);
                    return true;
                }
                for (int i = 0; i < dbReserved.Length; i++)
                {

                    string line = dbReserved[i].Trim();
                    string[] parts = line.Split(',');

                    if (int.Parse(parts[0]) == Login.user.ID)
                    {
                       string to_write = "";
                       for(int j = 0; j < parts.Length; j++)
                       {
                            to_write = parts[j].Trim() + ",";
                       }
                       to_write += Convert.ToString(id);
                       dbReserved[i] = to_write;
                       File.WriteAllLines(path, dbReserved);

                    }
                }
                return true;
            }
            public static bool Loan(int id)
            {

                string path = System.Reflection.Assembly.GetEntryAssembly().Location;
                path = System.IO.Path.GetFullPath(Path.Combine(path, @"..\..\..\..\//borrowed.txt"));

                string[] dbLoan = System.IO.File.ReadAllLines(path);
                // System.IO.File.AppendAllText(@path, to_append);
                bool uid_found = false;

                for (int i = 0; i < dbLoan.Length; i++)
                {

                    string line = dbLoan[i].Trim();
                    string[] parts = line.Split(',');
                    
                        if (parts[0] == Login.user.ID.ToString())
                        {
                            for (int j = 1; j < parts.Length; j++)
                            {
                                if (int.Parse(parts[j]) == id)
                                {

                                    return false;
                                }
                            }
                            uid_found = true;
                        }
                }

                if (uid_found == false)
                {
                    string to_append = "\n" + Login.user.ID + "," + id;

                    System.IO.File.AppendAllText(@path, to_append);
                    return true;
                }
                for (int i = 0; i < dbLoan.Length; i++)
                {

                    string line = dbLoan[i].Trim();
                    string[] parts = line.Split(',');

                    if (parts[0] == Login.user.ID.ToString())
                    {
                        string to_write = "";
                        for (int j = 0; j < parts.Length; j++)
                        {
                            to_write = parts[j].Trim() + ",";
                        }
                        to_write += Convert.ToString(id);
                        dbLoan[i] = to_write;
                        File.WriteAllLines(path, dbLoan);

                    }
                }
                return true;
            }
            public static bool Return(int id)
            {

                string path = System.Reflection.Assembly.GetEntryAssembly().Location;
                path = System.IO.Path.GetFullPath(Path.Combine(path, @"..\..\..\..\//borrowed.txt"));

                string[] dbLoan = System.IO.File.ReadAllLines(path);
                // System.IO.File.AppendAllText(@path, to_append);
                bool uid_found = false;
                bool loaned = false;

                for (int i = 0; i < dbLoan.Length; i++)
                {

                    string line = dbLoan[i].Trim();
                    string[] parts = line.Split(',');
                    if (parts[0] != null && parts[0] != "")
                    if (int.Parse(parts[0]) == Login.user.ID)
                    {

                        string to_write = "";
                        for (int j = 1; j < parts.Length; j++)
                        {
                            if (Convert.ToInt32(parts[j].Trim()) != id)
                                to_write = parts[j].Trim() + ",";
                            else 
                                loaned = true;

                        }
                        dbLoan[i] = to_write;
                        File.WriteAllLines(path, dbLoan);

                        uid_found = true;
                    }
                }

                if (uid_found == false)
                {

                    return false;
                }
              
                return loaned;
            }

        }

        public static book pub_book = new book();

        static menu_states menu = new menu_states();
        public enum printScr : int
        {
            index, sign_in, sign_up, browse, search, admin_panel, list_users, add_book, edit_book, book_page
        } // Id for page to draw

        public static int cur_page = (int)index;


        static void Main(string[] args)
        {
            var print = new Print();
            for(; ; )
            {
                Keybinds.keyPresses();
                print.Printer(cur_page); // Print " start page, "index" "
            }
        }
    }
}
