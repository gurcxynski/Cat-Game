using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using System.Collections.Generic;

namespace Cat_Trap
{
    internal class Hexagon
    {
        const float height = Helpers.hexHeight;
        const float width = Helpers.hexWidth;

        public int Value { get; private set; } = int.MaxValue;

        public Vector2 Position { get; }
        public Vector2 DrawnPosition { get => Helpers.ConvertToPixelPosition(Position); }

        public Polygon Bounds;

        public List<Hexagon> Linked { get; }

        public bool Active { get; private set; } = true;

        public Hexagon(Vector2 position)
        {
            Position = position;
                        
            Bounds = new Polygon(Helpers.vertices);

            Linked = new List<Hexagon>();
        }
        public void Link(Hexagon hexagon)
        {
            if (hexagon is null || hexagon == this || Linked.Contains(hexagon)) return;
            Linked.Add(hexagon);
            hexagon.Link(this);
        }
        public void Link(List<Hexagon> hexagons)
        {
            if (hexagons is null) return;
            hexagons.ForEach(hexagon => { hexagon.Link(this); });
            
        }
        public void Unlink(Hexagon hexagon)
        {
            if (hexagon is null || hexagon == this || !Linked.Contains(hexagon)) return;
            Linked.Remove(hexagon);
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
        public void Deactivate()
        {
            Active = false;
            Linked.ForEach(hex => hex.Unlink(this));
            Linked.Clear();
        }
        public void GenerateValue(int arg)
        {
            if (arg > Value) return;
            Value = arg;
            Linked.ForEach(hex => hex.GenerateValue(Value + 1));
        }

        public void ResetValue() => Value = int.MaxValue;
    }
}
