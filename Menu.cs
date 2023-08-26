using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using System;

namespace Cat_Trap
{
    public class Menu
    {
        RectangleF buttonBounds = new(100, 100, 300, 100);
        bool Active => StateMachine.State == StateMachine.GameState.Menu;
        bool Hovered => buttonBounds.Contains(Mouse.GetState().Position) && Active;
        public Menu() 
        {
            Helpers.mouseListener.MouseClicked += HandleClick;
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Helpers.newGameButton, (Rectangle)buttonBounds, Hovered ? Color.Red : Color.White);
        }

        void HandleClick(object sender, MouseEventArgs e)
        {
            if (!Hovered) return;
            StateMachine.NewGame();
        }
    }
}
