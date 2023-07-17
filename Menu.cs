using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Cat_Trap
{
    internal class Menu
    {
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new RectangleF(100, 100, 400, 100), Color.Pink);
        }

        internal void Update()
        {
            
        }
    }
}
