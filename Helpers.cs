using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cat_Trap
{
    enum Direction
    {
        left,
        leftUp,
        rightUp,
        right,
        rightDown,
        leftDown
    }
    internal class Helpers
    {
        public static readonly MouseListener mouseListener = new();

        // random number generator
        public static Random rng = new();

        // board
        public const int hexes = 11;

        // fraction of hexes starting deactivated
        public static double fraction = 0.2;

        public static Vector2 CatStart = new(hexes / 2);

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

        // jump direction
        public static Direction DetermineJumpDirection(Vector2 from, Vector2 to)
        {
            var relative = to - from;
            
            if (relative == Vector2.UnitX) return Direction.right;
            if (relative == -Vector2.UnitX) return Direction.left;
            switch (from.Y % 2 == 0)
            {
                case true:
                    if (relative == -Vector2.UnitY) return Direction.right; //Up
                    if (relative == new Vector2(-1, -1)) return Direction.left; //Up
                    if (relative == Vector2.UnitY) return Direction.right; //Down
                    if (relative == new Vector2(-1, 1)) return Direction.left; //Down
                    break;
                case false:
                    if (relative == -Vector2.UnitY) return Direction.left; //Up
                    if (relative == new Vector2(1, -1)) return Direction.right; //Up
                    if (relative == Vector2.UnitY) return Direction.left; //Down
                    if (relative == new Vector2(1, 1)) return Direction.right; //Down
                    break;
            }
            throw new InvalidDataException("Vectors not neighboring!");
        }

        // linking
        public static List<Vector2> GetLinking(Vector2 position) => new(){
                position + new Vector2(1, 0),
                position + new Vector2(0, 1),
                position + new Vector2(position.Y % 2 == 0 ? -1 : 1, 1)
        };


        // layout
        public const float marginInside = 6;
        public const float marginOutside = 25;

        // target hexagon
        public static Hexagon target = new(new Vector2(-10, -10));

        // border
        public static bool IsBorderHex(Vector2 position) => position.X == 0 || position.Y == 0 || position.X == hexes - 1 || position.Y == hexes - 1;
    
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
