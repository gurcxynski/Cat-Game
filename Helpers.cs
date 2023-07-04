using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using System.Collections.Generic;

namespace Cat_Trap
{
    internal class Helpers
    {
        public static readonly MouseListener mouseListener = new();

        // board
        public const int hexes = 11;

        // hexagons
        public const float height = 60;
        public const float width = height * 0.866f; // sqrt(3) / 2

        static readonly Vector2 unitVector = new (height / 2, 0);
        public static readonly List<Vector2> vertices = new(){
                unitVector.Rotate(1f / 6 * MathHelper.Pi),
                unitVector.Rotate(3f / 6 * MathHelper.Pi),
                unitVector.Rotate(5f / 6 * MathHelper.Pi),
                unitVector.Rotate(7f / 6 * MathHelper.Pi),
                unitVector.Rotate(9f / 6 * MathHelper.Pi),
                unitVector.Rotate(11f / 6 * MathHelper.Pi)
        };

        // linking
        public static List<Vector2> GetLinking(Vector2 position) => new(){
                position + new Vector2(1, 0),
                position + new Vector2(0, 1),
                position + new Vector2(position.Y % 2 == 0 ? -1 : 1, 1)
        };

        // layout
        public const float marginInside = 6;
        public const float marginOutside = 25;
    
        // get pixel position of the center of given hexagon
        public static Vector2 ConvertToPixelPosition(Vector2 GridPosition)
        {
            return new(GridPosition.X * (width + marginInside) + width / 2 + marginOutside + (GridPosition.Y % 2 == 0 ? 0 : (width + marginInside) / 2),
                       GridPosition.Y * 0.75f * (height + marginInside) + height / 2 + marginOutside);
        }

        //time of jump animation in ms
        public const int JumpTime = 500;
    }
}
