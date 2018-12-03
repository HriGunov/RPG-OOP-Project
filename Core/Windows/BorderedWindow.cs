using System.Linq;
using System;

using Models;
using OpenTK.Graphics;

namespace Core.Windows
{

    public class BorderedWindow : Window
    {

        public BorderedWindow(string title, int pinX, int pinY, int width, int height, bool includeTitle = false, int layer = 0)
            : this(title, pinX, pinY, width, height, new Symbol(' ', Color4.White, Color4.Green), includeTitle, layer)
        {

        }

        public BorderedWindow(string title, int pinX, int pinY, int width, int height, Symbol borderSymnol, bool includeTitle, int layer = 0) :
            base(title, pinX, pinY, width, height, layer)
        {
            CreateFrame(borderSymnol);

            if (includeTitle)
            {
                if (Width - 2 <= title.Length)
                {
                    throw new ArgumentOutOfRangeException("Title is too dam big. Cannot be bigger than window's width");
                }
                this.Title = title;
                CreateTitle();
            }
        }

        public override string Title
        {
            get => this.title;
            set
            {
                this.title = value;
            }
        }

        public override void SetTextLine(Symbol[] text, int row)
        {
            if (text.Length > this.matrix[0].Length)
                throw new ArgumentException("Text's length is longer than window's width");

            for (int col = 1; col < text.Length - 1; col++)
            {
                this.matrix[row][col] = text[col];
            }
        }

        public void CreateFrame()
        {
            CreateFrame(new Symbol(' ', Color4.White, Color4.Green));
        }

        private void CreateFrame(Symbol borderSymbol)
        {
            //we use a single object for all of the frame
            var symbol = new Symbol(borderSymbol);

            //this.TopLeftCorner = new Corner(pinX, pinY);
            //this.TopRightCorner = new Corner(pinX + width, pinY);
            //this.BottomLeftCorner = new Corner(pinX, pinY + height);
            //this.BottomRightCorner = new Corner(pinX + width, pinY + height);


            /*
            // calculate corners
            matrix[0][0] = new Symbol('╔');
            matrix[0][Width-1] = new Symbol('╗');
            matrix[Height - 1][0] = new Symbol('╚');
            matrix[Height - 1][Width - 1] = new Symbol('╝');
            */
            // calculate top and bottom part of the frame
            for (int col = 0; col < Width; col++)
            {
                matrix[0][col] = symbol;
                matrix[Height - 1][col] = symbol;
            }

            // calculate left and right part of the frame
            for (int row = 0; row < Height; row++)
            {
                matrix[row][0] = symbol;
                matrix[row][Width - 1] = symbol;
            }
        }

        private void CreateTitle()
        {
            Color4 foo = Matrix[0][0].Color;
            int titleStartIndex = (this.Width - this.Title.Length) / 2;
            int titleEndIndex = titleStartIndex + this.Title.Length;
        }

        public override void Update(Symbol[][] newMatrix)
        {
            base.Update(newMatrix);
            CreateFrame();
        }

        public override void Clear()
        {
            for (int row = 1; row <= this.Height - 2; row++)
            {
                this.ClearRow(row);
            }
        }

        public override void ClearRow(int row)
        {
            Console.SetCursorPosition(pinX + 1, pinY + row);

            this.matrix[row] =
                this.Matrix[row].Select((symb, index) =>
                    {
                        if (index == 0 || index == matrix[row].Count() - 1)
                            return symb;

                        return new Symbol(' ');
                    })
                    .ToArray();
        }
    }
}
