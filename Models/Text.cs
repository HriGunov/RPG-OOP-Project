using System;
using System.Linq;

using Models.Interfaces;
using OpenTK.Graphics;

namespace Models
{
    public class Text : IText
    {
        public readonly string value = "";
        private readonly Symbol[] symbols;
        private readonly Color4 color = Color4.White;

        public Text()
        {

        }

        public Text(string value)
        {
            if (value == null)
                throw new ArgumentException("Value cannot be null");

            this.value = value;

            symbols = value
                .Select(c => new Symbol(c, color))
                .ToArray();
        }

        public Text(string value, Color4 color)
            : this(value)
        {
            this.color = color;
        }

        public Text(Symbol[] symbols)
        {
            if (value == null)
                throw new ArgumentException("Symbols cannot be null");

            this.value = string.Join("", symbols.Select(x => x.Value));
            this.symbols = symbols;
        }

        public string Value
        {
            get
            {
                return this.value;
            }
        }

        public Symbol[] Symbols
        {
            get
            {
                return (Symbol[])this.symbols.Clone();
            }
            set => throw new NotImplementedException();
        }

        public Color4 Color
        {
            get
            {
                return this.color;
            }
        }
    }
}
