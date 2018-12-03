using System;
using System.Collections.Generic;

using Models.Items.Interfaces;
using Models.Tile.Interfaces;
using OpenTK.Graphics;

namespace Models.Tile.Abstract
{
    public abstract class DefaultTile : ITile
    {
        private bool blocksSight;
        private bool canWalk;
        private bool canActivate;
        private bool visible;
        private bool occoupied;
        private bool seenBefore;

        private Symbol visibleSymbol;

        public static Symbol SeenNotVisibleSymbol = new Symbol(' ', Color4.Black, Color4.Gray);
        public static Symbol NotSeenNotVisibleSymbol = new Symbol(' ', Color4.Black, Color4.Black);

        public DefaultTile() : this('%')
        {

        }

        protected DefaultTile(Symbol symbol, bool blocksSight = true, bool canWalk = false, bool canActivate = false)
        {
            BlocksSight = blocksSight;
            CanWalk = canWalk;
            CanActivate = canActivate;
            VisibleSymbol = symbol;
        }

        protected DefaultTile(char characher, bool blocksSight = true, bool canWalk = false, bool canActivate = false) : this(new Symbol(characher), blocksSight, canWalk, canActivate)
        {

        }

        public bool BlocksSight { get => blocksSight; protected set => blocksSight = value; }
        public bool CanWalk { get => canWalk; protected set => canWalk = value; }
        public bool CanActivate { get => canActivate; protected set => canActivate = value; }

        public bool Visible
        {
            get => visible; set
            {
                SeenBefore = true;
                visible = value;
            }
        }

        public List<IItem> Items { get; }

        public void Activate()
        {
            if (CanActivate)
            {
                throw new NotImplementedException();
            }
        }

        public bool Occoupied
        {
            get => occoupied;
            set
            {
                if (value == true)
                {
                    CanWalk = false;
                }
                else
                {
                    CanWalk = true;
                }

                occoupied = value;
            }

        }

        public Symbol Representation()
        {
            if (Visible)
            {
                return VisibleSymbol;
            }
            else
            {
                if (SeenBefore)
                {
                    return SeenNotVisibleSymbol;
                }
                return NotSeenNotVisibleSymbol;
            }
        }

        public bool SeenBefore
        {
            get => seenBefore;
            set
            {

                seenBefore = value;
            }
        }

        public Symbol VisibleSymbol
        {
            get => visibleSymbol;
            set => visibleSymbol = value;
        }

        public abstract char RegistrationKey { get; }
    }
}
