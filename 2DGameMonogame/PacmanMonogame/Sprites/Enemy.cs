using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PacmanMonogame;
using PacmanMonogame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprites
{
    public class Enemy : Sprite
    {
        public float Speed { get; set; }

        public float Health = 100;

        public Bullet Bullet;

        public Random Random;

        public bool isDead
        {
            get
            {
                return Health <= 0;
            }
        }

        private SoundEffect _shootSound;
        public Enemy(Texture2D texture, SoundEffect shootSong) : base(texture)
        {
            Random = new Random();
            _shootSound = shootSong;
          
        }

        public Sprite SetFollowTarget(Sprite followTarget, float followDistance)
        {
            FollowTarget = followTarget;
            FollowDistance = followDistance;
            return this;
        }    

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Follow();
            var randomNumber = Random.Next(0,100);
            if(randomNumber > 98) {
                ShootBullet(sprites);
            }
           
        }

        private void Follow()
        {
            if (FollowTarget == null)
            {
                return;
            }

            var distance = FollowTarget.Position - this.Position;
            _rotation = (float)Math.Atan2(distance.Y, distance.X);
            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            var currentDirection = Vector2.Distance(this.Position, FollowTarget.Position);
            if (currentDirection > FollowDistance)
            {
                var t = MathHelper.Min((float)Math.Abs(currentDirection - FollowDistance), LinearVelocity);
                var velocity = Direction * t;
                Position += velocity;

            }

            if (Position.X >= Globals.ScreenWidth - 3)
                Position.X = Globals.ScreenWidth - 5;
            if (Position.Y < 2)
                Position.Y = 5;
            if (Position.X < 2)
                Position.X = 5;
            if (Position.Y > Globals.ScreenHeight - 3)
                Position.Y = Globals.ScreenHeight - 5;


        }

        private void ShootBullet(List<Sprite> sprites)
        {
            var direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity *2 ;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;
            sprites.Add(bullet);
            _shootSound.Play();
        }

    }
}
