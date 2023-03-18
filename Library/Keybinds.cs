using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    class Keybinds
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]

        // Säger att vi har den här funktionen.
        static extern short GetAsyncKeyState(int vKey);

        // Definerar tangenter för att göra det lättare att läsa, icke nödvändigt.
        static int VK_RIGHT = 0x27; // Right Key
        static int VK_DOWN = 0x28; // Left Key
        static int VK_UP = 0x26;   // Up Key

        public class menu_states
        {
            public int active_item = 0;
            public int item_count = 2;
            public bool render = true;
            public bool enter = false;
        }

        public static menu_states menu = new menu_states();
        public static void keyPresses()
        {
            byte[] result_down = BitConverter.GetBytes(GetAsyncKeyState(VK_DOWN)); // Nödvändigt enligt då när man trycker en tangent representeras det som 0000, beroende på vad du trycker kan det vara, 0010, 1011, 1001.
            byte[] result_up = BitConverter.GetBytes(GetAsyncKeyState(VK_UP));
            byte[] result_right = BitConverter.GetBytes(GetAsyncKeyState(VK_RIGHT)); // Enter

            if (result_up[0] == 1) //Kollar om knappen tryckts ner sedan den kollade sist
            {
                if(menu.active_item > 0)
                {
                    menu.active_item--;
                }
                else
                {
                    menu.active_item = 0;
                }
                menu.render = true;
            }

            if (result_right[0] == 1)
            {
                menu.enter = true;
                menu.render = true;
            }

            if (result_down[0] == 1)
            {
                if (menu.active_item < menu.item_count)
                {
                    menu.active_item++;
                }
                else
                {
                    menu.active_item = menu.item_count;
                }
                menu.render = true;
            }
        }
    }
}
