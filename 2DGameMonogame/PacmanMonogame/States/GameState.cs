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
        private Rectangle rectangle;

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
        private int numberOfEnemies { get; set; } = 3 ;
           
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
            _spriteBatch.DrawString(_font, $"Health : {player.Health}", new Vector2(50, 30), Color.Black);
            _spriteBatch.End();
        }

        public override void LoadContent()


        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _texture = _content.Load<Texture2D>("play");

            var bulletTexture = _content.Load<Texture2D>("Bullet");
            _font = _content.Load<SpriteFont>("Font");
            healthTexture = _content.Load<Texture2D>("Healthbar");
            powerUpTexture = _content.Load<Texture2D>("goldCoin");

            rectangle = new Rectangle(0, 0, healthTexture.Width, healthTexture.Height);
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
            _enemyManager.SpawnEnemies(numberOfEnemies).ForEach(x =>
            {
                _sprites.Add(x);
            });
        }



        public override void PostUpdate(GameTime gameTime)
        {
            foreach (var spriteA in _sprites)
            {
                foreach (var spriteB in _sprites)
                {
                    if (spriteA == spriteB)
                        continue;

                    if (spriteA.Intersects(spriteB))
                        spriteA.OnCollide(spriteB);
                }
            }

            int count = _sprites.Count;
            for (int i = 0; i < count; i++)
            {
                foreach (var child in _sprites[i].Children)
                    _sprites.Add(child);

                _sprites[i].Children.Clear();
            }

            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }


        public override void Update(GameTime gameTime)
        {

            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);

            var countEnemies = 0;
            foreach(var sprite in _sprites)
                if(sprite is Enemy)
                    countEnemies++;

            if(countEnemies == 0) 
            {
                _enemyManager.SpawnEnemies(numberOfEnemies).ForEach(x =>
                {
                    _sprites.Add(x);
                });
            }
            player.Update(gameTime,_sprites);
            PostUpdate(gameTime);


        }
    }
}
