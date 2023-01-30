using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacmanMonogame.Core;
using PacmanMonogame.Manager;
using PacmanMonogame.Sprites;
using Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Camera _camera;

        private SpriteFont _font;
        private Texture2D _texture;
        private Texture2D healthTexture;
        private Texture2D powerUpTexture;
        private Texture2D backgroundTexture;
        private Texture2D bulletTexture;
        private Texture2D floortexture;
        private Texture2D rocketTexture;

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
        private MegaPowerUpManager _megaManager;
        private Random _random;
        private int numberOfEnemies { get; set; } = 3 ;
           
        public GameState(Jeu game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _random = new Random();
            _game = game;
           
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteBatch.Begin();
            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);

            _spriteBatch.DrawString(_font, $"Health : {player.Health}", new Vector2(50, 30), Color.Black);
     
            if (!player.isSwitch)
            {
                _spriteBatch.DrawString(_font, $"Ammo : {player.Cooldown - player.ShootCounter}", new Vector2(50, 50), Color.Black);
            }                
            else
            {
                _spriteBatch.DrawString(_font, $"Ammo Rocket : {player.CooldownRocket - player.ShootRocketCounter}", new Vector2(50, 50), Color.Black);
             
            }
                

            _spriteBatch.DrawString(_font, $"Ammo Mega Shoot : {player.CooldownMega - player.ShootCounterMega}", new Vector2(50, 70), Color.Black);

            _spriteBatch.End();
        }

        public override void LoadContent()
        {
            _camera = new Camera();

            _spriteBatch = new SpriteBatch(_graphicsDevice);


            _texture = _content.Load<Texture2D>("play");
            backgroundTexture = _content.Load<Texture2D>("ground");
            bulletTexture = _content.Load<Texture2D>("Bullet");
            _font = _content.Load<SpriteFont>("Font");
            healthTexture = _content.Load<Texture2D>("Healthbar");
            powerUpTexture = _content.Load<Texture2D>("hearth");
            floortexture = _content.Load<Texture2D>("floor");
            rocketTexture = _content.Load<Texture2D>("rocket");

            rectangle = new Rectangle(0, 0, healthTexture.Width, healthTexture.Height);
            _powerUpManager = new PowerUpManager(powerUpTexture);

            _megaManager = new MegaPowerUpManager(bulletTexture);

            player = new Player(_texture)
            {
                Position = new Vector2(100, 100),
                Origin = new Vector2(_texture.Width / 2, _texture.Height / 2),
                Bullet = new Bullet(bulletTexture),
                Rocket = new Rocket(rocketTexture)

            };

            _enemyManager = new EnemyManager(_texture, bulletTexture, player);
            _sprites = new List<Sprite>() {
                player,
                new Box(floortexture)
                {
                    Position = new Vector2(0, 0),
                    Origin = new Vector2(_texture.Width / 2, _texture.Height / 2),
                },
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
                if (_sprites[i] is Bullet)
                {
                    var bullet= (Bullet)_sprites[i];
                    if (bullet.IsRemoved) {
                        {
                            _sprites.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    if (_sprites[i].IsRemoved)
                    {
                        _sprites.RemoveAt(i);
                        i--;
                    }
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


            if (player.isDead)
            {
                _game.ChangeState(new GameOverState(_game, _graphicsDevice, _content));
            }

            var randomNumber = _random.Next(0, 100000);
            if (randomNumber > 99500)
            {

                _powerUpManager.SpawnPowerUps().ForEach(x =>
                {
                    _sprites.Add(x);
                });
            }

            var randomNumber_mega = _random.Next(0, 100000);
            if (randomNumber_mega > 99500)
            {

                _megaManager.SpawnPowerUps().ForEach(x =>
                {
                    _sprites.Add(x);
                });
            }
            PostUpdate(gameTime);


        }
    }
}
