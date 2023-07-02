using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;


namespace Cat_Trap
{
    internal class Hexagon
    {
        const float height = Helpers.height;
        const float width = Helpers.width;

        public Vector2 Position { get; }
        public Vector2 DrawnPosition { get => Helpers.ConvertToPixelPosition(Position); }

        public Polygon Bounds;

        public bool Active { get; set; } = true;

        public Hexagon(Vector2 position)
        {
            Position = position;
                        
            Bounds = new Polygon(Helpers.vertices);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawPolygon(DrawnPosition, Bounds, Active ? Color.Green : Color.DarkGreen, 30);
        }
        public bool IsInside(Vector2 point)
        {
            Vector2 relative = (point - DrawnPosition).ToAbsoluteSize();

            if (relative.Y < height / 4 && relative.X < width / 2) return true;

            if (relative.Y > height / 4 && relative.Y < height / 2 - 0.5 * relative.X && relative.X < width / 2) return true;

            return false;

        }
    }
}
