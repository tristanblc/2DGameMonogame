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

        public float Cooldown = 20;
        public float CooldownTimer = 0.25f;
 

        public float CooldownMega = 5;
        public float CooldownTimerMega = 0.35f;
        public float _timer;
        public float _timers;

        public float ShootCounter = 0;
        public float ShootCounterMega = 0;
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
            GetActions(sprites,gameTime);
        }


        private void UpdateHealthBar(SpriteBatch spriteBatch)
        {
            if(Health > 0)
            {
                spriteBatch.Draw(_texture, rectangle, Color.White);
            }

        }
        private void GetActions(List<Sprite> sprites,GameTime gameTime)
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

            if (currentKey.IsKeyDown(Keys.S))
            {
                Position += direction * LinearVelocity;
            }
            if (currentKey.IsKeyDown(Keys.Z))
            {
                Position -= direction * LinearVelocity;
            }

            if (currentKey.IsKeyDown(Keys.T) && previousKey.IsKeyUp(Keys.T))
            {
                _timers += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (ShootCounterMega < CooldownMega)
                {
                    ShootMegaBullet(sprites);
                    ShootCounterMega++;
                }            
            }

            if (currentKey.IsKeyDown(Keys.Space) && previousKey.IsKeyUp(Keys.Space))
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
             
                if (ShootCounter < Cooldown )
                {
                    ShootBullet(sprites);
                    ShootCounter++;
                }
                else
                {                    
                    if(_timer > CooldownTimer)
                    {
                        ShootCounter = 0;
                        _timer = 0;
                    }

                }
                    
               
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


        public void ShootMegaBullet(List<Sprite> sprites)
        {
            var direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
            var direction2 = new Vector2((float)Math.Cos(_rotation + 5), (float)Math.Sin(_rotation + 5));
            var direction3 = new Vector2((float)Math.Cos(_rotation - 5), (float)Math.Sin(_rotation - 5));


            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = direction2;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;


            var bullet2 = Bullet.Clone() as Bullet;
            bullet2.Direction = direction;
            bullet2.Position = this.Position;
            bullet2.LinearVelocity = this.LinearVelocity * 2;
            bullet2.LifeSpan = 2f;
            bullet2.Parent = this;

            var bullet3 = Bullet.Clone() as Bullet;
            bullet3.Direction = direction3;
            bullet3.Position = this.Position;
            bullet3.LinearVelocity = this.LinearVelocity * 2;
            bullet3.LifeSpan = 2f;
            bullet3.Parent = this;

            sprites.Add(bullet);
            sprites.Add(bullet2);
            sprites.Add(bullet3);
        }
    }
}
