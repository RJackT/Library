using System;
using System.Collections.Generic;
using System.IO;
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

                dbBooks[id] = title + ", " + author + ", " + Convert.ToString(availability) + ", " + Convert.ToString(release) + ", " + genre;

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
