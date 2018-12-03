using Core.Windows;
using Models;
using Models.Tile.Interfaces;
using OpenTK.Graphics;
using SunshineConsole;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Core
{
    public class Visualization
    {
        private Symbol[][] latestState;
        private static readonly Symbol errorSymbol = new Symbol('E', Color4.White, Color4.Red);
        private static readonly Symbol errorNullSymbol = new Symbol('N', Color4.White, Color4.Red);
        private static readonly Symbol errorOutOfWindowymbol = new Symbol('O', Color4.White, Color4.Red);
        private static readonly Symbol darkenesSymbol = new Symbol(' ', Color4.Black, Color4.Black);

        private HashSet<ITile> lastVisibleTiles = new HashSet<ITile>();
        private IMathFunctions math;

        public Visualization(IMathFunctions math)
        {
            this.math = math;
            Initialise();
        }
        void Initialise()
        {
            latestState = new Symbol[Engine.ConsoleHeight][]
                .Select(row =>
                {
                    return new Symbol[Engine.ConsoleWidth]
                        .Select(symb => new Symbol(' ', Color4.Black, Color4.Black))
                        .ToArray();
                })
                .ToArray();
        }

        public void PrintToConsole(WindowsManager windowsManager, ConsoleWindow console)
        {
            var currentState = windowsManager.GetCurrentState();

            for (int row = 0; row < Engine.ConsoleHeight; row++)
            {
                for (int col = 0; col < Engine.ConsoleWidth; col++)
                {
                    //nooo need to check if there is actual change in the cell we are writing, the console does that on its own
                    console.Write(row, col, currentState[row][col].Value, currentState[row][col].Color,
                        currentState[row][col].BackgroundColor);
                }
            }
        }


        public void UpdateGameplayWindow(Window gameplayWindow)
        {
            SetPlayerVisibilityTiles();

            int yLeftTopCorner = Engine.Player.Location.CordY - gameplayWindow.Height / 2;
            int xLeftTopCorner = Engine.Player.Location.CordX - gameplayWindow.Width / 2;


            var newState = new Symbol[gameplayWindow.Height][];
            for (int row = 0; row < gameplayWindow.Height; row++)
            {
                newState[row] = new Symbol[gameplayWindow.Width];
                for (int col = 0; col < gameplayWindow.Width; col++)
                {


                    if (row + yLeftTopCorner >= 0 && row + yLeftTopCorner < Engine.Map.Height
                                                  && col + xLeftTopCorner >= 0 &&
                                                  col + xLeftTopCorner < Engine.Map.Width)
                    {
                        var tile = Engine.Map[row + yLeftTopCorner, col + xLeftTopCorner];
                        newState[row][col] = tile.Tile
                            .Representation();

                        if (tile.Tile.Occoupied && tile.Tile.Visible)
                        {

                            if (Engine.Map.Enemies.Exists(enemy => enemy.Location.CordY == (row + yLeftTopCorner) && enemy.Location.CordX == (col + xLeftTopCorner)))
                            {
                                newState[row][col] = Engine.Map.Enemies.Find(enemy =>
                                    enemy.Location.CordY == (row + yLeftTopCorner) &&
                                    enemy.Location.CordX == (col + xLeftTopCorner)).Representation;

                            }
                        }

                    }
                    else
                    {
                        newState[row][col] = darkenesSymbol;
                    }
                }
            }

            //viualises player in the center of the gameplay window

            newState[(gameplayWindow.Height) / 2][gameplayWindow.Width / 2] = Engine.Player.Representation;
            gameplayWindow.Update(newState);
        }

        public void SetPlayerVisibilityTiles()
        {
            var player = Engine.Player;
            HashSet<ITile> visibleTiles = new HashSet<ITile>();
            int visionRange = (10 + (player.Attributes.Perception / 2)) / 2 + 3;

            var circle = math.GetPerimeter(player.Location.CordY, player.Location.CordX, visionRange);


            foreach (var perimeterPoint in circle)
            {
                var rayTracingPath = math.LineAlgorithm(player.Location.CordY, player.Location.CordX,
                                                            perimeterPoint.CordY, perimeterPoint.CordX);
                //makes sure we start from the players location
                if (rayTracingPath[0].CordX != player.Location.CordX || rayTracingPath[0].CordY != player.Location.CordY)
                {
                    rayTracingPath.Reverse();
                }

                foreach (var step in rayTracingPath)
                {

                    if (!(step.CordY < 0 || step.CordX < 0 || step.CordY >= player.Map.Height || step.CordX >= player.Map.Width))
                    {
                        visibleTiles.Add(player.Map[step.CordY, step.CordX].Tile);

                        if (player.Map[step.CordY, step.CordX].Tile.BlocksSight == true)
                        {
                            //shows which tiles block sight
                            if (Engine.Debug)
                            {
                                player.Map[step.CordY, step.CordX].Tile.VisibleSymbol = new Symbol(' ', Color4.AliceBlue, Color4.Red);
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;

                    }
  
                }
            }
            //sets the visible flag for th tiles
            foreach (var tile in visibleTiles)
            {
                tile.Visible = true;
            }
            //sets the visibility flag to false
            lastVisibleTiles.ExceptWith(visibleTiles);
            foreach (var tile in lastVisibleTiles)
            {
                tile.Visible = false;
            }

            lastVisibleTiles = visibleTiles;
        }


    }
}
