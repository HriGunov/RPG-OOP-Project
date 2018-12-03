using Core.Extension;
using Core.GetMap;
using Core.Logger;
using Core.Windows;
using Models;
using Models.Factories;
using Models.Unit.Player;
using OpenTK.Graphics;
using SunshineConsole;
using System;
using System.Linq;

namespace Core
{

    using Core.Interfaces;

    public class Engine : IEngine
    {

        public const int ConsoleWidth = 75;
        public const int ConsoleHeight = 30;

        public const int GameplayWindowWidth = 51;
        public const int GameplayWindowHeight = 25;

        private readonly ConsoleWindow console;
        private readonly WindowsManager windowsManager;
        private readonly ILogger logger;
        private readonly CombatManager combatManager;
        private readonly Visualization visualization;
        private readonly KeyHandler keyHandler;
        private readonly SelectablePopUpWindow inventoryWindow;
        private readonly Movement movement;
        private readonly IMathFunctions math;

        private static bool Run = true;

        public static IPlayer Player;


        public static SelectablePopUpWindow EnemyInfoWindow = new SelectablePopUpWindow("EnemyInfo", 5, 20, 25, 20, new Symbol('*'), false, 14);

        public static Map Map;
        public const bool Debug = false;

        public static int TurnCounter = 1;
        public static int KillCounter = 0;
        private Random randomProvider;
        
        // Constructor
        public Engine(ConsoleWindow console, WindowsManager windowsManager, 
            ILogger logger, CombatManager combatManager,
            KeyHandler keyHandler, Visualization visualization, 
            Movement movement, SelectablePopUpWindow inventoryWindow, 
            Random randomProvider, IMathFunctions math
            )
        {
            this.console = console;
            this.windowsManager = windowsManager;
            this.logger = logger;
            this.combatManager = combatManager;
            this.visualization = visualization;
            this.keyHandler = keyHandler;
            this.inventoryWindow = inventoryWindow;
            this.movement = movement;
            this.randomProvider = randomProvider;
            this.math = math;
        }

        public void Start()
        {
            Register.Populate();
            //Run Menu/startup logic for starting a game/creating character

            Map = new ParseMapFromText().GetLevel;

            var gameplayWindow =
                new BorderedWindow("Gameplay", 5, 0, GameplayWindowWidth, GameplayWindowHeight, false, 1);

            var hpWindow = new StatsWindow(0, 0, GameplayWindowWidth, 4, Player, 1);
            var atrWindow = new AttrubitesWindow("Attributes", 0, GameplayWindowWidth, 24, 5, new Symbol(' '), true, Player);
            var combatStats = new CombatStats("Combat Stats", 6, GameplayWindowWidth + 1, 23, 12, new Symbol(' '), true, Player);
            var borderUnderAtrWin = new TextLineWindow(5, GameplayWindowWidth, 24, Color4.White, Color4.Green, 5);
            borderUnderAtrWin.Update(new Symbol[] { });
            var turnCountWin = new TextLineWindow(ConsoleHeight - 1, 1, 15, Color4.White, Color4.Green, 2);
            turnCountWin.SetTextLine(Symbol.ParseString($"Turn: {TurnCounter}"), 0);

            var logWindow = new LogWindow("Log", 8, GameplayWindowWidth, 24, 20, 55);

            //LogWindow currently is not supported

            //Logger = logWindow;

            windowsManager.AddWindow(gameplayWindow);
            windowsManager.AddWindow(hpWindow);
            windowsManager.AddWindow(atrWindow);
            windowsManager.AddWindow(combatStats);
            windowsManager.AddWindow(borderUnderAtrWin);

            windowsManager.AddWindow(turnCountWin);
            windowsManager.AddWindow(inventoryWindow);
            windowsManager.AddWindow(EnemyInfoWindow);
            // WindowsManager.AddWindow(logWindow);
            visualization.UpdateGameplayWindow(gameplayWindow);
            hpWindow.Update();
            atrWindow.Update();

            visualization.PrintToConsole(windowsManager, console);
            var debugWindow1 = new Window("debug1", 0, 0, 70, 4, 10);
            var debugWindow2 = new Window("combatDebug", 5, GameplayWindowWidth, 23, 20, 10);
            if (Debug)
            {

                windowsManager.AddWindow(debugWindow1);
                windowsManager.AddWindow(debugWindow2);

            }


            // Finally, update the window until a key is pressed or the window is closed:
            while (console.WindowUpdate() && Run)
            {
                if (console.KeyPressed)
                {
                    //Handle player input

                    keyHandler.Handle(console.GetKey());

                    //Wake up enemies and setsup path
                    foreach (var enemy in Map.Enemies)
                    {
                        if (enemy.CanSee(Player.Location, math))
                        {
                            //Wake up enemies
                            if (!Map.AwakenedEnemies.Contains(enemy))
                            {
                                Map.AwakenedEnemies.Add(enemy);
                            }

                            enemy.UpdatePath(Player.Location, math);
                        }

                    }
                    //Enemy Activity
                    foreach (var enemy in Map.AwakenedEnemies)
                    {
                        //Checks if enemy can atack else it moves
                        if (enemy.Map.Neighbours(enemy.Location).Any(neighbour => neighbour.CordY == Player.Location.CordY && neighbour.CordX == Player.Location.CordX))
                        {
                            var successfulHit = combatManager.HandleAttack(enemy, Player);
                            if (successfulHit && !combatManager.OnePunchMode)
                            {
                                if (Player.Dead)
                                {
                                    Run = false;
                                }
                            }

                        }
                        else
                        {
                            //Simulates diffrent running speeds so the player can try and run from enemies
                            bool skipMovingTowardsPlayer = true;
                            int movementScore = enemy.Attributes.Agility + 1;

                            if (movementScore <= 0)
                            {
                                movementScore = 1;
                            }
                            else if (movementScore >= 10)
                            {
                                skipMovingTowardsPlayer = false;
                            }
                            else
                            {
                                skipMovingTowardsPlayer = movementScore < randomProvider.Next(10);
                            }

                            if (!skipMovingTowardsPlayer && enemy.MovementQueue.Count != 0 && movement.CanBeMovedTo(enemy.MovementQueue.Peek()))
                            {
                                movement.Move(enemy, enemy.MovementQueue.Dequeue());

                            }
                            else
                            {
                                movement.MoveRandom(enemy);
                            }
                            //else do nothing
                        }
                    }

                    TurnCounter++;
                    combatManager.MassRegenerate(Map.AwakenedEnemies, Engine.Player);

                    hpWindow.Update();
                    atrWindow.Update();

                    //logWindow.Update(); 
                    turnCountWin.SetTextLine(Symbol.ParseString($"Turn: {TurnCounter}"), 0);
                    visualization.UpdateGameplayWindow(gameplayWindow);

                    visualization.PrintToConsole(windowsManager, console);
                }
            }
        }
    }
}
