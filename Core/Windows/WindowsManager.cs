using System;
using System.Collections.Generic;
using System.Linq;

using Models;

namespace Core.Windows
{
    public class WindowsManager
    {
        private Symbol[][] intermediateConsoleState;
        private static List<Window> windows;

        public WindowsManager()
        {
            Initialise();
        }

        private Symbol[][] IntermediateConsoleState
        {
            get
            {
                return intermediateConsoleState;
            }
        }

        public void Initialise()
        {
            windows = new List<Window>();
            // filling the matrix with Symbols containing spaces (' ')

            intermediateConsoleState = new Symbol[Engine.ConsoleHeight][];
            for (int i = 0; i < Engine.ConsoleHeight; i++)
            {
                intermediateConsoleState[i] = new Symbol[Engine.ConsoleWidth];
                for (int j = 0; j < Engine.ConsoleWidth; j++)
                {
                    intermediateConsoleState[i][j] = new Symbol();
                }
            }
        }

        public void AddWindow(Window window)
        {
            windows.Add(window);

            // check if intersect with windows on same layer
            List<Window> sameLayerWindows = windows
                .Where(x => x.Layer == window.Layer)
                .ToList();

            foreach (Window w in sameLayerWindows.Where(layerWindow => !layerWindow.Equals(window)))
            {
                if (Intersect(window, w))
                    throw new ArgumentException($"Window you try to add intersects with the \"{w.Title}\" window");
            }
        }

        private bool Intersect(Window winA, Window winB)
        {
            bool noIntersect = winA.TopLeftCorner.X > winB.BottomRightCorner.X ||
                               winB.TopLeftCorner.X > winA.BottomRightCorner.X ||
                               winA.TopLeftCorner.Y > winB.BottomRightCorner.Y ||
                               winB.TopLeftCorner.Y > winA.BottomRightCorner.Y;

            return !noIntersect;
        }

        public Symbol[][] GetCurrentState()
        {
            windows = windows.OrderBy(window => window.Layer).ToList();

            foreach (var window in windows)
            {
                window.Imprint(IntermediateConsoleState);
            }

            return IntermediateConsoleState;
        }
    }
}
