using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanMonogame.Sprites
{
    public class Rock : Sprite
    {
        private float _timer;

        public Rock(Texture2D texture) : base(texture)
        {
            LifeSpan = 1.80f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }
            Position += Direction * LinearVelocity;

        }

        public override void OnCollide(Sprite sprite)
        {
            if (sprite == this.Parent)
                return;


            if (sprite is MegaPowerUp)
            {
                var mega = sprite as MegaPowerUp;
                mega.IsRemoved = true;
            }
             
            if(sprite is Enemy)
            {
                Enemy enemy = sprite as Enemy;
                enemy.IsRemoved = true;
            }
         
            if(sprite is Bullet)
            {
                var bullet = sprite as Bullet;
                bullet.IsRemoved = true;
            }
            IsRemoved = true;



        }
    }
}
