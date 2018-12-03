using System;
using System.Linq;

using Models;

namespace Core.Windows
{
    public class Window
    {
        protected Symbol[][] matrix;
        private int layer;

        // the Y and X of the top left corner of the window
        protected int pinY;
        protected int pinX;
        protected int width;
        protected int height;
        protected string title;

        /// <param name="title">Max allowed length of title is Globals.MAX_WINDOW_TITLE_LEN</param>
        /// <param name="pinY">Y of the top left corner of the window</param>
        /// <param name="pinX">X of the top left corner of the window</param>
        /// <param name="width">Window's width cannot be smaller than Globals.MAX_WINDOW_TITLE_LEN + 2</param>
        /// <param name="height">Window's height cannot be smaller than 2</param>
        public Window(string title, int pinY, int pinX, int width, int height, int layer = 0)
        {
            this.Title = title;
            this.Layer = layer;
            this.PinY = pinY;
            this.PinX = pinX;
            this.Width = width;
            this.Height = height;

            this.TopLeftCorner = new Corner(pinX, pinY);
            this.TopRightCorner = new Corner(pinX + width, pinY);
            this.BottomLeftCorner = new Corner(pinX, pinY + height);
            this.BottomRightCorner = new Corner(pinX + width, pinY + height);

            matrix = new Symbol[Height][];
            for (int i = 0; i < Height; i++)
            {
                matrix[i] = new Symbol[Width];
                for (int j = 0; j < Width; j++)
                {
                    matrix[i][j] = new Symbol();
                }
            }
        }

        public Symbol[][] Matrix
        {
            get
            {
                return this.matrix;
            }
            set => this.matrix = value;
        }

        public int Layer
        {
            get
            {
                return this.layer;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Layer cannot be less than 0");

                this.layer = value;
            }
        }

        public int PinY
        {
            get
            {
                return this.pinY;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException($"{nameof(value)} cannot be negative");
                if (value > Engine.ConsoleHeight - 1)
                    throw new ArgumentException($"{nameof(value)} cannot be bigger than Console window's heigth ({Engine.ConsoleHeight})");

                this.pinY = value;
            }
        }

        public int PinX
        {
            get
            {
                return this.pinX;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException($"{nameof(value)} cannot be negative");
                if (value > Engine.ConsoleWidth - 1)
                    throw new ArgumentException($"{nameof(value)} cannot be bigger than Console window's width ({Engine.ConsoleWidth})");

                this.pinX = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                if (value < 2)
                    throw new ArgumentException("Window's width cannot be smaller than 2");
                if (this.PinX + value > Engine.ConsoleWidth)
                    throw new ArgumentException("Window's width is too big and the window goes out of the console window");

                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Window's height cannot be smaller than 1");
                if (this.PinY + value > Engine.ConsoleHeight)
                    throw new ArgumentException("Window's height is too big and the window goes out of the console window");

                this.height = value;
            }
        }

        protected int CurrRow { get; set; }

        public Corner TopLeftCorner { get; }
        public Corner TopRightCorner { get; }
        public Corner BottomLeftCorner { get; }
        public Corner BottomRightCorner { get; }

        public virtual string Title { get => title; set => title = value; }

        public virtual void SetSymbol(Symbol sym, int y, int x)
        {
            matrix[y][x] = sym;
        }

        public virtual void SetTextLine(Symbol[] text, int row)
        {
            if (text.Length >= this.matrix[0].Length)
                throw new ArgumentException("Text's length is longer than window's width");

            for (int col = 0; col < text.Length; col++)
            {
                this.matrix[row][col] = text[col];
            }
        }

        /// <summary>
        /// This Window fills out his final position on the base layer with pinCordinates in mind
        /// </summary>
        /// <param name="baseLayer"> </param>
        public virtual void Imprint(Symbol[][] baseLayer)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    baseLayer[pinY + y][PinX + x] = matrix[y][x];
                }
            }
        }

        public virtual void Update(Symbol[][] newMatrix)
        {
            if (newMatrix.Length != matrix.Length || newMatrix[0].Length != matrix[0].Length)
            {
                throw new ArgumentException($"Can't update {title} window because, does not match the window size. ");
            }

            matrix = newMatrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curentlyActiveTiles">Sparse representation of the currently active tiles</param>
        public virtual void Update(Position[] curentlyActiveTiles)
        {
            Clear();
            foreach (var tile in curentlyActiveTiles)
            {
                matrix[tile.CordY][tile.CordX] = tile.Tile.Representation();
            }
        }

        public virtual void Clear()
        {
            for (int row = 0; row <= this.Height - 1; row++)
            {
                this.ClearRow(row);
            }
        }

        public virtual void ClearRow(int row)
        {
            this.matrix[row] =
                this.Matrix[row].Select(symb =>
                {
                    return new Symbol(' ');
                })
                .ToArray();
        }
    }
}
