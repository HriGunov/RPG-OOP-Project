using OpenTK.Graphics;

namespace Models
{
    public class Symbol
    {
        private char value;
        private Color4 color;

        public Symbol() : this(' ')
        {

        }

        public Symbol(char value) : this(value, Color4.White)
        {

        }

        public Symbol(char value, Color4 color) : this(value, color, Color4.Black)
        {
            BackgroundColor = Color4.Black;
        }

        public Symbol(Symbol sym) : this(sym.Value, sym.Color, sym.BackgroundColor)
        {

        }

        public Symbol(char value, Color4 color, Color4 backgroundColor)
        {
            this.Value = value;
            this.Color = color;
            this.BackgroundColor = backgroundColor;
        }

        public char Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Color4 Color
        {
            get { return this.color; }
            set { color = value; }
        }

        public Color4 BackgroundColor { get; set; }

        public static Symbol[] ParseString(string str)
        {
            var parsedStr = new Symbol[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                parsedStr[i] = new Symbol(str[i]);
            }

            return parsedStr;
        }
    }
}
