using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System.Diagnostics;

namespace Cat_Trap
{
    internal class Cat
    {
        public Vector2 Position { get; set; }
        public Vector2 DrawnPosition { get => IsJumping ? LerpPosition : Helpers.ConvertToPixelPosition(Position); }

        public static bool IsJumping { get => StateMachine.State == StateMachine.GameState.CatJumping; }
        public static bool Escaped { get => StateMachine.State == StateMachine.GameState.CatEscaping; }

        private Vector2 LerpPosition;
        Vector2 Destination;
        double StartedJump;

        public AnimatedSprite Sprite { get; }
        public Cat(AnimatedSprite sprite) 
        {
            Sprite = sprite;
            Position = Helpers.CatStart;
        }
        public void Reset()
        {
            Position = Helpers.CatStart;
            Sprite.Alpha = 1;
            Sprite.Play("idle");
            Destination = Vector2.Zero;
            StartedJump = 0;
            LerpPosition = Vector2.Zero;
        }
        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);

            if (Escaped) Sprite.Alpha -= (float)(0.0015 * gameTime.ElapsedGameTime.Milliseconds);

            if (Escaped && Sprite.Alpha <= 0.3)
            {
                StateMachine.GameOver();
            }

            if (StateMachine.State != StateMachine.GameState.CatJumping && StateMachine.State != StateMachine.GameState.CatEscaping) return;

            if (StartedJump == 0) StartedJump = gameTime.TotalGameTime.TotalMilliseconds;

            var wayCompleted = (gameTime.TotalGameTime.TotalMilliseconds - StartedJump) / Helpers.JumpTime;
            if (wayCompleted >= 1) { Land(); wayCompleted = 1; }
            LerpPosition = Vector2.Lerp(Helpers.ConvertToPixelPosition(Position), Helpers.ConvertToPixelPosition(Destination), (float)wayCompleted);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, DrawnPosition);
        }
        public void Jump(Vector2 dest)
        {
            if (dest == Position || IsJumping) return;
            if (dest == Helpers.target.Position)
            {
                JumpOff();
                return;
            }
            Destination = dest;
            StateMachine.BeginJump();

            var animation = Helpers.DetermineJumpDirection(Position, dest).ToString();
            Sprite.Play(animation);
        }
        void Land()
        {
            Position = Destination;
            StateMachine.EndJump();
            Destination = Vector2.Zero;
            StartedJump = 0;
            LerpPosition = Vector2.Zero;
            Sprite.Play("idle");
        }

        void JumpOff()
        {
            if (Position.X == 0) Jump(Position - Vector2.UnitX);
            else if (Position.Y == 0) Jump(Position - Vector2.UnitY);
            else if (Position.X == Helpers.hexes - 1) Jump(Position + Vector2.UnitX);
            else if (Position.Y == Helpers.hexes - 1) Jump(Position + Vector2.UnitY);
            StateMachine.Escaped();
        }
    }
}
