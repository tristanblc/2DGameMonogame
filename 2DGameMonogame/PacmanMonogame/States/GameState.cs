using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanMonogame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.States
{
    public class GameState : State
    {
        private GraphicsDevice _graphicsDevice;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;


        private SpriteFont _font;
        private Texture2D _pacmanTexture;


        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        private Jeu _game;
        private List<Sprite> _sprites;
        public GameState(Jeu game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);

            _spriteBatch.End();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _pacmanTexture = _content.Load<Texture2D>("pacman");
            _font = _content.Load<SpriteFont>("Font");

            _sprites = new List<Sprite>() {
                new Player( _pacmanTexture )
                {
                    Position = new Vector2(100,100)

                }


            };
        }

        public override void PostUpdate(GameTime gameTime)
        {
          
        }

        public override void Update(GameTime gameTime)
        {


            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);

            PostUpdate(gameTime);


        }
    }
}
