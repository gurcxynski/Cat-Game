using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cat_Trap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Cat cat;
        private Menu menu;
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
            menu = new();
            hexagons = new()
            {
                Helpers.target
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

                if (Helpers.IsBorderHex(hexagon.Position)) hexagon.Link(Helpers.target);
            });

            hexagons.ForEach(hexagon => { if (Helpers.rng.NextDouble() < Helpers.fraction && hexagon.Position != Helpers.CatStart && hexagon != Helpers.target) hexagon.Deactivate(); });

            Helpers.mouseListener.MouseUp += OnClick;
            
            GenerateWeights();

            base.Initialize();
        }

        void OnClick(object sender, MouseEventArgs args)
        {
            var target = hexagons.Find(item => item.IsInside(args.Position.ToVector2()));
            var current = GetHexByPosition(cat.Position);

            if (target is not null && args.Button == MouseButton.Left && target.Active && target.Position != cat.Position && !Cat.IsJumping && !Cat.Escaped)
            {
                target.Deactivate();

                GenerateWeights();

                var possible =
                    from hex in hexagons
                    where current.Linked.Contains(hex) && hex.Active
                    select hex;

                int fastest = int.MaxValue;
                var best = new List<Vector2>();

                foreach (var item in possible)
                {
                    if (item.Value < fastest)
                    {
                        fastest = item.Value;
                        best = new();
                    }
                    if (item.Value == fastest) best.Add(item.Position);
                }
                if (fastest == int.MaxValue)
                {
                    StateMachine.GameOver(); return;
                }
                cat.Jump(best[Helpers.rng.Next(best.Count)]);
            }
        }

        void GenerateWeights()
        {
            hexagons.ForEach(hex => hex.ResetValue());
            Helpers.target.GenerateValue(0);
        }

        Hexagon GetHexByPosition(Vector2 pos) => hexagons.Find(item => pos == item.Position);

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

            if (StateMachine.State != StateMachine.GameState.Menu) cat.Update(gameTime);
            else menu.Update();
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


            if (StateMachine.State != StateMachine.GameState.Menu) cat.Draw(_spriteBatch);
            else menu.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}