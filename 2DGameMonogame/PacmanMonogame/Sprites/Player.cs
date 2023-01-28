using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Sprites
{
    public class Player : Sprite
    {
       
     
        public float Speed { get; set; }

        public float Health = 100;

        public Rectangle rectangle;
        public bool isDead
        {
            get
            {
                return Health <= 0;
            }
        }


        public Bullet Bullet;

        public Player(Texture2D texture) : base(texture) 
        {
            Speed = 3f;
            Position = new Vector2(1000,1000);
        
        }


        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            GetActions(sprites);
        }


        private void UpdateHealthBar(SpriteBatch spriteBatch)
        {
            if(Health > 0)
            {
                spriteBatch.Draw(_texture, rectangle, Color.White);
            }

        }
        private void GetActions(List<Sprite> sprites)
        {


            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            if (currentKey.IsKeyDown(Keys.A))
            {
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            }
            if (currentKey.IsKeyDown(Keys.E))
            {
                _rotation += MathHelper.ToRadians(RotationVelocity);
            }
            var direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(180) - _rotation), -(float)Math.Sin(_rotation));

            if (currentKey.IsKeyDown(Keys.Z))
            {
                Position += direction * LinearVelocity;
            }
            if (currentKey.IsKeyDown(Keys.S))
            {
                Position -= direction * LinearVelocity;
            }
            if (currentKey.IsKeyDown(Keys.Space) && previousKey.IsKeyUp(Keys.Space))
            {
                ShootBullet(sprites);
            }
        }


        public void ShootBullet(List<Sprite> sprites)
        {
            var direction = new Vector2((float)Math.Cos( _rotation), (float)Math.Sin(_rotation));
            var bullet = Bullet.Clone() as Bullet;
                bullet.Direction = direction;
                bullet.Position = this.Position;
                bullet.LinearVelocity = this.LinearVelocity *2;
                bullet.LifeSpan = 2f;
                bullet.Parent = this;

            sprites.Add(bullet);
            
        }
    }
}
