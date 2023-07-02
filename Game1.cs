using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Input;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cat_Trap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Cat cat;
        private List<Hexagon> hexagons;

        private const int hexes = 11;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = (int)(Helpers.marginOutside * 2 + 0.75 * hexes * (Helpers.height + Helpers.marginInside));
            _graphics.PreferredBackBufferWidth = (int)(Helpers.marginOutside * 2 + hexes * (Helpers.width + Helpers.marginInside) + Helpers.width / 2);
        }

        protected override void Initialize()
        {
            hexagons = new();
            for (int i = 0; i < hexes; i++)
            {
                for (int j = 0; j < hexes; j++)
                {
                    hexagons.Add(new Hexagon(new Vector2(i, j)));
                }
            }

            Helpers.mouseListener.MouseUp += (sender, args) =>
            {
                var target = hexagons.Find(item => item.IsInside(args.Position.ToVector2()));
                if (target is null) return;
                if (args.Button == MouseButton.Right) cat.Jump(target.Position);
                else target.Active = false;
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

            if (Keyboard.GetState().IsKeyDown(Keys.D)) { }

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