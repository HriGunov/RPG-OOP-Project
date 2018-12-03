using System;

using Models;
using OpenTK.Graphics;

namespace Core.Windows.PopUp
{
    public class SelectableSymbols
    {
        private Text text;
        private int startRow;
        private int startCol;
        private bool selected;

        public SelectableSymbols(string text, int startRow, int startCol, bool selected = false)
            : this(new Text(text), startRow, startCol, selected)
        {

        }

        public SelectableSymbols(Text text, int startRow, int startCol, bool selected = false)
        {
            Text = text;
            StartRow = startRow;
            StartCol = startCol;
            Selected = selected;
        }

        public Text Text { get => text; set => text = value; }
        public int StartRow { get => startRow; set => startRow = value; }
        public int StartCol { get => startCol; set => startCol = value; }
        public bool Selected
        {
            get => selected;
            set
            {
                if (Selected)
                {
                    foreach (var symbol in Text.Symbols)
                    {
                        symbol.Color = Color4.Black;
                        symbol.BackgroundColor = Color4.White;
                    }
                }
                else
                {
                    foreach (var symbol in Text.Symbols)
                    {
                        symbol.Color = Color4.White;
                        symbol.BackgroundColor = Color4.Black;
                    }
                }

                selected = value;
            }
        }

        public void Activate(Action<string> activation)
        {
            activation(Text.value);
        }
    }
}
