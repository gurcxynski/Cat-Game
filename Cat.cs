using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace Cat_Trap
{
    internal class Cat
    {
        public Vector2 Position { get; set; }
        public Vector2 DrawnPosition { get => isJumping ? LerpPosition : Helpers.ConvertToPixelPosition(Position); }

        public bool isJumping = false;
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
            Destination = dest;
            isJumping = true;
            Sprite.Play("jump");
        }
        void Land()
        {
            Position = Destination;
            isJumping = false;
            Destination = Vector2.Zero;
            StartedJump = 0;
            LerpPosition = Vector2.Zero;
        }
    }
}
