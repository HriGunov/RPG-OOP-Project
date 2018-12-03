using Models;

namespace Core.Windows
{
    public class PopUpWindow : BorderedWindow
    {
        protected bool popUp = false;

        public PopUpWindow(string title, int pinX, int pinY, int width, int height, bool includeTitle = false, int layer = 0) : base(title, pinX, pinY, width, height, includeTitle, layer)
        {

        }

        public PopUpWindow(string title, int pinX, int pinY, int width, int height, Symbol borderSymnol, bool includeTitle = false, int layer = 0) : base(title, pinX, pinY, width, height, borderSymnol, includeTitle, layer)
        {

        }

        private bool PopUp => popUp;

        public virtual void Toggle()
        {
            popUp = !popUp;
        }

        public override void Imprint(Symbol[][] baseLayer)
        {
            if (PopUp)
            {
                base.Imprint(baseLayer);
            }
        }
    }
}
