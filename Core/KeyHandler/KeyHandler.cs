using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Windows;
using Models.Interfaces;
using Models.Unit.Player;

namespace Core
{
    public  class KeyHandler
    {
        private readonly SelectablePopUpWindow inventory;
        private readonly CombatManager combatManager;
        private readonly Movement movement;

        public KeyHandler(SelectablePopUpWindow inventory, CombatManager combatManager,Movement movement)
        {
            this.inventory = inventory;
            this.combatManager = combatManager;
            this.movement = movement;
        }

        public void Handle(Key input)
        {            
            switch (input)
            {
                case Key.Up:
                case Key.Keypad8:
                    PlayerMovementInput(-1, 0);
                    break;
                case Key.Down:
                case Key.Keypad2:
                    PlayerMovementInput(1, 0);
                    break;
                case Key.Left:
                case Key.Keypad4:
                    PlayerMovementInput(0, -1);
                    break;
                case Key.Right:
                case Key.Keypad6:
                    PlayerMovementInput(0, 1);
                    break;

                //Additional numpad controls 
                case Key.Keypad7:
                    PlayerMovementInput(-1, -1);
                    break;
                case Key.Keypad9:
                    PlayerMovementInput(-1, 1);
                    break;
                case Key.Keypad3:
                    PlayerMovementInput(1, 1);
                    break;
                case Key.Keypad1:
                    PlayerMovementInput(1, -1);
                    break;
                case Key.L:
                    Engine.EnemyInfoWindow.Toggle();
                    break;

                case Key.I: inventory.Toggle();
                    break;
                case Key.O:
                    combatManager.OnePunchMode  = !combatManager.OnePunchMode;
                    break;
                     
                //Do nothing
                case Key.Space:
                     break;
                case Key.Keypad5:
                    break;
                
                default:
                    break; ;
            }
        }
         private void PlayerMovementInput(int deltaY,int delaX)
         {
             ICoordinates newLocation = Engine.Player.Location.GetCoordinatesWithOffSet(deltaY,delaX);

                //check validity
             var neigbours = Engine.Player.Map.Neighbours(Engine.Player.Location.CordY, Engine.Player.Location.CordX);
             if (neigbours.Any(n => n.CordY == newLocation.CordY && n.CordX == newLocation.CordX))
             {
                 //If next position is occoupied
                 if (Engine.Player.Map[newLocation.CordY,newLocation.CordX].Tile.Occoupied)
                 {
                     var target = Engine.Player.Map.AwakenedEnemies.First(enemy =>
                         enemy.Location.CordY == newLocation.CordY && enemy.Location.CordX == newLocation.CordX);
                    combatManager.HandlePlayerAtack(Engine.Player, target);
                 }

                if (movement.CanBeMovedTo(newLocation))
                 {
                        //Move
                     movement.Move(Engine.Player,newLocation);
                 }
             }

         }
    }
}
