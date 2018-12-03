namespace Core.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Logger;
    using Models;

    /// <summary>
    /// Logs a new lines to the window and can't overflow
    /// </summary>
    public class LogWindow : Window, ILogger
    {
        private List<string> logHistory;

        private int horizontalOffset;
        int lineCounter = 1;
        // TODO => Can I avoid the same code blocks in both constructors?
        public LogWindow(string title, int pinY, int pinX, int width, int height, int layer = 0)
            : base(title, pinY, pinX, width, height, layer)
        {
            this.logHistory = new List<string>();
            this.CurrRow = 1;
            horizontalOffset = 1;
        }

        public List<string> LogHistory
        {
            get
            {
                return new List<string>(this.logHistory);
            }
        }

        private int CurrRow { get; set; }

        public void Log(string line)
        {
            var lines = line.Split('\n');

            List<string> linesToPrint = new List<string>();
            foreach (string currLine in lines)
            {
                if (currLine.Length < this.Width)
                {
                    logHistory.Add(currLine);
                }
                else
                {
                    Queue<string> words = new Queue<string>(
                        currLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray());

                    List<string> logLine = new List<string>();

                    for (int i = 0; i < words.Count; i++)
                    {
                        var word = words.Dequeue();
                        if (logLine.Sum(x => x.Length) + (logLine.Count - 2) * 2 < this.Width && words.Count != 0)
                        {
                            logLine.Add(word);
                        }
                        else
                        {
                            logHistory.Add(string.Join(" ", logLine));
                        }
                    }

                    if (logLine.Count != 0)
                    {
                        logHistory.Add(string.Join(" ", logLine));
                    }
                }
            }
        }

        public void WriteLine(string str, int row, int col)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int position = i;
                Matrix[row][position] = new Symbol(str[i]);
            }
        }

        public void Clear(bool eraseLogHistory)
        {
            if (eraseLogHistory)
                logHistory.Clear();

            base.Clear();
        }

        public void Update()
        {
            Clear();
            int curCounter = 0;

            for (int i = logHistory.Count - 1; i >= 0 && i < Width; i--, curCounter++)
            {
                WriteLine(logHistory[i], lineCounter, 0);
            }
        }
    }
}
