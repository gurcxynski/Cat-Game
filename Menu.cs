using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;

namespace Cat_Trap
{
    public class Menu
    {
        RectangleF buttonBounds = new((Helpers.windowWidth - Helpers.buttonWidth) / 2, (Helpers.windowHeight - Helpers.buttonHeight) / 2,
            Helpers.buttonWidth, Helpers.buttonHeight);

        static bool Active => StateMachine.State == StateMachine.GameState.Menu;
        bool Hovered => buttonBounds.Contains(Mouse.GetState().Position) && Active;
        public Menu() 
        {
            Helpers.mouseListener.MouseClicked += HandleClick;
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Helpers.newGameButton, (Rectangle)buttonBounds, Hovered ? new Color(230, 230, 230, 200) : new Color(255, 255, 255, 230));
        }

        void HandleClick(object sender, MouseEventArgs e)
        {
            if (!Hovered) return;
            StateMachine.NewGame();
        }
    }
}
