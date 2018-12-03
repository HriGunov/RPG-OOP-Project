using System.Collections.Generic;
using Core.Windows.PopUp;
using Models;

namespace Core.Windows
{
    public class SelectablePopUpWindow : PopUpWindow
    {
        public SelectablePopUpWindow(string title, int pinX, int pinY, int width, int height, bool includeTitle = false, int layer = 0)
            : base(title, pinX, pinY, width, height, includeTitle, layer)
        {
        }

        public SelectablePopUpWindow(string title, int pinX, int pinY, int width, int height, Symbol borderSymnol, bool includeTitle = false, int layer = 0)
            : base(title, pinX, pinY, width, height, borderSymnol, includeTitle, layer)
        {
            //testing purposes
            selectables.Add(new SelectableSymbols("Hehe", 2, 2));
        }

        List<SelectableSymbols> selectables = new List<SelectableSymbols>();

        void Update()
        {
            Clear();
            foreach (var element in selectables)
            {
                SetTextLine(element.Text.Symbols, element.StartRow);
            }
        }
    }
}
