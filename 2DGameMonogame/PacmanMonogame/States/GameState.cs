using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanMonogame.Manager;
using PacmanMonogame.Sprites;
using Sprites;
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
        private Texture2D _texture;
        private Texture2D healthTexture;
        private Texture2D powerUpTexture;

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        private Jeu _game;
        private List<Sprite> _sprites;

        Player player;

        Texture2D healthBar;
        Rectangle healthRectangle;
        private EnemyManager _enemyManager;
        private PowerUpManager _powerUpManager;
        private Random _random;
            
        public GameState(Jeu game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _random = new Random();
            

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
            _spriteBatch.Draw(healthTexture,healthRectangle,Color.White);
            _spriteBatch.End();
        }

        public override void LoadContent()


        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _texture = _content.Load<Texture2D>("play");

            var bulletTexture = _content.Load<Texture2D>("Bullet");
            _font = _content.Load<SpriteFont>("Font");
            healthTexture = _content.Load<Texture2D>("Health");
            powerUpTexture = _content.Load<Texture2D>("goldCoin");
            _powerUpManager = new PowerUpManager(powerUpTexture);

            player = new Player(_texture)
            {
                Position = new Vector2(100, 100),
                Origin = new Vector2(_texture.Width / 2, _texture.Height / 2),
                Bullet = new Bullet(bulletTexture)

            };

            _enemyManager = new EnemyManager(_texture, bulletTexture, player);
            _sprites = new List<Sprite>() {
                player,
            };
            _enemyManager.SpawnEnemies().ForEach(x =>
            {
                _sprites.Add(x);
            });
        }

        public override void PostUpdate(GameTime gameTime)
        {
          
        }

        public override void Update(GameTime gameTime)
        {
            var randomNumber = _random.Next(0, 1000);
            if (randomNumber > 998)
            {
                _powerUpManager.SpawnPowerUps().ForEach(x =>
                {
                    _sprites.Add(x);
                });
            }

            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);

            player.Update(gameTime,_sprites);
            PostUpdate(gameTime);


        }
    }
}
