using System;

namespace RPG.Visulization
{
    public class ConsoleSize
    {
        public static void Main()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorVisible = false;
            FieldInitialization();
            DrawingBorder();
            TextDrawing(47, 0, "Health: 100");
            TextDrawing(47, 2, "Armour: 50");
            TextDrawing(22, 9, "&");

            while (true)
            {
            }
        }

        public static void FieldInitialization()
        {
            Console.WindowHeight = 20;
            Console.WindowWidth = 65;

            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        public static void DrawingBorder()
        {
            for (int i = 19; i >= 0; i--)
            {
                PrintAtPosition(45, i, '*');
            }
        }

        static void PrintAtPosition(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        public static void TextDrawing(int x, int y, string message)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(message);
        }
    }
}
