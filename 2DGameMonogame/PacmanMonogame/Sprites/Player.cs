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


        public float CooldownRocket = 5;
        public float ShootCounter = 0;
        public float ShootRocketCounter = 0;
        public float ShootCounterMega = 0;

        public Rectangle rectangle;
        public bool isDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public bool isSwitch { get; private set; }

        public Bullet Bullet;
        public Rocket Rocket;

        public Player(Texture2D texture) : base(texture)
        {
            Speed = 3f;
            Position = new Vector2(1000, 1000);
            isSwitch = false;

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            GetActions(sprites, gameTime);
        }

        private void GetActions(List<Sprite> sprites, GameTime gameTime)
        {


            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            currentMouseState = Mouse.GetState();

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
                if (Position.X > Globals.ScreenWidth- 3)
                    Position.X = Globals.ScreenWidth - 5;
                if (Position.Y < 2)
                    Position.Y = 5;
                if (Position.Y > 500)
                    Position.Y = 200;
                if (Position.X < 2)
                    Position.X = 5;
                if (Position.Y > Globals.ScreenHeight - 3)
                    Position.Y = Globals.ScreenHeight - 5;
                else
                    Position += direction * LinearVelocity;



            }
            if (currentKey.IsKeyDown(Keys.Z))
            {
                if(Position.X >= Globals.ScreenWidth - 3)
                    Position.X = Globals.ScreenWidth - 5;
                if (Position.Y < 2)
                    Position.Y = 5;
                if (Position.X < 2)
                    Position.X = 5;
                if (Position.Y > Globals.ScreenHeight - 3)
                    Position.Y = Globals.ScreenHeight - 5;

                else                
                    Position -= direction * LinearVelocity;
            }


            if(currentKey.IsKeyDown(Keys.Q) && previousKey.IsKeyUp(Keys.Q))
            {
                isSwitch = !isSwitch;
            }
            
            if (currentKey.IsKeyDown(Keys.R) && previousKey.IsKeyUp(Keys.R))
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
             
                if(isSwitch == false)
                {
                    ShootRocketCounter = 0;
                    if (ShootCounter < Cooldown)
                    {

                        ShootBullet(sprites);
                        ShootCounter++;
                    }
                    else
                    {
                        if (_timer > CooldownTimer)
                        {
                            ShootCounter = 0;
                            _timer = 0;
                        }

                    }
                }
                if(isSwitch == true)
                {
                    ShootCounter = 0;
                    if (ShootRocketCounter < CooldownRocket)
                    {

                        ShootRocket(sprites);
                        ShootRocketCounter++;
                    }
                    else
                    {
                        if (_timer > CooldownTimer)
                        {
                            ShootRocketCounter = 0;
                            _timer = 0;
                        }

                    }
                }




            }
        }


        private void CheckLimit(Vector2 Position, bool sign, Vector2 direction)
        {
           
      
          
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
        public void ShootRocket(List<Sprite> sprites)
        {
            var direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
            var rocket = Rocket.Clone() as Rocket;
            rocket.Direction = direction;
            rocket.Position = this.Position;
            rocket.LinearVelocity = this.LinearVelocity * 3;
            rocket.LifeSpan = 2f;
            rocket.Parent = this;
            sprites.Add(rocket);


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
