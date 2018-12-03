namespace Models
{
    using System;
    using System.Collections.Generic;

    using Models.Interfaces;
    using Models.Unit.Enemies.Interfaces;

    public class Map : IMap
    {
        private int height;
        private int width;
        private Position[][] matrix;
        private List<IEnemy> enemies;
        private List<IEnemy> awakenedEnemies;
        private const int DEFAULT_DIM_SIZE = 20;

        public Map() : this(DEFAULT_DIM_SIZE)
        {

        }

        public Map(int dimentions) : this(dimentions, dimentions)
        {

        }

        public Map(Position[][] matrix)
        {
            Matrix = matrix;
            Width = Matrix[0].Length;
            Height = Matrix.Length;

            Enemies = new List<IEnemy>();
            AwakenedEnemies = new List<IEnemy>();
        }

        public Map(int rows, int cols)
        {
            Height = rows - 1;
            Width = cols - 1;

            matrix = new Position[rows][];
            for (int y = 0; y < rows; y++)
            {
                matrix[y] = new Position[cols];
                for (int x = 0; x < cols; x++)
                {
                    Matrix[y][x] = new Position(y, x);
                }
            }

            Enemies = new List<IEnemy>();
            AwakenedEnemies = new List<IEnemy>();
        }

        public Position this[int row, int col] => matrix[row][col];

        public Position[][] Matrix { get => matrix; set => matrix = value; }

        public int Height { get => height; private set => height = value; }
        public int Width { get => width; private set => width = value; }

        public bool Contains(int row, int col)
        {
            if (row < 0 || row > Height)
                return false;

            if (col < 0 || col > Width)
                return false;

            return true;
        }

        public bool Contains(ICoordinates pos)
        {
            return Contains(pos.CordY, pos.CordY);
        }

        public List<IEnemy> Enemies
        {
            get => enemies;
            set => enemies = value;
        }

        public List<IEnemy> AwakenedEnemies
        {
            get => awakenedEnemies;
            set => awakenedEnemies = value;
        }

        /// <summary>
        /// Checks if the 8 Neighbours exist
        /// </summary>
        /// <returns>returns all valid Neighbours</returns>
        public ICollection<Position> Neighbours(int cordY, int cordX)
        {
            var neighbours = new List<Position>(8);

            if (Contains(cordY + 1, cordX)) { neighbours.Add(Matrix[cordY + 1][cordX]); }
            if (Contains(cordY + 1, cordX + 1)) { neighbours.Add(Matrix[cordY + 1][cordX + 1]); }
            if (Contains(cordY + 1, cordX - 1)) { neighbours.Add(Matrix[cordY + 1][cordX - 1]); }
            if (Contains(cordY, cordX + 1)) { neighbours.Add(Matrix[cordY][cordX + 1]); }
            if (Contains(cordY, cordX - 1)) { neighbours.Add(Matrix[cordY][cordX - 1]); }
            if (Contains(cordY - 1, cordX)) { neighbours.Add(Matrix[cordY - 1][cordX]); }
            if (Contains(cordY - 1, cordX + 1)) { neighbours.Add(Matrix[cordY - 1][cordX + 1]); }
            if (Contains(cordY - 1, cordX - 1)) { neighbours.Add(Matrix[cordY - 1][cordX - 1]); }

            if (neighbours.Count == 0) { throw new ArgumentOutOfRangeException("Invalid position or level contains only 1 position"); }

            return neighbours;
        }

        public ICollection<Position> Neighbours(ICoordinates pos)
        {
            return Neighbours(pos.CordY, pos.CordX);
        }
    }
}
