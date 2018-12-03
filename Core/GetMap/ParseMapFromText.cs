using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Models.Factories;
using Models.Unit;
using Models.Unit.Player;
using Models;
using Models.Tile.Interfaces;
using Models.Tile;
using Models.Unit.Enemies.Interfaces;
using Core.GetMap.Interfaces;

namespace Core.GetMap
{
    public class ParseMapFromText : IGetMap
    {
        List<string> lines;
        private string sourcePath;
        private List<IEnemy> enemiesToPopulateMap = new List<IEnemy>();

        public ParseMapFromText() : this(AppDomain.CurrentDomain.BaseDirectory + "Default.map")
        {

        }

        public ParseMapFromText(string sourcePath)
        {
            this.SourcePath = sourcePath;
            lines = File.ReadLines(SourcePath).ToList();
        }

        public ParseMapFromText(List<string> lines)
        {
            this.lines = lines;
        }

        public string SourcePath
        {
            get { return this.sourcePath; }
            set { this.sourcePath = value; }
        }

        public Map GetLevel => GenerateLevel();

        private Map GenerateLevel()
        {
            int lenght = lines.Max(l => l.Length);
            int height = lines.Count;

            Position[][] mapMatrix = new Position[height][];
            for (int i = 0; i < height; i++)
            {
                mapMatrix[i] = new Position[lenght];
                for (int j = 0; j < lenght; j++)
                {
                    ITile tile = new WallTile();

                    char ch = '#';
                    if (j < lines[i].Length)
                    {
                        ch = lines[i][j];

                        // bool thereIsUnit = false;    =>>> It's never used?

                        //Checks if its a unit so we can set the ground under it
                        if (ch == '@' || Register.RegisteredEnemies.ContainsKey(ch))
                        {
                            if (ch == '@')
                            {
                                Engine.Player = new Player(new UnitPostion(i, j), null);

                                ch = '.';

                            }
                            else
                            {
                                var enemy = ModelFactory.ParseEnemy(ch, new UnitPostion(i, j));
                                enemiesToPopulateMap.Add(enemy);
                                ch = '.';
                            }
                        }

                        if (Register.RegisteredTiles.ContainsKey(ch))
                        {
                            tile = ModelFactory.ParseTile(ch);
                        }
                        else
                        {
                            tile = ModelFactory.ParseTile('#');
                        }

                    }

                    mapMatrix[i][j] = new Position(i, j, tile);
                }
            }

            // enemiesToPopulateMap.Add(ParseChar.ParseEnemy('d', new UnitPostion(3, 3)));

            Map mapWithoutEnemies = new Map(mapMatrix);
            foreach (IEnemy enemy in enemiesToPopulateMap)
            {
                enemy.Map = mapWithoutEnemies;
                mapWithoutEnemies[enemy.Location.CordY, enemy.Location.CordX].Tile.Occoupied = true;
                mapWithoutEnemies.Enemies.Add(enemy);
            }

            try
            {
                Engine.Player.Map = mapWithoutEnemies;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Map has no spawn position");
            }
            mapWithoutEnemies[Engine.Player.Location.CordY, Engine.Player.Location.CordX].Tile.Occoupied = true;
            return mapWithoutEnemies;
        }
    }
}
