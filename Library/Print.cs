using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Text;
using Library;

namespace Library
{
    public class Print
    {
        public void Printer (int page)
        {
            if (Keybinds.menu.render)
            {
                Keybinds.menu.render = false;
                Console.Clear();
                switch (page)
                {
                    case 0:
                        if (!Login.logged_in)
                        {
                            Keybinds.menu.item_count = 1;
                            Console.WriteLine("Would you like to:   (Press rightarrow to enter)");
                            if (Keybinds.menu.active_item == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                if (Keybinds.menu.enter) { Keybinds.menu.enter = false; Program.cur_page = (int)Program.printScr.sign_in; Keybinds.menu.render = true; }
                            }
                            Console.WriteLine("1. Sign in?");
                            Console.ForegroundColor = ConsoleColor.White;
                            if (Keybinds.menu.active_item == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                if (Keybinds.menu.enter) { Keybinds.menu.enter = false; Program.cur_page = (int)Program.printScr.sign_up; Keybinds.menu.render = true; }
                            }
                            Console.WriteLine("2. Sign up?");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Keybinds.menu.item_count = 1;
                            Console.WriteLine($"Welcome " + Login.user.name + "! Would you like to:");

                            if (Keybinds.menu.active_item == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                if (Keybinds.menu.enter) { Keybinds.menu.enter = false; Program.cur_page = (int)Program.printScr.browse; Keybinds.menu.render = true; }
                            }
                            Console.WriteLine("1. Browse");
                            Console.ForegroundColor = ConsoleColor.White;

                            if (Keybinds.menu.active_item == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                if (Keybinds.menu.enter) { Keybinds.menu.enter = false; Program.cur_page = (int)Program.printScr.search; Keybinds.menu.render = true; }
                            }
                            Console.WriteLine("2. Search");
                            Console.ForegroundColor = ConsoleColor.White;

                            if(Login.user.admin)
                            {
                                Keybinds.menu.item_count = 2;
                                if (Keybinds.menu.active_item == 2)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    if (Keybinds.menu.enter) { Keybinds.menu.enter = false; Program.cur_page = (int)Program.printScr.admin_panel; Keybinds.menu.render = true; }
                                }
                                Console.WriteLine("3. Admin panel");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        break;
                    case 1: // Om current page är login, render register and run code.
                        Login.LogIn();
                        Program.cur_page = (int)Program.printScr.index;
                        Keybinds.menu.render = true;
                        break;
                    case 2:  // Om current page är register, render register and run code.
                        Login.register();
                        Program.cur_page = (int)Program.printScr.index;
                        Keybinds.menu.render = true;
                        break;
                    case 3: // BROWSE
                        if (Keybinds.menu.active_item == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (Keybinds.menu.enter) { Keybinds.menu.enter = false; Program.cur_page = (int)Program.printScr.index; Keybinds.menu.render = true; }
                        }
                        Console.WriteLine("<-- Go back");
                        Console.ForegroundColor = ConsoleColor.White;

                        Program.book[] books = Program.book.FetchAll();
                        
                        Keybinds.menu.item_count = books.Length;

                        for(int i = 0; i < books.Length; i++)
                        {
                            Console.Write("-----------------------------------\n");
                            if(i+1 == Keybinds.menu.active_item) Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Title: " + books[i].title +
                                "\nAuthor: " + books[i].author + "\nAvailability: " + books[i].availability);
                            Console.ForegroundColor = ConsoleColor.White;
                        }


                        break;




                       /*
                        Write code for printing browse
                        
                        */

                        break;

                }
            }
        }
    }
}
