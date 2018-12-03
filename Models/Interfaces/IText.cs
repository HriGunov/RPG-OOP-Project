using OpenTK.Graphics;

namespace Models.Interfaces
{
    public interface IText
    {
        string Value { get; }

        Symbol[] Symbols { get; }

        Color4 Color { get; }
    }
}
