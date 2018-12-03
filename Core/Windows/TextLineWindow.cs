using System;
using Models;
using OpenTK.Graphics;

namespace Core.Windows
{
    public class TextLineWindow : Window
    {
        public Color4 Color { get; }
        public Color4 BgColor { get; }

        public TextLineWindow(int pinY, int pinX, int width, Color4 color, Color4 bgColor, int layer = 0) : base("TextLine", pinY, pinX, width, 1, layer)
        {
            Color = color;
            BgColor = bgColor;
        }

        public void SetTextLine(Symbol[] text)
        {
            SetTextLine(text, 0);
        }

        public override void SetTextLine(Symbol[] text, int row)
        {
            if (row != 0)
            {
                throw new ArgumentException("Text Line Window can only write on a single line");
            }
            if (text.Length >= this.matrix[0].Length)
                throw new ArgumentException("Text's length is longer than window's width");

            for (int i = 0; i < this.Width; i++)
            {
                this.matrix[row][i] = new Symbol(' ', Color4.Green, Color4.Green);
            }

            int col;
            for (col = 0; col < text.Length; col++)
            {
                var symbol = text[col];
                symbol.Color = this.Color;
                symbol.BackgroundColor = BgColor;
                this.matrix[row][col] = symbol;
            }
        }

        public void Update(Symbol[] text)
        {
            SetTextLine(text);
        }
    }
}
