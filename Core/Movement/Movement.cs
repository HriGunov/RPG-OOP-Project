using System;
using System.Linq;
using Models;
using Models.Unit.Interfaces;
using Models.Interfaces;
using Models.Tile.Interfaces;

namespace Core
{
    public class Movement
    {
        private readonly Random randomProvider;

        public Movement(Random randomProvider)
        {
            this.randomProvider = randomProvider;
        }

        void Move(IUnit unit, int row, int col)
        {
            //Sets unit's current location to be unoccoupied
            unit.Map[unit.Location.CordY, unit.Location.CordX].Tile.Occoupied = false;
            //then sets the target location to be occoupied
            unit.Map[row, col].Tile.Occoupied = false;
            unit.Location.Change(row, col);
        }

        /// <summary>
        /// Assumes all both parameters are corrent, only does movement of the pointers
        /// </summary>
        /// <param name="unit">Unit that will move</param>
        /// <param name="cords">Destination coordinates</param>
        public void Move(IUnit unit, ICoordinates newCords)
        {
            //Sets unit's current location to be unoccoupied
            unit.Map[unit.Location.CordY, unit.Location.CordX].Tile.Occoupied = false;

            //then sets the target location to be occoupied
            unit.Map[newCords.CordY, newCords.CordX].Tile.Occoupied = true;
            unit.Location.Change(newCords);
        }

        public bool CanBeMovedTo(ICoordinates cords)
        {
            if (Engine.Map.Contains(cords))
            {
                ITile tile = Engine.Map.Matrix[cords.CordY][cords.CordX].Tile;
                return CanBeMovedTo(tile);
            }
            return false;
        }

        public bool CanBeMovedTo(Map map, ICoordinates cords)
        {
            if (map.Contains(cords))
            {
                ITile tile = map.Matrix[cords.CordY][cords.CordX].Tile;
                return CanBeMovedTo(tile);

            }
            return false;
        }

        private bool CanBeMovedTo(ITile tile)
        {
            if (tile.CanWalk && (tile.Occoupied == false))
            {
                return true;
            }
            return false;
        }

        public void MoveRandom(IUnit unit)
        {
            var moveableNeighbours = unit.Map.Neighbours(unit.Location.CordY, unit.Location.CordX).
                Where(pos => CanBeMovedTo(pos.Tile) == true).ToArray();

            if (moveableNeighbours.Length > 0)
            {
                Move(unit, moveableNeighbours[randomProvider.Next(moveableNeighbours.Length)]);
            }
        }
    }
}
