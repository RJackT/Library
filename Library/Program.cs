using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
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
            public string title;
            public string author;
            public string availability;
            public string release;

            public static book[] FetchAll()
            {
                string[] dbBooks = System.IO.File.ReadAllLines(@"C:\Users\jack.rosandertangen\source\repos\Library\Library\books.txt");
                List<book> books = new List<book>();

                for (int i = 0; i < dbBooks.Length; i++)
                {
                    string line = dbBooks[i].Trim();
                    string[] parts = line.Split(',');

                    books.Add(new book
                    {
                        title = parts[0].Trim(),
                        author = parts[1].Trim(),
                        availability = parts[2].Trim(),
                        release = parts[3].Trim()
                    });
                }
                return books.ToArray();
            }  // Fetches all books in an array of books

            public static book Fetch(int id)
            {
                string[] dbBooks = System.IO.File.ReadAllLines(@"C:\Users\jack.rosandertangen\source\repos\Library\Library\books.txt");

                if(id >= dbBooks.Length)
                {
                    book no_result = new book {title = "ID out of range",
                        author = "",
                        availability = "",
                        release = "" };
                    return no_result;
                }

                string line = dbBooks[id].Trim();
                string[] parts = line.Split(',');

                return new book
                {
                    title = parts[0],
                    author = parts[1],
                    availability = parts[2],
                    release = parts[3]
                }; ;
            }  // Fetches book as book object.
        }

        static menu_states menu = new menu_states();
        public enum printScr : int
        {
            index, sign_in, sign_up, browse, search, admin_panel
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
