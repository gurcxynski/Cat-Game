using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Diagnostics;

namespace Cat_Trap
{
    internal class Cat
    {
        public Vector2 Position { get; set; }
        public Vector2 DrawnPosition { get => isJumping ? LerpPosition : Helpers.ConvertToPixelPosition(Position); }

        public bool isJumping { get; private set; } = false;
        public bool escaped { get; private set; } = false;

        private Vector2 LerpPosition;
        Vector2 Destination;
        double StartedJump;

        public AnimatedSprite Sprite { get; }
        public Cat(AnimatedSprite sprite) 
        {
            Sprite = sprite;
            Position = new Vector2(5, 5);
        }
        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);

            if (escaped) Sprite.Alpha -= (float)(0.0015 * gameTime.ElapsedGameTime.Milliseconds);

            if (!isJumping) return;

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
            if (dest == Position || isJumping) return;
            if (dest == Helpers.target.Position)
            {
                JumpOff();
                return;
            }
            Destination = dest;
            isJumping = true;

            var animation = Helpers.DetermineJumpDirection(Position, dest).ToString();
            Sprite.Play(animation);
        }
        void Land()
        {
            Position = Destination;
            isJumping = false;
            Destination = Vector2.Zero;
            StartedJump = 0;
            LerpPosition = Vector2.Zero;
            Sprite.Play("idle");
        }

        void JumpOff()
        {
            if (Position.X == 0) Jump(Position - Vector2.UnitX);
            else if (Position.Y == 0) Jump(Position - Vector2.UnitY);
            else if(Position.X == Helpers.hexes - 1) Jump(Position + Vector2.UnitX);
            else if(Position.Y == Helpers.hexes - 1) Jump(Position + Vector2.UnitY);
            escaped = true;
        }
    }
}
