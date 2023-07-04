using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Input;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cat_Trap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Cat cat;
        private List<Hexagon> hexagons;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = (int)(Helpers.marginOutside * 2 + 0.75 * Helpers.hexes * (Helpers.height + Helpers.marginInside));
            _graphics.PreferredBackBufferWidth = (int)(Helpers.marginOutside * 2 + Helpers.hexes * (Helpers.width + Helpers.marginInside) + Helpers.width / 2);
        }

        protected override void Initialize()
        {
            hexagons = new()
            {
                new Hexagon(new Vector2(-1, -1))
            };
            for (int i = 0; i < Helpers.hexes; i++)
            {
                for (int j = 0; j < Helpers.hexes; j++)
                {
                    hexagons.Add(new Hexagon(new Vector2(j, i)));
                }
            }

            hexagons.ForEach(hexagon => {
                hexagon.Link(hexagons.FindAll(neighbor =>
                    Helpers.GetLinking(hexagon.Position).Contains(neighbor.Position)));

                if (hexagon.Position.X == 0 || hexagon.Position.Y == 0 || hexagon.Position.X == Helpers.hexes - 1 || hexagon.Position.Y == Helpers.hexes - 1) 
                    hexagon.Link(hexagons[0]);
            });

            Helpers.mouseListener.MouseUp += (sender, args) =>
            {
                var target = hexagons.Find(item => item.IsInside(args.Position.ToVector2()));
                var current = hexagons.Find(item => item.Position == cat.Position);

                if (target is not null && args.Button == MouseButton.Left && target.Active && target.Position != cat.Position && !cat.isJumping)
                {
                    target.Active = false;
                    var catHex = hexagons.Find(item => item.Position == cat.Position);
                    var possible = from hex in hexagons where catHex.Linked.Contains(hex) && hex.Active select hex.Position;
                    cat.Jump(possible.ToArray()[new Random().Next(possible.Count())]);
                }
            };


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var spriteSheet = Content.Load<SpriteSheet>("cat.sf", new JsonContentLoader());
            var sprite = new AnimatedSprite(spriteSheet);

            sprite.Play("idle");
            cat = new(sprite);
        }

        protected override void Update(GameTime gameTime)
        {
            Helpers.mouseListener.Update(gameTime);

            cat.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.D)) {}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var item in hexagons)
            {
                item.Draw(_spriteBatch);
            }


            cat.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}